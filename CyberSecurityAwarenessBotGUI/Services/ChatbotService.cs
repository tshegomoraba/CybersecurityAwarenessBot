using CyberSecurityAwarenessBotGUI.Models;
using System;

namespace CyberSecurityAwarenessBotGUI.Services
{
    public class ChatbotService
    {
        private readonly UserProfile _userProfile;
        private readonly MemoryService _memoryService;
        private readonly SentimentService _sentimentService;
        private readonly ResponseService _responseService;
        public readonly TaskService TaskService;
        public readonly ActivityLogService LogService;

        private string _currentTopic = string.Empty;

        // Actions that the MainForm can subscribe to
        public event Action? OnOpenTaskManager;
        public event Action? OnOpenQuiz;

        public ChatbotService()
        {
            _userProfile = new UserProfile();
            _memoryService = new MemoryService(_userProfile);
            _sentimentService = new SentimentService();
            _responseService = new ResponseService();
            TaskService = new TaskService();
            LogService = new ActivityLogService();
        }

        public string StartBot()
        {
            return "Hello! Welcome to the Cybersecurity Awareness Bot.\n" +
                   "Type 'my name is [name]' to get started.\n\n" +
                   "Commands: 'open tasks', 'start quiz', 'show activity log'";
        }

        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something so I can assist you.";

            string lower = input.Trim().ToLower();

            // ── Activity log ──────────────────────────────────────────────
            if (lower.Contains("show activity log") || lower.Contains("what have you done"))
            {
                return LogService.GetRecentLog();
            }

            // ── Name registration ─────────────────────────────────────────
            if (lower.StartsWith("my name is"))
            {
                string name = input.Substring(10).Trim();
                _memoryService.RememberUserName(name);
                LogService.Log($"User introduced themselves as '{name}'.");
                return $"Nice to meet you, {_memoryService.GetUserName()}! Ask me about passwords, phishing, scams, privacy, or safe browsing. " +
                       "You can also type 'open tasks' or 'start quiz'.";
            }

            // ── NLP: Task manager intent ──────────────────────────────────
            if (ContainsAny(lower, "open task", "manage task", "view task", "show task", "task list",
                                   "add task", "my tasks", "task manager", "remind me", "set reminder",
                                   "add a reminder", "enable 2fa", "two-factor", "review privacy",
                                   "update password", "check account"))
            {
                LogService.Log("User requested task manager via NLP.");
                OnOpenTaskManager?.Invoke();
                return "Opening your Task Manager now. You can add, complete, or delete cybersecurity tasks there.";
            }

            // ── NLP: Quiz intent ──────────────────────────────────────────
            if (ContainsAny(lower, "start quiz", "take quiz", "quiz me", "test me",
                                   "play quiz", "begin quiz", "open quiz", "game", "mini game"))
            {
                LogService.Log("User requested the cybersecurity quiz.");
                OnOpenQuiz?.Invoke();
                return "Launching the Cybersecurity Quiz! Good luck! 🎯";
            }

            // ── Favourite topic memory ────────────────────────────────────
            if (lower.Contains("i am interested in") || lower.Contains("i'm interested in"))
            {
                string topic = ExtractInterest(input);
                _memoryService.RememberFavoriteTopic(topic);
                _memoryService.RememberLastTopic(topic);
                _currentTopic = topic;
                LogService.Log($"User expressed interest in '{topic}'.");
                return $"Great, {_memoryService.GetUserName()}! I will remember that you are interested in {topic}. " +
                       GetTopicTip(topic);
            }

            if (lower.Contains("remember my topic"))
            {
                string favourite = _memoryService.GetFavoriteTopic();
                if (!string.IsNullOrWhiteSpace(favourite))
                    return $"I remember you are interested in {favourite}. Here is a tip: {GetTopicTip(favourite)}";
                return "I do not have a favourite topic saved yet. Say: 'I am interested in privacy'.";
            }

            if (lower.Contains("another tip") || lower.Contains("tell me more"))
            {
                if (!string.IsNullOrWhiteSpace(_currentTopic))
                    return $"Here is another tip about {_currentTopic}: {_responseService.GetRandomResponse(_currentTopic)}";
                return "Please ask me about a topic first — passwords, phishing, scams, privacy, or safe browsing.";
            }

            // ── Sentiment + topic detection ───────────────────────────────
            string sentiment = _sentimentService.DetectSentiment(input);
            string sentimentPrefix = _sentimentService.GetSentimentPrefix(sentiment);

            string generalResponse = _responseService.GetGeneralResponse(input, _memoryService.GetUserName());
            if (!string.IsNullOrWhiteSpace(generalResponse))
                return sentimentPrefix + generalResponse;

            string topicDetected = _responseService.GetTopicFromInput(input);
            if (!string.IsNullOrWhiteSpace(topicDetected))
            {
                _currentTopic = topicDetected;
                _memoryService.RememberLastTopic(topicDetected);
                LogService.Log($"User asked about topic: '{topicDetected}'.");
                return sentimentPrefix + _responseService.GetRandomResponse(topicDetected);
            }

            return "I didn't quite understand that. Try asking about passwords, phishing, scams, or type 'open tasks' / 'start quiz'.";
        }

        // ── NLP helper: checks multiple keyword phrases ───────────────────
        private bool ContainsAny(string input, params string[] keywords)
        {
            foreach (var kw in keywords)
            {
                if (input.Contains(kw)) return true;
            }
            return false;
        }

        private string ExtractInterest(string input)
        {
            string lower = input.ToLower();
            if (lower.Contains("privacy")) return "privacy";
            if (lower.Contains("password")) return "password";
            if (lower.Contains("phishing")) return "phishing";
            if (lower.Contains("scam")) return "scam";
            if (lower.Contains("browsing")) return "browsing";
            return "cybersecurity";
        }

        private string GetTopicTip(string topic) => _responseService.GetRandomResponse(topic);
    }
}