@echo off
echo Setting up Python FinLlama dependencies...
echo.

REM Check if Python is available
python --version >nul 2>&1
if %errorlevel% neq 0 (
    echo Python not found. Please install Python from https://python.org
    pause
    exit /b 1
)

echo Python found. Installing required packages...
echo.

REM Install packages
pip install -r requirements.txt

if %errorlevel% equ 0 (
    echo.
    echo ✅ Setup completed successfully!
    echo FinLlama Python analyzer is ready to use.
) else (
    echo.
    echo ❌ Installation failed. Please check your Python and pip installation.
)

echo.
pause
