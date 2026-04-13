using CybersecurityAwarenessBot.Models;
using CybersecurityAwarenessBot.Services;
using CybersecurityAwarenessBot.UI;

namespace CybersecurityAwarenessBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";

            ConsoleUI.DisplayHeader();
            AudioPlayer.PlayGreeting("Assets/greeting.wav.wav");

            UserProfile user = new UserProfile();
            ChatbotService chatbot = new ChatbotService();

            ConsoleUI.WriteBotMessage("Welcome to your Cybersecurity Awareness Assistant.");
            ConsoleUI.WriteBotMessage("I'm here to help you understand how to stay safe online in a simple and interactive way.");

            ConsoleUI.WriteBotMessage("Before we begin, please tell me your name:");
            user.Name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(user.Name))
            {
                ConsoleUI.WriteBotMessage("I didn't catch your name. Please type it so we can continue:");
                user.Name = Console.ReadLine();
            }

            ConsoleUI.WriteBotMessage($"Great to meet you, {user.Name}.");
            ConsoleUI.WriteBotMessage("You can ask me about topics like:");
            ConsoleUI.WriteBotMessage("password safety, phishing, malware, scams, safe browsing, and suspicious links.");
            ConsoleUI.WriteBotMessage("Whenever you're ready, just type your question.");
            ConsoleUI.WriteBotMessage("Type 'exit' anytime to end the session.");

            bool running = true;

            while (running)
            {
                ConsoleUI.WriteUserPrompt(user.Name);
                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    ConsoleUI.WriteBotMessage("It looks like you didn't type anything. Please try again with a question or topic.");
                    continue;
                }

                string cleanInput = userInput.Trim().ToLower();

                // 🔍 Keyword Expansion (makes bot smarter)
                cleanInput = ExpandKeywords(cleanInput);

                switch (cleanInput)
                {
                    case "exit":
                        ConsoleUI.WriteBotMessage($"Thanks for chatting, {user.Name}. Remember to stay safe online and think before you click.");
                        running = false;
                        break;

                    default:
                        string response = chatbot.GetResponse(cleanInput, user.Name);

                        ConsoleUI.WriteBotMessage(response);

                        // 🎯 Quiz interaction trigger (simple detection)
                        if (response.Contains("Quiz"))
                        {
                            HandleQuiz();
                        }

                        break;
                }
            }
        }

        // 🧠 Keyword Expansion (simple AI behavior simulation)
        static string ExpandKeywords(string input)
        {
            if (input.Contains("hack") || input.Contains("hacked") || input.Contains("security breach"))
                return "malware phishing scam";

            if (input.Contains("email") || input.Contains("message"))
                return "phishing";

            if (input.Contains("safe") || input.Contains("internet") || input.Contains("browse"))
                return "safe browsing";

            if (input.Contains("virus") || input.Contains("infected"))
                return "malware";

            if (input.Contains("fake") || input.Contains("money") || input.Contains("prize"))
                return "scam";

            return input;
        }

        // 🎯 Simple Quiz Handler
        static void HandleQuiz()
        {
            ConsoleUI.WriteBotMessage("Please answer the quiz by typing A, B, C, or D:");

            string answer = Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(answer))
            {
                ConsoleUI.WriteBotMessage("No answer detected. Try again next time.");
                return;
            }

            ConsoleUI.WriteBotMessage("Answer received. Reviewing your response...");

            if (answer == "C")
            {
                ConsoleUI.WriteBotMessage("Correct! Well done — you understand this topic well.");
            }
            else
            {
                ConsoleUI.WriteBotMessage("That's not correct, but good try. Review the explanation and try again later.");
            }
        }
    }
}