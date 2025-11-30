using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PocketPiggy.Repositories;

namespace PocketPiggy.View
{
    public class FinLlamaReportForm : Form
    {
        private readonly string _username;
        private readonly DataTable _transactions;
        private readonly string _reportText;

        private Label _lblTitle;
        private Label _lblSummary;
        private Button _btnSave;
        private Button _btnCopy;

        private decimal _totalIncome;
        private decimal _totalSpending;
        private decimal _netSavings;
        private decimal _savingsRate;
        private DataTable _itemsTable;

        public FinLlamaReportForm(string username, DataTable transactions, string reportText)
        {
            _username = username;
            _transactions = transactions;
            _reportText = reportText ?? string.Empty;

            InitializeComponent();
            BuildItemsFromTransactions();
            BindUi();
        }

        private string BuildHtmlFromMarkdown(string md)
        {
            if (string.IsNullOrWhiteSpace(md)) md = "(No report content)";

            var sb = new StringBuilder();
            sb.AppendLine("<html><head><meta charset='utf-8'>");
            sb.AppendLine("<style>");
            sb.AppendLine("body{font-family:'Segoe UI',Arial,sans-serif;margin:12px;color:#1e1e1e;}");
            sb.AppendLine("h1,h2,h3{margin:10px 0 6px 0;}");
            sb.AppendLine("p{margin:6px 0;}");
            sb.AppendLine("table{border-collapse:collapse;width:100%;margin:8px 0;}");
            sb.AppendLine("th,td{border:1px solid #d0d7de;padding:6px 8px;text-align:left;}");
            sb.AppendLine("th{background:#f6f8fa;font-weight:600;}");
            sb.AppendLine("code,pre{font-family:Consolas,monospace;}");
            sb.AppendLine("</style></head><body>");

            // Simple block parser that supports headings, lists, and paragraphs
            var lines = md.Replace("\r\n", "\n").Split('\n');
            int i = 0;
            bool inUl = false;
            while (i < lines.Length)
            {
                string line = lines[i];

                // Headings # .. ######
                var m = Regex.Match(line, "^(#{1,6})\\s+(.*)$");
                if (m.Success)
                {
                    int level = m.Groups[1].Value.Length;
                    string text = InlineToHtml(m.Groups[2].Value);
                    sb.AppendFormat("<h{0}>{1}</h{0}>\n", level, text);
                    i++; continue;
                }

                // List items - unordered
                if (Regex.IsMatch(line, "^\\s*[-*]\\s+"))
                {
                    if (!inUl) { sb.Append("<ul>"); inUl = true; }
                    string item = Regex.Replace(line, "^\\s*[-*]\\s+", "");
                    sb.AppendFormat("<li>{0}</li>", InlineToHtml(item));
                    i++; continue;
                }
                else if (inUl && string.IsNullOrWhiteSpace(line))
                {
                    sb.Append("</ul>"); inUl = false; i++; continue;
                }

                // Blank -> paragraph break
                if (string.IsNullOrWhiteSpace(line)) { sb.Append("<br/>"); i++; continue; }

                // Paragraph
                sb.AppendFormat("<p>{0}</p>", InlineToHtml(line));
                i++;
            }
            if (inUl) sb.Append("</ul>");

            sb.AppendLine("</body></html>");
            return sb.ToString();
        }

        private string InlineToHtml(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            // Remove table pipes entirely
            s = s.Replace("|", "");

            // Convert **text** to <b><i>text</i></b> and remove remaining single '*'
            var rx = new Regex(@"\*\*(.+?)\*\*", RegexOptions.Singleline);
            var sb = new StringBuilder();
            int last = 0;
            foreach (Match match in rx.Matches(s))
            {
                // Append text before match (encode and strip single *)
                string before = s.Substring(last, match.Index - last).Replace("*", "");
                sb.Append(System.Net.WebUtility.HtmlEncode(before));

                // Matched content
                string inner = match.Groups[1].Value.Replace("*", "");
                sb.Append("<b><i>");
                sb.Append(System.Net.WebUtility.HtmlEncode(inner));
                sb.Append("</i></b>");

                last = match.Index + match.Length;
            }
            // Remainder
            string tail = s.Substring(last).Replace("*", "");
            sb.Append(System.Net.WebUtility.HtmlEncode(tail));

            return sb.ToString();
        }

        private void InitializeComponent()
        {
            Text = "FinLlamma Analyzer";
            Width = 980;
            Height = 700;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 76,
                Padding = new Padding(16, 12, 16, 8),
                BackColor = Color.FromArgb(245, 248, 252)
            };
            _lblTitle = new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Text = GetTitleFromReport(_reportText)
            };
            var lblSub = new Label
            {
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(90, 90, 90),
                Text = "FinLlamma Analyzer: insights, tips, and category breakdown"
            };
            header.Controls.Add(lblSub);
            header.Controls.Add(_lblTitle);

            var bottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 56,
                Padding = new Padding(12, 8, 12, 8),
                BackColor = Color.FromArgb(245, 248, 252)
            };
            _btnSave = new Button { Text = "Save to Database", Width = 140, Height = 32, Location = new Point(10, 10) };
            _btnCopy = new Button { Text = "Copy Text Report", Width = 140, Height = 32, Location = new Point(160, 10) };
            var btnClose = new Button { Text = "Close", Width = 90, Height = 32, Anchor = AnchorStyles.Right | AnchorStyles.Top };
            bottom.Resize += (s, a) => btnClose.Left = bottom.Width - btnClose.Width - 10;
            _btnSave.Click += OnSaveClicked;
            _btnCopy.Click += (s, a) => { try { Clipboard.SetText(_reportText); MessageBox.Show("Report copied to clipboard."); } catch (Exception ex) { MessageBox.Show($"Copy failed: {ex.Message}"); } };
            btnClose.Click += (s, a) => Close();
            bottom.Controls.Add(_btnSave);
            bottom.Controls.Add(_btnCopy);
            bottom.Controls.Add(btnClose);

            var content = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16), BackColor = Color.White };

            // Single full-width AI Report (plain text)
            var leftPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            var lblReportHeader = new Label
            {
                Text = "AI Report",
                Dock = DockStyle.Top,
                Height = 26,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold)
            };
            // Summary block under header
            var summaryPanel = new Panel { Dock = DockStyle.Top, Height = 90, Padding = new Padding(4) };
            _lblSummary = new Label
            {
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular)
            };
            summaryPanel.Controls.Add(_lblSummary);
            var webReport = new WebBrowser
            {
                Dock = DockStyle.Fill,
                AllowWebBrowserDrop = false,
                IsWebBrowserContextMenuEnabled = false,
                WebBrowserShortcutsEnabled = true,
                ScriptErrorsSuppressed = true
            };
            string html = BuildHtmlFromMarkdown(_reportText);
            webReport.DocumentText = html;
            // Add in order: Fill first, then summary, then header (for proper docking)
            leftPanel.Controls.Add(webReport);
            leftPanel.Controls.Add(summaryPanel);
            leftPanel.Controls.Add(lblReportHeader);
            content.Controls.Add(leftPanel);

            Controls.Add(content);
            Controls.Add(bottom);
            Controls.Add(header);
        }

        private string GetTitleFromReport(string text)
        {
            try
            {
                var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                if (lines.Length > 0 && lines[0].StartsWith("#"))
                {
                    return lines[0].TrimStart('#', ' ');
                }
            }
            catch { }
            return $"FinLlamma Analyzer – {DateTime.Now:MMMM yyyy}";
        }

        private void BuildItemsFromTransactions()
        {
            _totalIncome = 0m;
            _totalSpending = 0m;

            var dt = _transactions;
            if (dt == null || dt.Rows.Count == 0)
            {
                _itemsTable = new DataTable();
                _itemsTable.Columns.Add("Category");
                _itemsTable.Columns.Add("Type");
                _itemsTable.Columns.Add("Amount", typeof(decimal));
                _itemsTable.Columns.Add("Percent", typeof(decimal));
                return;
            }

            string[] spendingTypes = new[] { "Expenses", "Bills", "BillPlan", "ExpensePlan" };
            string[] incomeTypes = new[] { "Income" };

            foreach (DataRow row in dt.Rows)
            {
                string ttype = row["transaction_type"]?.ToString() ?? string.Empty;
                if (incomeTypes.Contains(ttype, StringComparer.OrdinalIgnoreCase))
                {
                    if (decimal.TryParse(row["amount"]?.ToString(), out var amt))
                        _totalIncome += amt;
                }
                else if (spendingTypes.Contains(ttype, StringComparer.OrdinalIgnoreCase))
                {
                    if (decimal.TryParse(row["amount"]?.ToString(), out var amt))
                        _totalSpending += Math.Abs(amt);
                }
            }

            _netSavings = _totalIncome - _totalSpending;
            _savingsRate = _totalIncome > 0 ? Math.Round((_netSavings / _totalIncome) * 100m, 2) : 0m;

            var spendingGroups = dt.AsEnumerable()
                .Where(r => spendingTypes.Contains((r["transaction_type"]?.ToString() ?? string.Empty), StringComparer.OrdinalIgnoreCase))
                .GroupBy(r => (r["category"]?.ToString() ?? "Uncategorized"))
                .Select(g => new { Category = g.Key, Amount = g.Sum(r => Math.Abs(ToDecimal(r["amount"]))) })
                .OrderByDescending(x => x.Amount)
                .ToList();

            var incomeGroups = dt.AsEnumerable()
                .Where(r => incomeTypes.Contains((r["transaction_type"]?.ToString() ?? string.Empty), StringComparer.OrdinalIgnoreCase))
                .GroupBy(r => (r["category"]?.ToString() ?? (r["type"]?.ToString() ?? "Income")))
                .Select(g => new { Category = g.Key, Amount = g.Sum(r => ToDecimal(r["amount"])) })
                .OrderByDescending(x => x.Amount)
                .ToList();

            _itemsTable = new DataTable();
            _itemsTable.Columns.Add("Category");
            _itemsTable.Columns.Add("Type");
            _itemsTable.Columns.Add("Amount", typeof(decimal));
            _itemsTable.Columns.Add("Percent", typeof(decimal));

            foreach (var inc in incomeGroups)
            {
                var pct = _totalIncome > 0 ? Math.Round((inc.Amount / _totalIncome) * 100m, 2) : 0m;
                _itemsTable.Rows.Add(inc.Category, "Income", inc.Amount, pct);
            }
            foreach (var sp in spendingGroups)
            {
                var pct = _totalSpending > 0 ? Math.Round((sp.Amount / _totalSpending) * 100m, 2) : 0m;
                _itemsTable.Rows.Add(sp.Category, "Spending", sp.Amount, pct);
            }
        }

        private static decimal ToDecimal(object o)
        {
            if (o == null || o == DBNull.Value) return 0m;
            if (decimal.TryParse(o.ToString(), out var d)) return d;
            return 0m;
        }

        private void BindUi()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Total Income: ₱{_totalIncome:N2}");
            sb.AppendLine($"Total Spending: ₱{_totalSpending:N2}");
            sb.AppendLine($"Net Savings: ₱{_netSavings:N2}");
            sb.AppendLine($"Savings Rate: {_savingsRate:N2}%");
            _lblSummary.Text = sb.ToString();
        }

        private string ExtractTips(string report)
        {
            if (string.IsNullOrWhiteSpace(report)) return "";
            try
            {
                // Try to find common headings for recommendations/tips
                string[] markers = new[] {
                    "## AI Recommendations", "## Recommendations", "## Tips",
                    "### AI Recommendations", "### Recommendations", "### Tips"
                };

                foreach (var m in markers)
                {
                    int idx = report.IndexOf(m, StringComparison.OrdinalIgnoreCase);
                    if (idx >= 0)
                    {
                        // Return section from marker to end or until next top-level heading
                        string tail = report.Substring(idx).Trim();
                        // Stop at next heading if present
                        var lines = tail.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                        var sb = new StringBuilder();
                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (i > 0 && lines[i].StartsWith("#")) break;
                            sb.AppendLine(lines[i]);
                        }
                        return sb.ToString().Trim();
                    }
                }

                // Fallback: return last 30 lines as tips
                var all = report.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                var slice = all.Length > 30 ? all.Skip(all.Length - 30) : all.AsEnumerable();
                return string.Join(Environment.NewLine, slice);
            }
            catch { return report; }
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                var title = _lblTitle.Text;
                var items = _itemsTable.AsEnumerable()
                    .Select(r => (
                        category: r.Field<string>("Category"),
                        itemType: r.Field<string>("Type"),
                        amount: r.Field<decimal>("Amount"),
                        percent: r.Field<decimal>("Percent")
                    ));

                int id = AiReportRepository.InsertReport(
                    _username,
                    title,
                    _reportText,
                    _totalIncome,
                    _totalSpending,
                    _netSavings,
                    _savingsRate,
                    items
                );

                MessageBox.Show($"Report saved (ID: {id}).");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save failed: {ex.Message}");
            }
        }
    }
}
