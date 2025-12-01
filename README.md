# 🐷 Pocket Piggy - Personal & Business Finance Management System

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![MySQL](https://img.shields.io/badge/MySQL-8.0+-orange.svg)](https://www.mysql.com/)
[![Python](https://img.shields.io/badge/Python-3.8+-green.svg)](https://www.python.org/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

A comprehensive financial management application built with C# WinForms, featuring AI-powered spending analysis, dual account types (Personal & Business), and advanced financial tracking capabilities.

## 📋 Table of Contents

- [Features](#-features)
- [System Requirements](#-system-requirements)
- [Installation](#-installation)
- [Database Setup](#-database-setup)
- [Configuration](#-configuration)
- [Usage](#-usage)
- [AI Integration](#-ai-integration)
- [Architecture](#-architecture)
- [Version Control](#-version-control)
- [Contributing](#-contributing)
- [Troubleshooting](#-troubleshooting)
- [License](#-license)

## 🚀 Features

### Personal Finance Management
- **Transaction Tracking**: Income, Expenses, Bills, and Savings
- **Financial Dashboard**: Real-time balance and spending summaries
- **Monthly Reports**: Automated financial analysis and trends
- **Profile Management**: User profiles with picture upload
- **Security**: Password change requests with admin approval

### Business Finance Management
- **Business Transactions**: Income, Expenses, Receivables, Cash Reserves
- **Inventory Management**: Stock tracking and valuation
- **KPI Dashboard**: Key Performance Indicators and metrics
- **Business Reports**: Comprehensive financial analysis
- **Multi-user Support**: Separate business account management

### AI-Powered Analysis
- **FinLlamma Analyzer**: Advanced spending analysis using ML models
- **Smart Recommendations**: AI-generated financial advice
- **Spending Forecasts**: Predictive analysis for future expenses
- **Category Analysis**: Automated expense categorization
- **Financial Health Scoring**: Comprehensive financial wellness assessment

### Administrative Features
- **Admin Panel**: User management and system oversight
- **Ticket System**: Password change approval workflow
- **User Analytics**: System usage and financial statistics
- **Data Management**: Backup and restore capabilities

## 🖥️ System Requirements

### Minimum Requirements
- **OS**: Windows 10 or later (64-bit)
- **Framework**: .NET 8.0 Runtime
- **Database**: MySQL 8.0 or later
- **Python**: 3.8 or later (for AI features)
- **Memory**: 4 GB RAM
- **Storage**: 500 MB free space

### Recommended Requirements
- **OS**: Windows 11 (64-bit)
- **Framework**: .NET 8.0 SDK (for development)
- **Database**: MySQL 8.0+ with 2GB+ allocated memory
- **Python**: 3.9+ with virtual environment
- **Memory**: 8 GB RAM
- **Storage**: 2 GB free space

## 📦 Installation

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/pocket-piggy.git
cd pocket-piggy
```

### 2. Install .NET Dependencies
```bash
# Restore NuGet packages
dotnet restore Pocket_Piggy_OOP/Pocket_Piggy_OOP.csproj

# Build the application
dotnet build Pocket_Piggy_OOP/Pocket_Piggy_OOP.csproj --configuration Release
```

### 3. Install Python Dependencies
```bash
# Navigate to Python directory
cd Pocket_Piggy_OOP/Python

# Install Python dependencies
pip install -r requirements.txt

# Or run the setup script (Windows)
setup_python.bat
```

### 4. Database Setup
See [Database Setup](#-database-setup) section below.

## 🗄️ Database Setup

### MySQL Installation
1. **Download MySQL**: [MySQL Community Server](https://dev.mysql.com/downloads/mysql/)
2. **Install MySQL** with default settings
3. **Set root password** during installation
4. **Start MySQL service**

### Database Creation
```sql
-- Create database
CREATE DATABASE pocketpiggydb CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Create user (optional, for security)
CREATE USER 'pocketpiggy'@'localhost' IDENTIFIED BY 'your_secure_password';
GRANT ALL PRIVILEGES ON pocketpiggydb.* TO 'pocketpiggy'@'localhost';
FLUSH PRIVILEGES;
```

### Schema Setup
The application will automatically create required tables on first run. Core tables include:

- `users` - Personal user accounts
- `business_users` - Business user accounts  
- `transactions` - Personal financial transactions
- `business_transactions` - Business financial transactions
- `tickets` - Admin approval system
- `questionnaire_answers` - User questionnaire responses
- `goals` - Financial goals tracking

## ⚙️ Configuration

### Environment Variables
Set these environment variables for database configuration:

```bash
# Database Configuration
DB_SERVER=localhost
DB_NAME=pocketpiggydb
DB_USER=root
DB_PASSWORD=your_mysql_password
DB_PORT=3306

# AI Configuration (Optional)
HUGGINGFACE_TOKEN=your_huggingface_token
```

### Windows Environment Variables
1. **Open System Properties** → Advanced → Environment Variables
2. **Add New Variables**:
   - `DB_SERVER`: `localhost`
   - `DB_NAME`: `pocketpiggydb`
   - `DB_USER`: `root`
   - `DB_PASSWORD`: `your_mysql_password`

### Application Configuration
The application uses these default settings if environment variables are not set:
- **Server**: localhost
- **Database**: pocketpiggydb
- **User**: root
- **Password**: (empty)
- **Port**: 3306

## 🎯 Usage

### First Time Setup
1. **Run the application**:
   ```bash
   dotnet run --project Pocket_Piggy_OOP/Pocket_Piggy_OOP.csproj
   ```

2. **Create an account**:
   - Choose Personal or Business account type
   - Fill in required information
   - Complete the questionnaire

3. **Start tracking finances**:
   - Add income, expenses, and savings
   - Upload profile picture
   - Explore the dashboard

### Personal Account Features
- **Dashboard**: View balance, recent transactions, monthly totals
- **Add Transactions**: Income, Expenses, Bills, Savings
- **Profile Settings**: Update name, picture, security settings
- **FinLlama Analysis**: AI-powered spending insights
- **Reports**: Monthly and yearly financial summaries

### Business Account Features
- **Business Dashboard**: KPIs, cash flow, receivables
- **Transaction Management**: Business income and expenses
- **Inventory Tracking**: Stock levels and valuations
- **Financial Reports**: Profit/loss, cash flow statements
- **Multi-location Support**: Manage multiple business locations

### Admin Features
- **User Management**: View and manage all users
- **Ticket System**: Approve password change requests
- **System Analytics**: Usage statistics and reports
- **Data Management**: Backup and maintenance tools

## 🤖 AI Integration

### FinLlamma Analyzer
The application includes advanced AI-powered financial analysis:

#### Features
- **Spending Pattern Analysis**: Identifies trends and anomalies
- **Category-based Insights**: Detailed breakdown by expense categories
- **Financial Health Scoring**: Comprehensive wellness assessment
- **Predictive Forecasting**: Future spending predictions
- **Personalized Recommendations**: AI-generated financial advice

#### Setup
1. **Install Python dependencies**:
   ```bash
   pip install pandas numpy scikit-learn requests
   ```

2. **Configure Hugging Face Token** (optional):
   ```bash
   set HUGGINGFACE_TOKEN=your_token_here
   ```

3. **Test AI functionality**:
   ```bash
   cd Pocket_Piggy_OOP/Python
   python debug_test.py
   ```

#### Usage
- **Access from Menu**: Click "Analyze (FinLlamma Analyzer)"
- **View Reports**: Clean report view with bold headings and readable lists
- **Save Reports**: Save to database (ai_reports, ai_report_items) or copy text
- **HTML Render**: Report is rendered as formatted HTML (headings, lists); tables are simplified for readability

#### Database Objects (AI Reports)
When saving an AI report, the app uses these tables (auto-created if missing):

```sql
CREATE TABLE IF NOT EXISTS ai_reports (
  id INT AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(255) NOT NULL,
  title VARCHAR(255) NULL,
  report_text LONGTEXT NULL,
  total_income DECIMAL(18,2) NULL,
  total_spending DECIMAL(18,2) NULL,
  net_savings DECIMAL(18,2) NULL,
  savings_rate DECIMAL(9,2) NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS ai_report_items (
  id INT AUTO_INCREMENT PRIMARY KEY,
  report_id INT NOT NULL,
  category VARCHAR(255) NULL,
  item_type ENUM('Income','Spending') NOT NULL,
  amount DECIMAL(18,2) NOT NULL,
  percent DECIMAL(9,2) NULL,
  CONSTRAINT fk_ai_report_items_report FOREIGN KEY (report_id)
    REFERENCES ai_reports(id) ON DELETE CASCADE
);
```

Indexes (optional):
```sql
CREATE INDEX idx_ai_reports_username_created ON ai_reports(username, created_at);
CREATE INDEX idx_ai_report_items_report ON ai_report_items(report_id);
```

## 🏗️ Architecture

### Project Structure
```
Pocket_Piggy_OOP/
├── Models/                 # Data models and database layer
│   ├── Database.cs        # Database connection and utilities
│   ├── BusinessTransaction.cs
│   └── ...
├── ViewModels/            # MVVM pattern view models
│   ├── SignUpViewModels.cs
│   ├── TransactionViewModel.cs
│   └── ...
├── View/                  # Personal user interface forms
│   ├── LogIn.cs
│   ├── Menu.cs
│   ├── SignUp.cs
│   └── ...
├── View_Business/         # Business user interface forms
│   ├── businessMain.cs
│   ├── BusinessSignUp.cs
│   └── ...
├── Repositories/          # Data access layer
│   ├── ProfileRepository.cs
│   └── ...
├── Services/              # External service integrations
│   ├── PythonFinLlamaService.cs
│   └── ...
├── Python/                # AI and analytics scripts
│   ├── finllama_analyzer.py
│   ├── test_analyzer.py
│   ├── requirements.txt
│   └── ...
└── Properties/            # Application properties and resources
```

### Design Patterns
- **MVVM (Model-View-ViewModel)**: Separation of concerns
- **Repository Pattern**: Data access abstraction
- **Service Layer**: External integrations
- **Factory Pattern**: Object creation
- **Observer Pattern**: Event handling

### Technologies Used
- **Frontend**: C# WinForms (.NET 8.0)
- **Backend**: MySQL Database
- **AI/ML**: Python with pandas, scikit-learn, numpy
- **Charts**: WinForms.DataVisualization
- **Export**: EPPlus for Excel integration
- **Security**: SHA256 password hashing

## 📊 Version Control

### Current Version: 2.1.0

### Version History
- **v2.1.0** (Current)
  - ✅ FinLlamma Analyzer tabular report UI/UX (clean single-column HTML render)
  - ✅ Save AI reports to database (ai_reports, ai_report_items)
  - ✅ Menu updated: "Analyze (FinLlamma Analyzer)"

- **v2.0.0**
  - ✅ AI-powered FinLlama integration
  - ✅ Enhanced profile management with pictures
  - ✅ Flexible date parsing for international formats
  - ✅ Improved admin panel with ticket system
  - ✅ Business account enhancements
  - ✅ Docker containerization support

- **v1.5.0**
  - ✅ Business account functionality
  - ✅ Inventory management system
  - ✅ KPI dashboard and reporting
  - ✅ Multi-user support

- **v1.0.0**
  - ✅ Personal finance tracking
  - ✅ Basic transaction management
  - ✅ User authentication system
  - ✅ MySQL database integration

### Branching Strategy
- **main**: Production-ready code
- **develop**: Development branch
- **feature/***: Feature development branches
- **hotfix/***: Critical bug fixes
- **release/***: Release preparation branches

### Git Workflow
```bash
# Clone repository
git clone https://github.com/yourusername/pocket-piggy.git

# Create feature branch
git checkout -b feature/new-feature

# Make changes and commit
git add .
git commit -m "feat: add new feature"

# Push to remote
git push origin feature/new-feature

# Create pull request
# Merge after review
```

## 🤝 Contributing

### Development Setup
1. **Fork the repository**
2. **Clone your fork**:
   ```bash
   git clone https://github.com/yourusername/pocket-piggy.git
   ```
3. **Install dependencies** (see Installation section)
4. **Create feature branch**:
   ```bash
   git checkout -b feature/your-feature-name
   ```
5. **Make changes and test**
6. **Submit pull request**

### Code Standards
- **C# Coding Standards**: Follow Microsoft C# conventions
- **Database Naming**: Use snake_case for table/column names
- **Python Code**: Follow PEP 8 style guide
- **Comments**: Document complex logic and public methods
- **Testing**: Include unit tests for new features

### Pull Request Process
1. **Update documentation** for any new features
2. **Add tests** for new functionality
3. **Ensure all tests pass**
4. **Update version numbers** if applicable
5. **Submit PR** with detailed description

## 🔧 Troubleshooting

### Common Issues

#### Database Connection Issues
```
Error: Unable to connect to MySQL server
```
**Solution**:
1. Verify MySQL service is running
2. Check connection string in environment variables
3. Ensure database exists and user has permissions
4. Test connection with MySQL Workbench

#### Python AI Features Not Working
```
Error: Python script execution failed
```
**Solution**:
1. Verify Python installation: `python --version`
2. Install dependencies: `pip install -r requirements.txt`
3. Check Python path in system environment
4. Run debug script: `python debug_test.py`

#### Profile Picture Issues
```
Error: Unknown column 'name' in 'field list'
```
**Solution**:
1. Database schema mismatch - run schema update
2. Check ProfileRepository.cs for correct column names
3. Verify `display_name` column exists in users table

#### Date Format Errors in FinLlama
```
Error: time data doesn't match format
```
**Solution**:
1. Update to latest version (includes flexible date parsing)
2. Check transaction date formats in database
3. Verify Python pandas version: `pip install pandas>=1.3.0`

### Debug Mode
Enable debug mode by modifying `Program.cs`:
```csharp
// Enable debug logging
string testUser = "your_test_username";
Application.Run(new LogIn());
```

### Log Files
Application logs are stored in:
- **Windows**: `%APPDATA%/PocketPiggy/logs/`
- **Debug Output**: Visual Studio Output window

### Support
For additional support:
1. **Check existing issues**: [GitHub Issues](https://github.com/yourusername/pocket-piggy/issues)
2. **Create new issue**: Provide detailed error description
3. **Include logs**: Attach relevant log files
4. **System info**: OS version, .NET version, MySQL version

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **MySQL**: Database management system
- **Microsoft .NET**: Application framework
- **Python Community**: AI/ML libraries (pandas, scikit-learn, numpy)
- **Hugging Face**: AI model hosting and APIs
- **Chart.js**: Data visualization components
- **EPPlus**: Excel integration library

## 📞 Contact

- **Developer**: Niel Rabara
- **Email**: nrRabara@mcm.edu.ph
- **Email**: nielrabara90@gmail.com
- **GitHub**: [@NielRabara](https://github.com/NielRabara)
- **Discord**: shun1o


- **Developer**: Sittie Almaira S. Palanggalan
- **Email**: alamirap.921@gmail.com
- **GitHub**: [@aira219](https://github.com/aira219)

- **Project Link**: [https://github.com/yourusername/pocket-piggy]([https://github.com/yourusername/pocket-piggy](https://github.com/NielRabara/pocket-piggy.git))

---

**Made with ❤️ for better financial management**

*Last updated: December 2025*
