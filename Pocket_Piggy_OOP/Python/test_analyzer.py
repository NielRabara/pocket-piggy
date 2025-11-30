#!/usr/bin/env python3
"""
Simple test analyzer - no external dependencies
Tests if Python integration is working
"""

import sys
import csv
from datetime import datetime

def main():
    if len(sys.argv) < 2:
        print("Usage: python test_analyzer.py <csv_file>")
        sys.exit(1)
    
    csv_file = sys.argv[1]
    
    try:
        # Read CSV file
        transactions = []
        with open(csv_file, 'r', encoding='utf-8') as f:
            reader = csv.DictReader(f)
            for row in reader:
                transactions.append(row)
        
        # Filter spending transactions
        spending_types = ['Expenses', 'Bills', 'BillPlan', 'ExpensePlan']
        spending_transactions = [
            t for t in transactions 
            if t.get('transaction_type', '').strip() in spending_types
        ]
        
        # Filter income transactions
        income_types = ['Income', 'Add Balance']
        income_transactions = [
            t for t in transactions 
            if t.get('transaction_type', '').strip() in income_types
        ]
        
        if not spending_transactions:
            print("# Test Analysis - No Spending Data")
            print("No spending transactions found.")
            return
        
        # Calculate spending totals
        total_spending = 0
        category_totals = {}
        
        for t in spending_transactions:
            try:
                amount = abs(float(t.get('amount', 0)))
                category = t.get('category', 'Uncategorized').strip()
                
                total_spending += amount
                category_totals[category] = category_totals.get(category, 0) + amount
            except (ValueError, TypeError):
                continue
        
        # Calculate income totals
        total_income = 0
        for t in income_transactions:
            try:
                amount = abs(float(t.get('amount', 0)))
                total_income += amount
            except (ValueError, TypeError):
                continue
        
        # Find top category
        top_category = max(category_totals.items(), key=lambda x: x[1]) if category_totals else ('None', 0)
        
        # Generate report in professional format
        current_month = datetime.now().strftime('%B %Y')
        avg_per_transaction = total_spending / len(spending_transactions) if spending_transactions else 0
        
        # Sort categories by amount
        sorted_categories = sorted(category_totals.items(), key=lambda x: x[1], reverse=True)
        
        # Calculate savings metrics
        net_savings = total_income - total_spending
        savings_rate = (net_savings / total_income * 100) if total_income > 0 else 0
        savings_status = "surplus" if net_savings > 0 else "deficit"
        has_income_data = total_income > 0
        
        report = f"""# FinLlama Smart Spending Report – {current_month}

## AI Insights Summary
Your financial activity this month shows concentrated spending in a few categories, with a dominant portion going to {top_category[0]}. This is a basic analysis - install full ML packages for advanced AI insights and forecasting.

## Spending Overview
- **Total Spending**: P{total_spending:,.2f}
- **Number of Transactions**: {len(spending_transactions)}
- **Average per Transaction**: P{avg_per_transaction:,.2f}
- **Period Covered**: {current_month}

## Income and Savings Analysis"""
        
        if has_income_data:
            report += f"""
- **Total Income**: P{total_income:,.2f}
- **Net Savings**: P{net_savings:,.2f} ({savings_status})
- **Savings Rate**: {savings_rate:.1f}%
- **Spending Ratio**: {(total_spending/total_income*100) if total_income > 0 else 0:.1f}% of income"""
        else:
            report += """
- **Income Data**: Not available - add income transactions for complete analysis
- **Recommendation**: Record your monthly income to track savings rate and financial health"""
        
        report += f"""

## Top Spending Categories
"""
        
        for category, amount in sorted_categories[:5]:
            percentage = (amount / total_spending * 100) if total_spending > 0 else 0
            report += f"**{category}**: P{amount:,.2f} ({percentage:.1f}%)\n"
        
        if sorted_categories:
            report += f"\n**Insight**: The majority of expenses are concentrated in {top_category[0]}. Even small reductions in this area can produce significant monthly savings.\n"
        
        report += f"""
## Forecast (Next Month)
**Predicted High-Spend Category**:
{top_category[0]} — P{top_category[1]:,.2f} (based on current trend)

## AI Recommendations
### {top_category[0]} (P{top_category[1]:,.2f})
"""
        
        # Add category-specific recommendations
        if 'food' in top_category[0].lower() or 'dining' in top_category[0].lower():
            report += """- Prepare meals at home 4–5 days a week to reduce costs by up to 30%.
- Set a fixed weekly grocery budget between P3,000–P4,000.
- Limit eating out to a maximum of twice per week.
- Purchase staple items in bulk to reduce per-unit cost.

"""
        elif 'internet' in top_category[0].lower() or 'bill' in top_category[0].lower():
            report += """- Review your internet speed vs actual usage needs.
- Negotiate with providers for loyalty discounts or better rates.
- Consider bundling services for cost savings.
- Monitor data usage to avoid overage charges.

"""
        elif 'transport' in top_category[0].lower():
            report += """- Consider carpooling or public transport for daily commute.
- Plan errands efficiently to reduce number of trips.
- Look into monthly transport passes for potential savings.
- Maintain vehicle regularly to improve fuel efficiency.

"""
        else:
            report += f"""- Review and analyze your {top_category[0].lower()} spending patterns.
- Set a monthly budget limit for this category.
- Look for alternatives or ways to reduce costs.
- Track expenses weekly to stay within budget.

"""
        
        report += f"""### General Financial Tips
- **Total monthly spending**: P{total_spending:,.2f}"""
        
        if has_income_data:
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
- **Monthly Income**: P{total_income:,.2f}
- **Monthly Spending**: P{total_spending:,.2f}
- **Net Result**: P{net_savings:,.2f} ({savings_status})
- **Financial Health**: {'Good' if savings_rate >= 10 else 'Needs Improvement'}"""

        report += f"""

## System Status
- **Python Integration**: SUCCESS
- **CSV Processing**: SUCCESS
- **Data Analysis**: SUCCESS
- **ML Packages**: Not installed (basic analysis only)

### Upgrade to Full Analysis
Run `setup_python.bat` to install ML packages for:
- Advanced AI insights and forecasting
- Trend analysis and predictions
- Enhanced recommendations
- Income and goals tracking

---
*Generated by FinLlama Test Analyzer v2.0*
*Analysis completed: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}*
*For full AI features, install ML packages using setup_python.bat*"""

        print(report)
        
    except Exception as e:
        print(f"""# Test Analysis Error

## Error Details
{str(e)}

## Troubleshooting
- Check if CSV file exists and is readable
- Verify CSV has proper headers: transaction_type, amount, category
- Ensure Python has file access permissions

---
*Test failed at: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}*""")
        sys.exit(1)

if __name__ == "__main__":
    main()
