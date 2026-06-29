using System;
using System.Drawing;
using System.Windows.Forms;
using CyberSecurityAwarenessBotGUI.Services;
using CyberSecurityAwarenessBotGUI.Forms;

namespace CyberSecurityAwarenessBotGUI
{
    public partial class Form1 : Form
    {
        private readonly ChatbotService _chatbotService;
        private readonly AudioPlayer _audioPlayer;
        private readonly QuizService _quizService;

        private TextBox txtChatDisplay;
        private TextBox txtUserInput;
        private Button btnSend;
        private Button btnClear;
        private Button btnTasks;
        private Button btnQuiz;
        private Label lblTitle;
        private Label lblAscii;
        private Label lblHelp;

        public Form1()
        {
            InitializeComponent();

            _chatbotService = new ChatbotService();
            _audioPlayer = new AudioPlayer();
            _quizService = new QuizService();

            _chatbotService.OnOpenTaskManager += OpenTaskManager;
            _chatbotService.OnOpenQuiz += OpenQuiz;

            BuildGui();
            StartApplication();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(960, 680);
            this.Name = "Form1";
            this.ResumeLayout(false);
        }

        private void BuildGui()
        {
            this.Text = "Cybersecurity Awareness Bot - Part 3";
            this.Width = 960;
            this.Height = 680;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 240, 250);

            lblTitle = new Label
            {
                Text = "Cyber Awareness Bot",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblTitle.SetBounds(20, 10, 900, 40);

            lblAscii = new Label
            {
                Text =
@"  ____  _  _  ____  _____  ____ 
 / ___)( \/ )(  _ \(  _  )(  _ \
( (__   \  /  ) _ < )(_)(  )   /
 \___)  (__) (____/(_____)(_)\_)",
                Font = new Font("Consolas", 8, FontStyle.Regular),
                ForeColor = Color.DarkSlateGray,
                AutoSize = false
            };
            lblAscii.SetBounds(40, 55, 860, 80);

            txtChatDisplay = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White
            };
            txtChatDisplay.SetBounds(30, 145, 620, 390);

            lblHelp = new Label
            {
                Text =
@"Commands:

open tasks
start quiz
show activity log

Topics:
password, phishing
scams, privacy
safe browsing

NLP Examples:
'Add task to enable 2FA'
'Remind me to update password'
'Quiz me on cybersecurity'",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Black,
                BackColor = Color.FromArgb(210, 225, 245),
                BorderStyle = BorderStyle.FixedSingle
            };
            lblHelp.SetBounds(665, 145, 265, 390);

            txtUserInput = new TextBox
            {
                Font = new Font("Segoe UI", 11)
            };
            txtUserInput.SetBounds(30, 550, 480, 35);
            txtUserInput.KeyDown += TxtUserInput_KeyDown;

            btnSend = new Button
            {
                Text = "Send",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White
            };
            btnSend.SetBounds(520, 550, 80, 35);
            btnSend.Click += BtnSend_Click;

            btnClear = new Button
            {
                Text = "Clear",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.Gray,
                ForeColor = Color.White
            };
            btnClear.SetBounds(610, 550, 80, 35);
            btnClear.Click += BtnClear_Click;

            btnTasks = new Button
            {
                Text = "Tasks",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White
            };
            btnTasks.SetBounds(700, 550, 100, 35);
            btnTasks.Click += (s, e) => OpenTaskManager();

            btnQuiz = new Button
            {
                Text = "Quiz",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.DarkOrange,
                ForeColor = Color.White
            };
            btnQuiz.SetBounds(810, 550, 100, 35);
            btnQuiz.Click += (s, e) => OpenQuiz();

            this.Controls.AddRange(new Control[]
            {
                lblTitle, lblAscii, txtChatDisplay, lblHelp,
                txtUserInput, btnSend, btnClear, btnTasks, btnQuiz
            });
        }

        private void StartApplication()
        {
            _audioPlayer.PlayGreeting();
            AddBotMessage(_chatbotService.StartBot());
        }

        private void BtnSend_Click(object sender, EventArgs e) => SendMessage();

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
            AddBotMessage("Chat cleared. You may continue.");
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

        private void OpenTaskManager()
        {
            var taskForm = new TaskForm(
                _chatbotService.TaskService,
                _chatbotService.LogService);
            taskForm.ShowDialog(this);
        }

        private void OpenQuiz()
        {
            var quizForm = new QuizForm(
                _quizService,
                _chatbotService.LogService);
            quizForm.ShowDialog(this);
        }

        private void AddUserMessage(string message)
        {
            txtChatDisplay.AppendText($"You: {message}{Environment.NewLine}");
        }

        private void AddBotMessage(string message)
        {
            txtChatDisplay.AppendText(
                $"Bot: {message}{Environment.NewLine}{Environment.NewLine}");
        }
    }
}