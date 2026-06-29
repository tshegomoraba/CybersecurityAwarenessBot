using System;
using System.Drawing;
using System.Windows.Forms;
using CyberSecurityAwarenessBotGUI.Services;

namespace CyberSecurityAwarenessBotGUI.Forms
{
    public class QuizForm : Form
    {
        private readonly QuizService _quizService;
        private readonly ActivityLogService _logService;

        private Label lblProgress;
        private Label lblQuestion;
        private RadioButton[] radioOptions;
        private Button btnSubmit;
        private Label lblFeedback;

        public QuizForm(QuizService quizService, ActivityLogService logService)
        {
            _quizService = quizService;
            _quizService.Reset();
            _logService = logService;
            _logService.Log("Quiz started.");
            BuildUI();
            LoadQuestion();
        }

        private void BuildUI()
        {
            this.Text = "Cybersecurity Quiz";
            this.Size = new Size(620, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(230, 240, 250);

            var lblTitle = new Label
            {
                Text = "🔐 Cybersecurity Knowledge Quiz",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Bounds = new Rectangle(20, 10, 560, 35)
            };

            lblProgress = new Label
            {
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = false,
                Bounds = new Rectangle(20, 48, 560, 20)
            };

            lblQuestion = new Label
            {
                Font = new Font("Segoe UI", 11),
                AutoSize = false,
                Bounds = new Rectangle(20, 75, 560, 60),
                ForeColor = Color.Black
            };

            radioOptions = new RadioButton[4];
            for (int i = 0; i < 4; i++)
            {
                radioOptions[i] = new RadioButton
                {
                    Font = new Font("Segoe UI", 10),
                    AutoSize = false,
                    Bounds = new Rectangle(30, 145 + (i * 35), 540, 30)
                };
            }

            btnSubmit = new Button
            {
                Text = "Submit Answer",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                Bounds = new Rectangle(220, 295, 160, 35)
            };
            btnSubmit.Click += BtnSubmit_Click;

            lblFeedback = new Label
            {
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.DarkGreen,
                AutoSize = false,
                Bounds = new Rectangle(20, 340, 560, 55),
                TextAlign = ContentAlignment.TopLeft
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblProgress);
            this.Controls.Add(lblQuestion);
            foreach (var rb in radioOptions) this.Controls.Add(rb);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(lblFeedback);
        }

        private void LoadQuestion()
        {
            if (!_quizService.HasMoreQuestions)
            {
                ShowFinalScore();
                return;
            }

            var q = _quizService.GetCurrentQuestion();
            lblProgress.Text = $"Question {_quizService.CurrentQuestionNumber} of {_quizService.TotalQuestions}";
            lblQuestion.Text = q.Question;
            lblFeedback.Text = "";

            for (int i = 0; i < 4; i++)
            {
                if (i < q.Options.Count)
                {
                    radioOptions[i].Text = q.Options[i];
                    radioOptions[i].Checked = false;
                    radioOptions[i].Visible = true;
                }
                else
                {
                    radioOptions[i].Visible = false;
                }
            }

            btnSubmit.Text = "Submit Answer";
            btnSubmit.Enabled = true;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Find selected radio
            int selected = -1;
            for (int i = 0; i < 4; i++)
            {
                if (radioOptions[i].Visible && radioOptions[i].Checked)
                {
                    selected = i;
                    break;
                }
            }

            if (selected == -1)
            {
                MessageBox.Show("Please select an answer.", "No Answer Selected");
                return;
            }

            string feedback = _quizService.SubmitAnswer(selected);
            lblFeedback.Text = feedback;
            lblFeedback.ForeColor = feedback.StartsWith("✅") ? Color.DarkGreen : Color.Crimson;

            btnSubmit.Text = _quizService.HasMoreQuestions ? "Next Question" : "See Final Score";
            btnSubmit.Click -= BtnSubmit_Click;
            btnSubmit.Click += (s, ev) =>
            {
                btnSubmit.Click -= null;
                btnSubmit.Click += BtnSubmit_Click;
                LoadQuestion();
            };
        }

        private void ShowFinalScore()
        {
            string finalMsg = _quizService.GetFinalFeedback();
            _logService.Log($"Quiz completed. {finalMsg}");

            lblQuestion.Text = finalMsg;
            lblProgress.Text = "Quiz Complete!";
            lblFeedback.Text = "Close this window to return to the chatbot.";
            lblFeedback.ForeColor = Color.DarkBlue;

            foreach (var rb in radioOptions) rb.Visible = false;
            btnSubmit.Enabled = false;
        }
    }
}