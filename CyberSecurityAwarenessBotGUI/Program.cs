using System;
using System.Windows.Forms;

namespace CyberSecurityAwarenessBotGUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Change 'MainForm' to 'Form1' if you haven't renamed the class inside Form1.cs
            Application.Run(new MainForm());
        }
    }
}
