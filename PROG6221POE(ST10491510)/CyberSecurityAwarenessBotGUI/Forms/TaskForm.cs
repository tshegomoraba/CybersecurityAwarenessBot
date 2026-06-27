using System;
using System.Drawing;
using System.Windows.Forms;
using CyberSecurityAwarenessBotGUI.Services;
using CyberSecurityAwarenessBotGUI.Models;

namespace CyberSecurityAwarenessBotGUI.Forms
{
    public class TaskForm : Form
    {
        private readonly TaskService _taskService;
        private readonly ActivityLogService _logService;

        private DataGridView dgvTasks;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private DateTimePicker dtpReminder;
        private CheckBox chkSetReminder;
        private Button btnAdd;
        private Button btnComplete;
        private Button btnDelete;
        private Button btnRefresh;

        public TaskForm(TaskService taskService, ActivityLogService logService)
        {
            _taskService = taskService;
            _logService = logService;
            BuildUI();
            LoadTasks();
        }

        private void BuildUI()
        {
            this.Text = "Task Assistant - Cybersecurity Tasks";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(230, 240, 250);

            var lblHeading = new Label
            {
                Text = "Cybersecurity Task Manager",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Bounds = new Rectangle(20, 10, 740, 40)
            };

            var lblTitle = new Label { Text = "Task Title:", Bounds = new Rectangle(20, 60, 100, 25) };
            txtTitle = new TextBox { Bounds = new Rectangle(120, 58, 300, 25) };

            var lblDesc = new Label { Text = "Description:", Bounds = new Rectangle(20, 95, 100, 25) };
            txtDescription = new TextBox { Bounds = new Rectangle(120, 93, 300, 25) };

            chkSetReminder = new CheckBox
            {
                Text = "Set Reminder Date",
                Bounds = new Rectangle(20, 128, 150, 25)
            };
            chkSetReminder.CheckedChanged += (s, e) => dtpReminder.Enabled = chkSetReminder.Checked;

            dtpReminder = new DateTimePicker
            {
                Bounds = new Rectangle(175, 126, 200, 25),
                Enabled = false,
                MinDate = DateTime.Today
            };

            btnAdd = new Button
            {
                Text = "Add Task",
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Bounds = new Rectangle(20, 165, 100, 30)
            };
            btnAdd.Click += BtnAdd_Click;

            btnRefresh = new Button
            {
                Text = "Refresh",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Bounds = new Rectangle(130, 165, 100, 30)
            };
            btnRefresh.Click += (s, e) => LoadTasks();

            btnComplete = new Button
            {
                Text = "Mark Complete",
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Bounds = new Rectangle(240, 165, 120, 30)
            };
            btnComplete.Click += BtnComplete_Click;

            btnDelete = new Button
            {
                Text = "Delete Task",
                BackColor = Color.Crimson,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Bounds = new Rectangle(370, 165, 100, 30)
            };
            btnDelete.Click += BtnDelete_Click;

            dgvTasks = new DataGridView
            {
                Bounds = new Rectangle(20, 210, 740, 340),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            this.Controls.AddRange(new Control[]
            {
                lblHeading, lblTitle, txtTitle, lblDesc, txtDescription,
                chkSetReminder, dtpReminder, btnAdd, btnRefresh,
                btnComplete, btnDelete, dgvTasks
            });
        }

        private void LoadTasks()
        {
            try
            {
                var tasks = _taskService.GetAllTasks();
                dgvTasks.DataSource = tasks;

                // Hide raw Id column from the user view but keep it for selection
                if (dgvTasks.Columns.Contains("Id"))
                    dgvTasks.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a task title.", "Validation");
                return;
            }

            DateTime? reminder = chkSetReminder.Checked ? dtpReminder.Value.Date : (DateTime?)null;

            _taskService.AddTask(txtTitle.Text.Trim(), txtDescription.Text.Trim(), reminder);

            string logMsg = $"Task added: '{txtTitle.Text.Trim()}'";
            if (reminder.HasValue) logMsg += $" (Reminder: {reminder.Value:dd MMM yyyy})";
            _logService.Log(logMsg);

            txtTitle.Clear();
            txtDescription.Clear();
            chkSetReminder.Checked = false;
            LoadTasks();
        }

        private void BtnComplete_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["Id"].Value);
            string title = dgvTasks.SelectedRows[0].Cells["Title"].Value?.ToString() ?? "";

            _taskService.MarkCompleted(id);
            _logService.Log($"Task marked complete: '{title}'");
            LoadTasks();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["Id"].Value);
            string title = dgvTasks.SelectedRows[0].Cells["Title"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show($"Delete task '{title}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                _taskService.DeleteTask(id);
                _logService.Log($"Task deleted: '{title}'");
                LoadTasks();
            }
        }
    }
}