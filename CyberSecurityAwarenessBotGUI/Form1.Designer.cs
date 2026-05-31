using CyberSecurityAwarenessBotGUI.Services;
using CyberSecurityAwarenessBotGUI.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberSecurityAwarenessBotGUI
{
    public partial class MainForm : Form
    {
        private readonly ChatbotService _chatbotService;
        private readonly AudioPlayer _audioPlayer;

        private TextBox txtChatDisplay;
        private TextBox txtUserInput;
        private Button btnSend;
        private Button btnClear;
        private Label lblTitle;
        private Label lblAscii;
        private Label lblHelp;

        public MainForm()
        {
            InitializeComponent();

            _chatbotService = new ChatbotService();
            _audioPlayer = new AudioPlayer();

            BuildGui();
            StartApplication();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ClientSize = new Size(900, 650);
            Name = "MainForm";
            ResumeLayout(false);
        }

        private void BuildGui()
        {
            this.Text = "Cybersecurity Awareness Bot";
            this.Width = 900;
            this.Height = 650;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 240, 250);

            lblTitle = new Label();
            lblTitle.Text = "Cyber Awareness Bot";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.AutoSize = false;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.SetBounds(20, 10, 840, 40);

            lblAscii = new Label();
            lblAscii.Text =
@"  ____  _  _  ____  _____  ____ 
 / ___)( \/ )(  _ \(  _  )(  _ \
( (__   \  /  ) _ < )(_)(  )   /
 \___)  (__) (____/(_____)(_)\_)
  ____  _____  ____ 
 (  _ \(  _  )(_  _)
  ) _ < )(_)(   )(  
 (____/(____/  (__) ";
            lblAscii.Font = new Font("Consolas", 8, FontStyle.Regular);
            lblAscii.ForeColor = Color.DarkSlateGray;
            lblAscii.AutoSize = false;
            lblAscii.SetBounds(40, 55, 800, 120);

            txtChatDisplay = new TextBox();
            txtChatDisplay.Multiline = true;
            txtChatDisplay.ReadOnly = true;
            txtChatDisplay.ScrollBars = ScrollBars.Vertical;
            txtChatDisplay.Font = new Font("Segoe UI", 10);
            txtChatDisplay.BackColor = Color.White;
            txtChatDisplay.SetBounds(30, 185, 600, 330);

            lblHelp = new Label();
            lblHelp.Text =
@"Topics you can ask about:

Password safety
Phishing
Scams
Privacy
Safe browsing

Try:
my name is Desire
I am worried about scams
Tell me about privacy
Give me another tip
Remember my topic";
            lblHelp.Font = new Font("Segoe UI", 10);
            lblHelp.ForeColor = Color.Black;
            lblHelp.BackColor = Color.FromArgb(210, 225, 245);
            lblHelp.BorderStyle = BorderStyle.FixedSingle;
            lblHelp.SetBounds(650, 185, 200, 330);

            txtUserInput = new TextBox();
            txtUserInput.Font = new Font("Segoe UI", 11);
            txtUserInput.SetBounds(30, 535, 600, 35);
            txtUserInput.KeyDown += TxtUserInput_KeyDown;

            btnSend = new Button();
            btnSend.Text = "Send";
            btnSend.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSend.BackColor = Color.DarkBlue;
            btnSend.ForeColor = Color.White;
            btnSend.SetBounds(650, 535, 90, 35);
            btnSend.Click += BtnSend_Click;

            btnClear = new Button();
            btnClear.Text = "Clear";
            btnClear.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnClear.BackColor = Color.Gray;
            btnClear.ForeColor = Color.White;
            btnClear.SetBounds(760, 535, 90, 35);
            btnClear.Click += BtnClear_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblAscii);
            this.Controls.Add(txtChatDisplay);
            this.Controls.Add(lblHelp);
            this.Controls.Add(txtUserInput);
            this.Controls.Add(btnSend);
            this.Controls.Add(btnClear);
        }

        private void StartApplication()
        {
            _audioPlayer.PlayGreeting();
            AddBotMessage(_chatbotService.StartBot());
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
                e.SuppressKeyPress = true;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtChatDisplay.Clear();
            AddBotMessage("Chat cleared. You may continue asking cybersecurity questions.");
        }

        private void SendMessage()
        {
            string input = txtUserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                AddBotMessage("Please enter a message first.");
                return;
            }

            AddUserMessage(input);
            string response = _chatbotService.ProcessInput(input);
            AddBotMessage(response);

            txtUserInput.Clear();
            txtUserInput.Focus();
        }

        private void AddUserMessage(string message)
        {
            txtChatDisplay.AppendText($"You: {message}{Environment.NewLine}");
        }

        private void AddBotMessage(string message)
        {
            txtChatDisplay.AppendText($"Bot: {message}{Environment.NewLine}{Environment.NewLine}");
        }
    }
}