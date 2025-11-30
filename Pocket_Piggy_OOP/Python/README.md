# FinLlama Python AI Integration

Advanced spending analysis using Python machine learning and AI models.

## 🚀 Features

### Advanced Analytics
- **Machine Learning Forecasting**: Uses scikit-learn for linear regression predictions
- **Smart Category Analysis**: Identifies spending patterns and trends
- **Month-over-Month Growth**: Calculates spending changes over time
- **AI-Powered Insights**: Integrates with Hugging Face for natural language insights

### Intelligent Recommendations
- **Category-Specific Advice**: Tailored recommendations for Food, Transport, Bills, etc.
- **Percentage-Based Savings**: Specific savings targets (15-30% reductions)
- **Actionable Tips**: Meal prepping, budgeting, service optimization
- **Financial Rules**: 50/30/20 budgeting guidance

### Robust Error Handling
- **Multiple Python Detection**: Tries python, python3, py commands
- **Graceful Fallbacks**: Falls back to local analysis if Python fails
- **Dependency Checking**: Handles missing libraries gracefully
- **Timeout Protection**: 30-second timeout for Python execution

## 📋 Requirements

### Python Installation
1. **Install Python 3.7+** from [python.org](https://python.org)
2. **Add Python to PATH** during installation
3. **Verify installation**: Open Command Prompt and run `python --version`

### Required Packages
Run the setup script or install manually:

```bash
# Option 1: Run setup script (recommended)
cd Python
setup_python.bat

# Option 2: Manual installation
pip install pandas numpy scikit-learn requests
```

## 🎯 How It Works

### 1. Data Processing
- Filters spending transactions (Expenses, Bills, BillPlan, ExpensePlan)
- Converts amounts to absolute values
- Groups by categories and months
- Calculates trends and growth rates

### 2. Machine Learning Analysis
- **Linear Regression**: Predicts next month's spending per category
- **Trend Analysis**: Identifies increasing/decreasing patterns
- **Statistical Summaries**: Mean, totals, percentages

### 3. AI Insights Generation
- Sends structured data to Hugging Face FLAN-T5 model
- Generates natural language insights
- Provides contextual financial advice
- Falls back to local analysis if API unavailable

### 4. Report Generation
- Comprehensive markdown report
- Visual spending breakdowns
- Actionable recommendations
- Forecast predictions

## 🔧 Usage

### From C# Application
The integration is automatic:
1. Click "Analyze (Python AI)" in Transactions dialog
2. Python script runs in background
3. Results displayed in popup window
4. Option to save report as .md or .txt

### Command Line Usage
```bash
cd Python
python finllama_analyzer.py "path/to/transactions.csv"
```

## 📊 Sample Output

```markdown
# FinLlama Python Analysis Report

## 🤖 AI Insights
Your food spending represents 93% of total expenses. Consider meal planning 
and bulk purchasing to reduce costs by 20-25%.

## 📊 Summary of Spending
- **Total spending**: ₱26,799.00
- **Transaction count**: 2
- **Average per transaction**: ₱13,399.50

### Top Spending Categories:
1. **Food & Dining**: ₱25,000.00 (93.3%)
2. **Internet**: ₱1,799.00 (6.7%)

## 🔮 Predicted High-Spend Category Next Month
- **Food & Dining** (₱25,000.00)
- *Forecast method: ML Linear Regression*

## 💡 AI Recommendations
🍽️ **Food & Dining** (₱25,000.00): Your biggest expense!
   • Try meal prepping 4-5 days per week to save 20-30%
   • Set a weekly grocery budget of ₱3,000-4,000
   • Cook at home 5 days/week, eat out max 2 days
```

## 🛠️ Troubleshooting

### Python Not Found
- Install Python from [python.org](https://python.org)
- Ensure "Add Python to PATH" is checked during installation
- Restart your application after Python installation

### Package Installation Errors
```bash
# Upgrade pip first
python -m pip install --upgrade pip

# Install packages with user flag
pip install --user pandas numpy scikit-learn requests
```

### Permission Errors
- Run Command Prompt as Administrator
- Or use `--user` flag with pip install

### Timeout Issues
- Check internet connection for Hugging Face API
- Python script has 30-second timeout protection
- Falls back to local analysis automatically

## 🔄 Fallback Behavior

If Python analysis fails, the system automatically:
1. Uses LocalAnalytics.cs for basic analysis
2. Provides detailed error information
3. Suggests troubleshooting steps
4. Maintains full functionality

## 🎨 Customization

### Modify Analysis Logic
Edit `finllama_analyzer.py`:
- Change spending categories in `IncludeTypes`
- Adjust ML model parameters
- Customize recommendation logic
- Modify report formatting

### Update AI Model
Change Hugging Face model in Python script:
```python
self.hf_api_url = "https://api-inference.huggingface.co/models/your-model"
```

## 📈 Performance

- **Analysis time**: 2-10 seconds depending on data size
- **Memory usage**: ~50MB for Python process
- **API calls**: 1 request to Hugging Face per analysis
- **Fallback time**: <1 second if Python fails

---

*FinLlama Python AI Integration v1.0*
