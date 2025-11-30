using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketPiggy.Services
{
    public static class PythonFinLlamaService
    {
        private static readonly string PythonScriptPath = GetPythonScriptPath();
        private static readonly string TestScriptPath = GetTestScriptPath();

        private static string GetPythonScriptPath()
        {
            // Try multiple possible locations for the Python script
            var possiblePaths = new[]
            {
                // Direct path to your project
                @"C:\Users\Niel Rabara\source\repos\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOP\Python\finllama_analyzer.py",
                // Source directory (development)
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "Python", "finllama_analyzer.py"),
                // Relative to project root
                Path.Combine(Directory.GetCurrentDirectory(), "Python", "finllama_analyzer.py"),
                // In the same directory as executable
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Python", "finllama_analyzer.py"),
                // Try to find project root by looking for .csproj file
                FindPythonScriptInProjectRoot()
            };

            foreach (var path in possiblePaths)
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    return path;
                }
            }

            // Fallback to original path
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Python", "finllama_analyzer.py");
        }

        private static string FindPythonScriptInProjectRoot()
        {
            try
            {
                var currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                
                // Walk up the directory tree to find the project root
                while (currentDir != null && currentDir.Parent != null)
                {
                    // Look for .csproj file to identify project root
                    if (currentDir.GetFiles("*.csproj").Length > 0)
                    {
                        var pythonScript = Path.Combine(currentDir.FullName, "Python", "finllama_analyzer.py");
                        if (File.Exists(pythonScript))
                        {
                            return pythonScript;
                        }
                    }
                    currentDir = currentDir.Parent;
                }
            }
            catch
            {
                // Ignore errors in path resolution
            }
            
            return null;
        }

        private static string GetTestScriptPath()
        {
            // Try multiple possible locations for the test script
            var possiblePaths = new[]
            {
                // Direct path to your project
                @"C:\Users\Niel Rabara\source\repos\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOP\Python\test_analyzer.py",
                // Source directory (development)
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "Python", "test_analyzer.py"),
                // Relative to project root
                Path.Combine(Directory.GetCurrentDirectory(), "Python", "test_analyzer.py"),
                // In the same directory as executable
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Python", "test_analyzer.py"),
                // Try to find project root by looking for .csproj file
                FindTestScriptInProjectRoot()
            };

            foreach (var path in possiblePaths)
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    return path;
                }
            }

            // Fallback to original path
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Python", "test_analyzer.py");
        }

        private static string FindTestScriptInProjectRoot()
        {
            try
            {
                var currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                
                // Walk up the directory tree to find the project root
                while (currentDir != null && currentDir.Parent != null)
                {
                    // Look for .csproj file to identify project root
                    if (currentDir.GetFiles("*.csproj").Length > 0)
                    {
                        var testScript = Path.Combine(currentDir.FullName, "Python", "test_analyzer.py");
                        if (File.Exists(testScript))
                        {
                            return testScript;
                        }
                    }
                    currentDir = currentDir.Parent;
                }
            }
            catch
            {
                // Ignore errors in path resolution
            }
            
            return null;
        }

        public static async Task<string> GenerateSpendingInsightAsync(DataTable transactions)
        {
            try
            {
                // Create temporary CSV file
                string tempCsvPath = Path.GetTempFileName().Replace(".tmp", ".csv");
                
                // Export DataTable to CSV
                await ExportDataTableToCsvAsync(transactions, tempCsvPath);
                
                // Try full ML script first if packages available, otherwise use test script
                string result;
                
                // Check if ML packages are available by trying to import them
                bool mlPackagesAvailable = await CheckMLPackagesAvailable();
                
                if (mlPackagesAvailable && File.Exists(PythonScriptPath))
                {
                    try
                    {
                        // Use full ML analyzer
                        result = await CallPythonScriptAsync(tempCsvPath, PythonScriptPath);
                    }
                    catch (Exception fullEx)
                    {
                        // If full script fails, fall back to test script
                        if (File.Exists(TestScriptPath))
                        {
                            result = await CallPythonScriptAsync(tempCsvPath, TestScriptPath);
                        }
                        else
                        {
                            throw new Exception($"Full ML script failed ({fullEx.Message}) and test script not found at: {TestScriptPath}");
                        }
                    }
                }
                else
                {
                    // Use test script (no ML packages or full script not found)
                    if (File.Exists(TestScriptPath))
                    {
                        result = await CallPythonScriptAsync(tempCsvPath, TestScriptPath);
                    }
                    else
                    {
                        throw new Exception($"Test script not found at: {TestScriptPath} and ML packages not available");
                    }
                }
                
                // Clean up temp file
                try { File.Delete(tempCsvPath); } catch { }
                
                return result;
            }
            catch (Exception ex)
            {
                return GenerateFallbackAnalysis(transactions, ex.Message);
            }
        }

        private static async Task ExportDataTableToCsvAsync(DataTable dt, string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Write headers
                var headers = new string[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    headers[i] = dt.Columns[i].ColumnName;
                }
                await writer.WriteLineAsync(string.Join(",", headers));

                // Write data rows
                foreach (DataRow row in dt.Rows)
                {
                    var values = new string[dt.Columns.Count];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        var value = row[i]?.ToString() ?? "";
                        // Escape commas and quotes
                        if (value.Contains(",") || value.Contains("\""))
                        {
                            value = "\"" + value.Replace("\"", "\"\"") + "\"";
                        }
                        values[i] = value;
                    }
                    await writer.WriteLineAsync(string.Join(",", values));
                }
            }
        }

        private static async Task<string> CallPythonScriptAsync(string csvPath, string scriptPath = null)
        {
            // Try different Python executables - prioritize the one that worked in command line
            string[] pythonCommands = { "python" }; // Only use python since that's what works
            
            foreach (string pythonCmd in pythonCommands)
            {
                try
                {
                    var targetScript = scriptPath ?? PythonScriptPath;
                    var processInfo = new ProcessStartInfo
                    {
                        FileName = pythonCmd,
                        Arguments = $"\"{targetScript}\" \"{csvPath}\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        WorkingDirectory = Path.GetDirectoryName(targetScript) ?? Directory.GetCurrentDirectory()
                    };

                    using (var process = Process.Start(processInfo))
                    {
                        if (process == null)
                            continue;

                        // Use Task.WhenAny for proper timeout handling
                        var outputTask = process.StandardOutput.ReadToEndAsync();
                        var errorTask = process.StandardError.ReadToEndAsync();
                        var timeoutTask = Task.Delay(15000); // 15 second timeout

                        var completedTask = await Task.WhenAny(
                            Task.WhenAll(outputTask, errorTask),
                            timeoutTask
                        );

                        if (completedTask == timeoutTask)
                        {
                            // Timeout occurred
                            try { process.Kill(); } catch { }
                            throw new Exception($"Python script timed out after 15 seconds using '{pythonCmd}' with command: {pythonCmd} \"{targetScript}\" \"{csvPath}\"");
                        }

                        var output = await outputTask;
                        var error = await errorTask;
                        
                        // Wait a bit more for process to exit
                        if (!process.WaitForExit(2000))
                        {
                            try { process.Kill(); } catch { }
                        }

                        if (process.ExitCode == 0 && !string.IsNullOrWhiteSpace(output))
                        {
                            return output.Trim();
                        }
                        
                        if (!string.IsNullOrWhiteSpace(error))
                        {
                            throw new Exception($"Python script error with '{pythonCmd}': {error}");
                        }
                        
                        if (string.IsNullOrWhiteSpace(output))
                        {
                            throw new Exception($"Python script '{pythonCmd}' exited with code {process.ExitCode} and no output. Command: {pythonCmd} \"{targetScript}\" \"{csvPath}\"");
                        }
                        
                        throw new Exception($"Python script '{pythonCmd}' exited with code {process.ExitCode}. Output: {output}");
                    }
                }
                catch (Exception ex)
                {
                    // Try next Python command
                    if (pythonCmd == pythonCommands[pythonCommands.Length - 1])
                    {
                        throw new Exception($"Failed to execute Python script with all commands. Last error: {ex.Message}");
                    }
                }
            }
            
            throw new Exception("Could not find Python interpreter (tried: python, python3, py)");
        }

        private static string GenerateFallbackAnalysis(DataTable dt, string error)
        {
            try
            {
                // Simple fallback analysis without external dependencies
                var spendingTypes = new[] { "Expenses", "Bills", "BillPlan", "ExpensePlan" };
                var spendingRows = dt.AsEnumerable()
                    .Where(row => spendingTypes.Contains(row["transaction_type"]?.ToString() ?? "", StringComparer.OrdinalIgnoreCase))
                    .ToList();

                if (spendingRows.Count == 0)
                {
                    return $@"# Python FinLlama Analysis Failed

## Error Details
{error}

## No Spending Data Found
No transactions of type 'Expenses', 'Bills', 'BillPlan', or 'ExpensePlan' were found.

### Troubleshooting Steps:
1. Run setup_python.bat to install Python dependencies
2. Ensure Python is in your system PATH
3. Check that spending transactions exist in your data

---
*Python analysis failed. Please set up Python environment.*";
                }

                decimal totalSpending = 0;
                var categoryTotals = new Dictionary<string, decimal>();

                foreach (var row in spendingRows)
                {
                    var category = row["category"]?.ToString() ?? "Uncategorized";
                    var amountStr = row["amount"]?.ToString() ?? "0";
                    if (decimal.TryParse(amountStr, out decimal amount))
                    {
                        amount = Math.Abs(amount);
                        totalSpending += amount;
                        categoryTotals[category] = categoryTotals.GetValueOrDefault(category, 0) + amount;
                    }
                }

                var topCategory = categoryTotals.OrderByDescending(kv => kv.Value).FirstOrDefault();

                return $@"# Python FinLlama Analysis Failed

## Error Details
{error}

## Path Debugging Information
- **Script path being used**: {PythonScriptPath}
- **Script exists**: {File.Exists(PythonScriptPath)}
- **Current directory**: {Directory.GetCurrentDirectory()}
- **Base directory**: {AppDomain.CurrentDomain.BaseDirectory}

## Basic Fallback Analysis
- **Total spending**: ₱{totalSpending:N2}
- **Top category**: {topCategory.Key} (₱{topCategory.Value:N2})
- **Transactions analyzed**: {spendingRows.Count}

## Recommendations
- Set up Python environment by running setup_python.bat
- Focus on reducing {topCategory.Key} expenses
- Track spending patterns weekly

### Setup Instructions:
1. **Close your application first**
2. Navigate to: `C:\Users\Niel Rabara\source\repos\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOPV2 - Copy\Pocket_Piggy_OOP\Python\`
3. Double-click `setup_python.bat`
4. Wait for installation to complete
5. **Rebuild your application** (Build → Rebuild Solution)
6. Try the analysis again

---
*Python analysis failed. Using basic C# fallback.*";
            }
            catch (Exception fallbackError)
            {
                return $@"# FinLlama Analysis Error

## Python Analysis Failed
Error: {error}

## Fallback Analysis Also Failed
Error: {fallbackError.Message}

## Basic Information
- Transactions in dataset: {dt.Rows.Count}
- Python script location: {PythonScriptPath}

### Troubleshooting Steps:
1. Run setup_python.bat in the Python folder
2. Ensure Python is installed and accessible via PATH
3. Install required packages: pandas, numpy, scikit-learn, requests
4. Check that the Python script exists

---
*Both Python and fallback analysis failed. Please check your setup.*";
            }
        }

        private static async Task<bool> CheckMLPackagesAvailable()
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "-c \"import pandas, numpy, sklearn, requests; print('ML_PACKAGES_AVAILABLE')\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(processInfo))
                {
                    if (process == null) return false;

                    var outputTask = process.StandardOutput.ReadToEndAsync();
                    var timeoutTask = Task.Delay(5000); // 5 second timeout

                    var completedTask = await Task.WhenAny(outputTask, timeoutTask);
                    
                    if (completedTask == timeoutTask)
                    {
                        try { process.Kill(); } catch { }
                        return false;
                    }

                    var output = await outputTask;
                    process.WaitForExit(1000);

                    return process.ExitCode == 0 && output.Contains("ML_PACKAGES_AVAILABLE");
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPythonAvailable()
        {
            string[] pythonCommands = { "python" };
            
            foreach (string cmd in pythonCommands)
            {
                try
                {
                    var processInfo = new ProcessStartInfo
                    {
                        FileName = cmd,
                        Arguments = "--version",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    using (var process = Process.Start(processInfo))
                    {
                        if (process != null)
                        {
                            process.WaitForExit(5000);
                            if (process.ExitCode == 0)
                            {
                                return true;
                            }
                        }
                    }
                }
                catch
                {
                    // Continue to next command
                }
            }
            
            return false;
        }
    }
}
