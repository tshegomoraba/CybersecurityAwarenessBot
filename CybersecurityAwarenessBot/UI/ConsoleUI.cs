using System;
using System.Threading;

namespace CybersecurityAwarenessBot.UI
{
    public static class ConsoleUI
    {
        public static void DisplayHeader()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("====================================================");
            Console.WriteLine("        CYBERSECURITY AWARENESS BOT");
            Console.WriteLine("====================================================");

            Console.WriteLine(@"
        ░█████╗░██╗░░░██╗██████╗░██████╗░███████╗██████╗░
        ██╔══██╗██║░░░██║██╔══██╗██╔══██╗██╔════╝██╔══██╗
        ██║░░╚═╝██║░░░██║██████╔╝██████╔╝█████╗░░██████╔╝
        ██║░░██╗██║░░░██║██╔═══╝░██╔══██╗██╔══╝░░██╔══██╗
        ╚█████╔╝╚██████╔╝██║░░░░░██║░░██║███████╗██║░░██║
        ░╚════╝░░╚═════╝░╚═╝░░░░░╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝

              🔐 CYBERSECURITY AWARENESS SYSTEM 🔐
        ───────────────────────────────────────────────
           PROTECT • EDUCATE • DEFEND • AWARENESS
        ───────────────────────────────────────────────
        ");

            Console.ResetColor();
        }

        // ================= LOGIN SCREEN =================
        public static void ShowLoginScreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            TypeText("\n[ SYSTEM BOOTING... ]");
            LoadingBar();

            TypeText("\nEnter Username: ");
        }

        // ================= TYPING EFFECT =================
        public static void TypeText(string text, int delay = 20)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        // ================= LOADING BAR =================
        public static void LoadingBar(int total = 30)
        {
            Console.Write("\n[");
            for (int i = 0; i < total; i++)
            {
                Console.Write("█");
                Thread.Sleep(50);
            }
            Console.WriteLine("] 100%");
        }

        // ================= BOT MESSAGE =================
        public static void WriteBotMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            TypeText($"Bot: {message}", 10);
            Console.ResetColor();
        }

        // ================= USER PROMPT =================
        public static void WriteUserPrompt(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{userName}: ");
            Console.ResetColor();
        }

        // ================= SECURITY ALERT =================
        public static void ShowAlert(string alertMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\n⚠️  SECURITY ALERT ⚠️");
            TypeText(alertMessage, 15);

            Console.ResetColor();
        }
    }
}