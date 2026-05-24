//Name: Ndaedzo Given Tshiovhe
//Student Number: 10487456
//This is a Windows Forms-based cybersecurity chatbot designed to fulfill Part 2 and POE requirements.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CybersecurityChatbot
{
    // =========================================================================
    // CODE SEGMENT: CUSTOM EXCEPTION CLASSES (Error Handlers)
    // =========================================================================
    public class UnknownQueryException : Exception
    {
        public UnknownQueryException(string message) : base(message) { }
    }

    public class MissingContextException : Exception
    {
        public MissingContextException(string message) : base(message) { }
    }

    public class EmptyInputException : Exception
    {
        public EmptyInputException(string message) : base(message) { }
    }

    // =========================================================================
    // CODE SEGMENT: MAIN APPLICATION ENTRY
    // =========================================================================
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new ChatbotForm());
        }
    }

    // =========================================================================
    // CODE SEGMENT: GUI WINDOW & BUSINESS LOGIC
    // =========================================================================
    public class ChatbotForm : Form
    {
        // 1. Memory Store properties (Requirement 5)
        private string userName = "";
        private string favoriteTopic = "";
        private string lastDiscussedTopic = "";

        // 2. Generic Collection Database (Requirements 3 & 8)
        private Dictionary<string, List<string>> ResponseDatabase = null!;

        // 3. Delegate Definition (Requirement 2)
        public delegate string ResponseProcessor(string input);
        private ResponseProcessor processorDelegate;

        // UI Core Chat Controls
        private RichTextBox rtbChatLog;
        private TextBox txtUserInput;
        private Button btnSend;
        private Label lblHeader;

        // UI Interactive Tasks Controls
        private GroupBox grpChecklist;
        private CheckedListBox chkBoxListTips;
        private ProgressBar prgProgress;
        private Label lblProgressText;

        public ChatbotForm()
        {
            // Set up form parameters (Widened canvas to cleanly fit interactive side panel)
            this.Text = "CyberSentinel Security Terminal";
            this.Size = new Size(820, 580);
            this.MinimumSize = new Size(820, 580);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeUIComponents();
            InitializeResponseDatabase();

            // Bind processing core to our custom delegate signature
            processorDelegate = new ResponseProcessor(ProcessChatLogic);

            // Print Startup System Greet
            AppendColoredText("[System]: Security Tunnel Established.\r\n", Color.DarkGray);
            AppendColoredText("CyberSentinel: Standing by. What safety domain can I assist you with today?\r\n\r\n", Color.Cyan);
        }

        private void InitializeUIComponents()
        {
            // Custom Top Header Label
            lblHeader = new Label
            {
                Text = "CYBERSENTINEL SECURITY PROTOCOL",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(10, 92, 90),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(15, 15),
                Size = new Size(775, 40)
            };

            // Main output chat logs display window
            rtbChatLog = new RichTextBox
            {
                Location = new Point(15, 70),
                Size = new Size(435, 380),
                ReadOnly = true,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.FromArgb(0, 255, 204),
                Font = new Font("Consolas", 10),
                BorderStyle = BorderStyle.None,
                Padding = new Padding(10)
            };

            // Input field control panel
            txtUserInput = new TextBox
            {
                Location = new Point(15, 475),
                Size = new Size(330, 35),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(51, 51, 51),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtUserInput.KeyDown += TxtUserInput_KeyDown;

            // Trigger action push-button submission
            btnSend = new Button
            {
                Text = "SEND",
                Location = new Point(355, 474),
                Size = new Size(95, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(10, 92, 90),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Click += BtnSend_Click;

            // Interactive Module Panels
            grpChecklist = new GroupBox
            {
                Text = " Personal Threat Mitigation Tracker ",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(470, 70),
                Size = new Size(320, 435),
                FlatStyle = FlatStyle.Flat
            };

            chkBoxListTips = new CheckedListBox
            {
                Location = new Point(15, 30),
                Size = new Size(290, 310),
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.None,
                CheckOnClick = true
            };
            
            chkBoxListTips.Items.Add("Created a strong phrase password");
            chkBoxListTips.Items.Add("Enabled Multi-Factor Auth (MFA)");
            chkBoxListTips.Items.Add("Inspected incoming URL links cleanly");
            chkBoxListTips.Items.Add("Disabled tracking/geo-locations");
            chkBoxListTips.Items.Add("Ignored suspicious SMS parcel claims");
            chkBoxListTips.Items.Add("Configured backup recovery codes");

            chkBoxListTips.ItemCheck += ChkBoxListTips_ItemCheck;

            lblProgressText = new Label
            {
                Text = "System Defense Index: 0%",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(0, 255, 204),
                Location = new Point(15, 355),
                Size = new Size(290, 20)
            };

            prgProgress = new ProgressBar
            {
                Location = new Point(15, 385),
                Size = new Size(290, 23),
                Style = ProgressBarStyle.Continuous,
                BackColor = Color.FromArgb(51, 51, 51)
            };

            grpChecklist.Controls.AddRange(new Control[] { chkBoxListTips, lblProgressText, prgProgress });
            this.Controls.AddRange(new Control[] { lblHeader, rtbChatLog, txtUserInput, btnSend, grpChecklist });
        }

        private void InitializeResponseDatabase()
        {
            ResponseDatabase = new Dictionary<string, List<string>>
            {
                { "password", new List<string> {
                    "SECURITY BEST PRACTICE: Combine unrelated random words into a phrase like 'Blue-Mountain-Elephant-2026'.",
                    "TIP: Never reuse account recovery keys across multiple personal social media apps.",
                    "ALERT: Activate Multi-Factor authentication parameters wherever humanly possible."
                }},
                { "scam", new List<string> {
                    "THREAT ALERT: Attackers frequently execute courier parcel scams via SMS networks across South Africa.",
                    "PROTOCOL: Treat generic lottery wins or sudden profile validation requests as critical indicators of fraud.",
                    "TIP: If an administrative alert requires immediate wire transfers to protect funds, trace the authority manually."
                }},
                { "privacy", new List<string> {
                    "PRIVACY DOMAIN: Restrict localized application geo-location parameters inside your device settings.",
                    "TIP: Minimize digital footprint leakage by refusing options to store credit cards directly on public vendor webs.",
                    "PROTOCOL: Isolate personal identifiable data elements from default open visibility indexes."
                }}
            };
        }

        private void BtnSend_Click(object? sender, EventArgs e) => ExecuteMessageExchange();

        private void TxtUserInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                ExecuteMessageExchange();
            }
        }

        private void ExecuteMessageExchange()
        {
            string rawInput = txtUserInput.Text.Trim();
            
            try
            {
                if (string.IsNullOrEmpty(rawInput))
                {
                    throw new EmptyInputException("User data payload stream was completely empty.");
                }

                // Render out User entry stream
                AppendColoredText($"You: {rawInput}\r\n", Color.White);
                txtUserInput.Clear();

                // Query dynamic data mapping using the required delegate architecture
                string botOutput = processorDelegate(rawInput);

                // Render out System machine logic output answers
                AppendColoredText($"CyberSentinel: {botOutput}\r\n\r\n", Color.FromArgb(0, 255, 204));
            }
            catch (EmptyInputException)
            {
                // Smoothly ignore execution if user pushes send on an empty line
                return;
            }
            catch (Exception ex)
            {
                // Display custom error exceptions explicitly in red text within the log window
                AppendColoredText($"[EXCEPTION DETECTED]: {ex.Message}\r\n\r\n", Color.Red);
                txtUserInput.Clear();
            }
            
            // Auto-scroll update
            rtbChatLog.SelectionStart = rtbChatLog.Text.Length;
            rtbChatLog.ScrollToCaret();
        }

        // --- INTERACTIVE EVENT MECHANIC ---
        private void ChkBoxListTips_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            int itemsChecked = chkBoxListTips.CheckedItems.Count;
            
            // Explicitly qualified to avoid missing enum type errors
            if (e.NewValue == System.Windows.Forms.CheckState.Checked) itemsChecked++;
            if (e.NewValue == System.Windows.Forms.CheckState.Unchecked) itemsChecked--;

            int totalItems = chkBoxListTips.Items.Count;
            int percentage = (int)(((double)itemsChecked / totalItems) * 100);

            prgProgress.Value = percentage;
            lblProgressText.Text = $"System Defense Index: {percentage}%";
        }

        // --- CORE PROCESSOR METHOD MECHANIC ---
        private string ProcessChatLogic(string input)
        {
            string cleanInput = input.ToLower();

            // A. Identity Context & State Profiling
            if (cleanInput.StartsWith("my name is "))
            {
                userName = input.Substring(11).Trim();
                return $"Acknowledged. User identity vector set to: {userName}.";
            }
            if (cleanInput.Contains("interested in") || cleanInput.Contains("want to learn about"))
            {
                if (cleanInput.Contains("password")) favoriteTopic = "password";
                else if (cleanInput.Contains("scam")) favoriteTopic = "scam";
                else if (cleanInput.Contains("privacy")) favoriteTopic = "privacy";

                if (!string.IsNullOrEmpty(favoriteTopic))
                {
                    return $"Understood. Identity interest attribute tracking updated to target domain: '{favoriteTopic}'.";
                }
            }

            // B. Sentiment Pattern Extraction Engine
            string emotionalContext = "";
            if (cleanInput.Contains("worried") || cleanInput.Contains("scared") || cleanInput.Contains("afraid"))
            {
                emotionalContext = "It is completely understandable to feel concerned. Threat landscapes shift daily, but simple protocols reduce risks immensely. ";
            }
            else if (cleanInput.Contains("frustrated") || cleanInput.Contains("annoyed") || cleanInput.Contains("tired") || cleanInput.Contains("annoying"))
            {
                emotionalContext = "Acknowledged. Complexity fatigue is real. Let's keep this straightforward and actionable. ";
            }

            // C. Structural Continuity Strategy (Throws Context Error)
            if (cleanInput.Contains("explain more") || cleanInput.Contains("another tip") || cleanInput.Contains("tell me more"))
            {
                if (!string.IsNullOrEmpty(lastDiscussedTopic))
                {
                    return FetchRandomResponse(lastDiscussedTopic);
                }
                if (!string.IsNullOrEmpty(favoriteTopic))
                {
                    return $"Falling back to preference records. Topic '{favoriteTopic}':\r\n" + FetchRandomResponse(favoriteTopic);
                }
                
                throw new MissingContextException("Cannot fetch subsequent safety nodes because tracking index is null.");
            }

            // D. Target Core Topic Matrix Check
            foreach (string topicKey in ResponseDatabase.Keys)
            {
                if (cleanInput.Contains(topicKey))
                {
                    lastDiscussedTopic = topicKey;
                    return emotionalContext + FetchRandomResponse(topicKey);
                }
            }

            // E. Edge Case Safe Handlers (Throws Unknown Query Error)
            throw new UnknownQueryException("The provided message package data format does not match known knowledge bases.");
        }

        private string FetchRandomResponse(string key)
        {
            var collection = ResponseDatabase[key];
            Random rand = new Random();
            return collection[rand.Next(collection.Count)];
        }

        private void AppendColoredText(string text, Color color)
        {
            rtbChatLog.SelectionStart = rtbChatLog.Text.Length;
            rtbChatLog.SelectionLength = 0;
            rtbChatLog.SelectionColor = color;
            rtbChatLog.AppendText(text);
            rtbChatLog.SelectionColor = rtbChatLog.ForeColor;
        }
    }
}