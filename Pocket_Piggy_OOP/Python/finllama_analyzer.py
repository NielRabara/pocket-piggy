#!/usr/bin/env python3
"""
FinLlama Python Analyzer
Advanced spending analysis using local ML models
"""

import sys
import json
import pandas as pd
import numpy as np
import os
from datetime import datetime, timedelta
import warnings
warnings.filterwarnings('ignore')

# Try to import ML libraries, fallback if not available
try:
    from sklearn.linear_model import LinearRegression
    from sklearn.preprocessing import StandardScaler
    HAS_SKLEARN = True
except ImportError:
    HAS_SKLEARN = False

try:
    import requests
    HAS_REQUESTS = True
except ImportError:
    HAS_REQUESTS = False

class FinLlamaAnalyzer:
    def __init__(self):
        # Read Hugging Face token from environment for security
        self.hf_token = os.getenv("HUGGINGFACE_TOKEN", "")
        self.hf_api_url = "https://api-inference.huggingface.co/models/google/flan-t5-base"
        
    def analyze_spending(self, csv_data):
        """Main analysis function"""
        try:
            # Parse CSV data
            df = pd.read_csv(csv_data) if isinstance(csv_data, str) else pd.DataFrame(csv_data)
            
            # Filter spending transactions
            spending_types = ['Expenses', 'Bills', 'BillPlan', 'ExpensePlan']
            df_spending = df[df['transaction_type'].isin(spending_types)].copy()
            
            # Get income data
            income_types = ['Income', 'Add Balance', 'Savings']
            df_income = df[df['transaction_type'].isin(income_types)].copy()
            
            if df_spending.empty:
                return self._generate_no_data_report()
            
            # Clean and prepare data
            df_spending['amount'] = df_spending['amount'].abs()
            df_spending['date'] = self._parse_dates_flexible(df_spending['date'])
            df_spending['month'] = df_spending['date'].dt.to_period('M')
            
            # Process income data
            if not df_income.empty:
                df_income['amount'] = df_income['amount'].abs()
                df_income['date'] = self._parse_dates_flexible(df_income['date'])
                df_income['month'] = df_income['date'].dt.to_period('M')
            
            # Generate analysis
            summary = self._generate_summary(df_spending, df_income)
            forecast = self._generate_forecast(df_spending)
            recommendations = self._generate_recommendations(df_spending, summary)
            
            # Try to get AI insights
            ai_insights = self._get_ai_insights(summary, forecast)
            
            return self._format_report(summary, forecast, recommendations, ai_insights, df_income)
            
        except Exception as e:
            return self._generate_error_report(str(e))
    
    def _parse_dates_flexible(self, date_series):
        """Flexible date parsing to handle multiple formats"""
        try:
            # Try multiple date formats commonly used in databases
            formats_to_try = [
                '%d/%m/%Y %I:%M:%S %p',  # 25/11/2024 12:00:00 AM
                '%d/%m/%Y %H:%M:%S',     # 25/11/2024 12:00:00
                '%Y-%m-%d %H:%M:%S',     # 2024-11-25 12:00:00
                '%Y-%m-%d',              # 2024-11-25
                '%d/%m/%Y',              # 25/11/2024
                '%m/%d/%Y',              # 11/25/2024
            ]
            
            # First try pandas automatic parsing with mixed format
            try:
                return pd.to_datetime(date_series, format='mixed', dayfirst=True)
            except:
                pass
            
            # Try each format individually
            for fmt in formats_to_try:
                try:
                    return pd.to_datetime(date_series, format=fmt)
                except:
                    continue
            
            # Last resort: let pandas infer the format
            return pd.to_datetime(date_series, infer_datetime_format=True, dayfirst=True)
            
        except Exception as e:
            # If all else fails, return current date for all entries
            print(f"Date parsing failed: {e}")
            return pd.to_datetime(['2024-01-01'] * len(date_series))
    
    def _generate_summary(self, df_spending, df_income=None):
        """Generate spending summary statistics with income analysis"""
        total_spending = df_spending['amount'].sum()
        transaction_count = len(df_spending)
        avg_per_transaction = total_spending / transaction_count if transaction_count > 0 else 0
        
        # Category analysis
        category_totals = df_spending.groupby('category')['amount'].sum().sort_values(ascending=False)
        
        # Monthly trends
        monthly_totals = df_spending.groupby('month')['amount'].sum().sort_index()
        
        # Calculate month-over-month growth
        mom_growth = {}
        if len(monthly_totals) > 1:
            for i in range(1, len(monthly_totals)):
                current = monthly_totals.iloc[i]
                previous = monthly_totals.iloc[i-1]
                growth = ((current - previous) / previous * 100) if previous > 0 else 0
                mom_growth[monthly_totals.index[i]] = growth
        
        # Income analysis
        total_income = 0
        monthly_income = {}
        savings_rate = 0
        net_savings = 0
        
        if df_income is not None and not df_income.empty:
            total_income = df_income['amount'].sum()
            monthly_income = df_income.groupby('month')['amount'].sum().to_dict()
            monthly_income = {str(k): v for k, v in monthly_income.items()}
            
            # Calculate savings (income - spending)
            net_savings = total_income - total_spending
            savings_rate = (net_savings / total_income * 100) if total_income > 0 else 0
        
        return {
            'total_spending': total_spending,
            'transaction_count': transaction_count,
            'avg_per_transaction': avg_per_transaction,
            'category_totals': category_totals.to_dict(),
            'monthly_totals': {str(k): v for k, v in monthly_totals.to_dict().items()},
            'mom_growth': {str(k): v for k, v in mom_growth.items()},
            'latest_month': str(monthly_totals.index[-1]) if len(monthly_totals) > 0 else 'N/A',
            'total_income': total_income,
            'monthly_income': monthly_income,
            'net_savings': net_savings,
            'savings_rate': savings_rate
        }
    
    def _generate_forecast(self, df):
        """Generate spending forecast using ML if available"""
        if not HAS_SKLEARN or len(df) < 3:
            return self._simple_forecast(df)
        
        try:
            # Prepare data for ML forecasting
            monthly_data = df.groupby(['month', 'category'])['amount'].sum().reset_index()
            
            forecasts = {}
            for category in df['category'].unique():
                cat_data = monthly_data[monthly_data['category'] == category]
                if len(cat_data) >= 2:
                    # Simple linear regression forecast
                    X = np.arange(len(cat_data)).reshape(-1, 1)
                    y = cat_data['amount'].values
                    
                    model = LinearRegression()
                    model.fit(X, y)
                    
                    # Predict next month
                    next_month_pred = model.predict([[len(cat_data)]])[0]
                    forecasts[category] = max(0, next_month_pred)  # Ensure non-negative
            
            # Find predicted highest spending category
            predicted_top = max(forecasts.items(), key=lambda x: x[1]) if forecasts else ('N/A', 0)
            
            return {
                'method': 'ML Linear Regression',
                'category_forecasts': forecasts,
                'predicted_top_category': predicted_top[0],
                'predicted_top_amount': predicted_top[1]
            }
            
        except Exception:
            return self._simple_forecast(df)
    
    def _simple_forecast(self, df):
        """Simple forecast based on recent trends"""
        category_totals = df.groupby('category')['amount'].sum()
        top_category = category_totals.idxmax() if len(category_totals) > 0 else 'N/A'
        top_amount = category_totals.max() if len(category_totals) > 0 else 0
        
        return {
            'method': 'Simple Trend Analysis',
            'category_forecasts': category_totals.to_dict(),
            'predicted_top_category': top_category,
            'predicted_top_amount': top_amount
        }
    
    def _generate_recommendations(self, df, summary):
        """Generate personalized recommendations (now handled in _format_report)"""
        # This method is now primarily used for validation
        # The actual recommendations are generated in _format_report for better formatting
        category_totals = summary['category_totals']
        
        if not category_totals:
            return ["No spending data available for recommendations."]
        
        return ["Recommendations generated in report format."]
    
    def _get_ai_insights(self, summary, forecast):
        """Try to get AI insights from Hugging Face"""
        if not HAS_REQUESTS:
            return "AI insights unavailable (requests library not installed)"

        if not self.hf_token:
            return "AI insights unavailable (HUGGINGFACE_TOKEN not configured)"
        
        try:
            top_categories = sorted(summary['category_totals'].items(), key=lambda x: x[1], reverse=True)[:3]
            top_cat_text = ", ".join([f"{cat}: P{amt:,.0f}" for cat, amt in top_categories])
            
            prompt = f"""Analyze this spending data and provide 2-3 key financial insights:
            
Total spending: P{summary['total_spending']:,.2f}
Top categories: {top_cat_text}
Predicted next month top category: {forecast['predicted_top_category']}

Provide brief, actionable financial advice:"""
            
            headers = {"Authorization": f"Bearer {self.hf_token}"}
            payload = {
                "inputs": prompt,
                "parameters": {
                    "max_new_tokens": 150,
                    "temperature": 0.7,
                    "do_sample": True
                }
            }
            
            response = requests.post(self.hf_api_url, headers=headers, json=payload, timeout=10)
            
            if response.status_code == 200:
                result = response.json()
                if isinstance(result, list) and len(result) > 0:
                    return result[0].get('generated_text', '').strip()
                elif isinstance(result, dict):
                    return result.get('generated_text', '').strip()
            
            return f"AI API unavailable (status: {response.status_code})"
            
        except Exception as e:
            return f"AI insights unavailable: {str(e)}"
    
    def _format_report(self, summary, forecast, recommendations, ai_insights, df_income=None):
        """Format the final report in professional style"""
        top_categories = sorted(summary['category_totals'].items(), key=lambda x: x[1], reverse=True)[:5]
        current_month = datetime.now().strftime('%B %Y')
        
        # Determine financial status
        has_income_data = summary.get('total_income', 0) > 0
        savings_status = "surplus" if summary.get('net_savings', 0) > 0 else "deficit"
        
        report = f"""# FinLlama Smart Spending Report – {current_month}

## AI Insights Summary
{ai_insights if ai_insights and not ai_insights.startswith('AI') else 'Your financial activity this month shows concentrated spending in a few categories, with a dominant portion going to ' + (top_categories[0][0] if top_categories else 'various categories') + '. Based on trend analysis, this pattern is expected to continue next month unless adjustments are made.'}

## Spending Overview
- **Total Spending**: P{summary['total_spending']:,.2f}
- **Number of Transactions**: {summary['transaction_count']}
- **Average per Transaction**: P{summary['avg_per_transaction']:,.2f}
- **Period Covered**: {current_month}

## Income and Savings Analysis"""
        
        if has_income_data:
            report += f"""
- **Total Income**: P{summary['total_income']:,.2f}
- **Net Savings**: P{summary['net_savings']:,.2f} ({savings_status})
- **Savings Rate**: {summary['savings_rate']:.1f}%
- **Spending Ratio**: {(summary['total_spending']/summary['total_income']*100) if summary['total_income'] > 0 else 0:.1f}% of income"""
        else:
            report += """
- **Income Data**: Not available - add income transactions for complete analysis
- **Recommendation**: Record your monthly income to track savings rate and financial health"""
        
        report += f"""

## Top Spending Categories
"""
        
        for i, (category, amount) in enumerate(top_categories, 1):
            percentage = (amount / summary['total_spending'] * 100) if summary['total_spending'] > 0 else 0
            report += f"**{category}**: P{amount:,.2f} ({percentage:.1f}%)\n"
        
        if top_categories:
            top_cat_name, top_cat_amount = top_categories[0]
            top_percentage = (top_cat_amount / summary['total_spending'] * 100) if summary['total_spending'] > 0 else 0
            report += f"\n**Insight**: The majority of expenses are concentrated in {top_cat_name}. Even small reductions in this area can produce significant monthly savings.\n"
        
        report += f"""
## Forecast (Next Month)
**Predicted High-Spend Category**:
{forecast['predicted_top_category']} — P{forecast['predicted_top_amount']:,.2f} (based on trend projection)

## AI Recommendations
"""
        
        # Enhanced recommendations in the professional format
        if top_categories:
            top_cat_name, top_cat_amount = top_categories[0]
            report += f"### {top_cat_name} (P{top_cat_amount:,.2f})\n"
            
            if 'food' in top_cat_name.lower() or 'dining' in top_cat_name.lower():
                report += """- Prepare meals at home 4–5 days a week to reduce costs by up to 30%.
- Set a fixed weekly grocery budget between P3,000–P4,000.
- Limit eating out to a maximum of twice per week.
- Purchase staple items in bulk to reduce per-unit cost.

"""
            elif 'internet' in top_cat_name.lower() or 'bill' in top_cat_name.lower():
                report += """- Review your internet speed vs actual usage needs.
- Negotiate with providers for loyalty discounts or better rates.
- Consider bundling services for cost savings.
- Monitor data usage to avoid overage charges.

"""
            elif 'transport' in top_cat_name.lower():
                report += """- Consider carpooling or public transport for daily commute.
- Plan errands efficiently to reduce number of trips.
- Look into monthly transport passes for potential savings.
- Maintain vehicle regularly to improve fuel efficiency.

"""
            else:
                report += f"""- Review and analyze your {top_cat_name.lower()} spending patterns.
- Set a monthly budget limit for this category.
- Look for alternatives or ways to reduce costs.
- Track expenses weekly to stay within budget.

"""
        
        report += f"""### General Financial Tips
- **Total monthly spending**: P{summary['total_spending']:,.2f}"""
        
        if has_income_data:
            savings_rate = summary.get('savings_rate', 0)
            if savings_rate >= 20:
                report += f"""
- **EXCELLENT savings rate** ({savings_rate:.1f}%) - you're on track with the 20% savings goal!"""
            elif savings_rate >= 10:
                report += f"""
- **GOOD savings rate** ({savings_rate:.1f}%) - try to reach 20% for optimal financial health."""
            elif savings_rate > 0:
                report += f"""
- **LOW savings rate** ({savings_rate:.1f}%) - aim for at least 20% of income."""
            else:
                report += f"""
- **WARNING: No savings** - you're spending more than you earn. Immediate budget review needed."""
        
        report += f"""
- Apply the 50/30/20 budgeting principle: 50% needs, 30% wants, 20% savings.
- Automate at least 20% of income for savings.
- Conduct weekly expense reviews to identify unnecessary spending.
- Use expense tracking tools for better monitoring and accountability."""

        if has_income_data:
            report += f"""

## Financial Health Summary
- **Monthly Income**: P{summary['total_income']:,.2f}
- **Monthly Spending**: P{summary['total_spending']:,.2f}
- **Net Result**: P{summary['net_savings']:,.2f} ({savings_status})
- **Financial Health**: {'Good' if summary['savings_rate'] >= 10 else 'Needs Improvement'}"""

        report += f"""

---
*Generated by FinLlama Python Analyzer v2.0*
*Analysis completed: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}*
"""
        
        return report
    
    def _generate_no_data_report(self):
        return """# FinLlama Analysis Report

## No Spending Data Found
No transactions of type 'Expenses', 'Bills', 'BillPlan', or 'ExpensePlan' were found in the provided data.

Please ensure you have recorded some spending transactions to generate insights.
"""
    
    def _generate_error_report(self, error):
        return f"""# FinLlama Analysis Error

An error occurred during analysis: {error}

Please check your transaction data format and try again.
"""

def main():
    """Main entry point for command line usage"""
    if len(sys.argv) < 2:
        print("Usage: python finllama_analyzer.py <csv_file_path>")
        sys.exit(1)
    
    csv_file = sys.argv[1]
    analyzer = FinLlamaAnalyzer()
    
    try:
        report = analyzer.analyze_spending(csv_file)
        print(report)
    except Exception as e:
        print(f"Error: {e}")
        sys.exit(1)

if __name__ == "__main__":
    main()
