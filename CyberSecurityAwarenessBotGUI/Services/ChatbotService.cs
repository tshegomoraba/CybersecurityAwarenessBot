using CyberSecurityAwarenessBotGUI.Models;

namespace CyberSecurityAwarenessBotGUI.Services
{
    public class ChatbotService
    {
        private readonly UserProfile _userProfile;
        private readonly MemoryService _memoryService;
        private readonly SentimentService _sentimentService;
        private readonly ResponseService _responseService;

        private string _currentTopic = string.Empty;

        public ChatbotService()
        {
            _userProfile = new UserProfile();
            _memoryService = new MemoryService(_userProfile);
            _sentimentService = new SentimentService();
            _responseService = new ResponseService();
        }

        public string StartBot()
        {
            return "Hello. Welcome to the Cyber security Awareness Bot. Please enter your name by typing: my name is yourname";
        }

        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Please type something so I can assist you.";
            }

            string lowerInput = input.Trim().ToLower();

            if (lowerInput.StartsWith("my name is"))
            {
                string name = input.Substring(10).Trim();
                _memoryService.RememberUserName(name);
                return $"Nice to meet you, {_memoryService.GetUserName()}. You can ask me about passwords, phishing, scams, privacy, or safe browsing.";
            }

            if (lowerInput.Contains("i am interested in") || lowerInput.Contains("i'm interested in"))
            {
                string topic = ExtractInterest(input);
                _memoryService.RememberFavoriteTopic(topic);
                _memoryService.RememberLastTopic(topic);
                _currentTopic = topic;

                return $"Great, {_memoryService.GetUserName()}. I will remember that you are interested in {topic}. {GetTopicTip(topic)}";
            }

            if (lowerInput.Contains("remember my topic"))
            {
                string favourite = _memoryService.GetFavoriteTopic();
                if (!string.IsNullOrWhiteSpace(favourite))
                {
                    return $"I remember that you are interested in {favourite}. Here is a useful tip: {GetTopicTip(favourite)}";
                }
                return "I do not have a favourite topic saved yet. You can say: I am interested in privacy.";
            }

            if (lowerInput.Contains("another tip") || lowerInput.Contains("tell me more"))
            {
                if (!string.IsNullOrWhiteSpace(_currentTopic))
                {
                    return $"Here is another tip about {_currentTopic}: {_responseService.GetRandomResponse(_currentTopic)}";
                }
                return "Please first ask me about a topic such as passwords, phishing, scams, privacy, or safe browsing.";
            }

            string sentiment = _sentimentService.DetectSentiment(input);
            string sentimentPrefix = _sentimentService.GetSentimentPrefix(sentiment);

            string generalResponse = _responseService.GetGeneralResponse(input, _memoryService.GetUserName());

            if (!string.IsNullOrWhiteSpace(generalResponse))
            {
                return sentimentPrefix + generalResponse;
            }

            string topicDetected = _responseService.GetTopicFromInput(input);
            if (!string.IsNullOrWhiteSpace(topicDetected))
            {
                _currentTopic = topicDetected;
                _memoryService.RememberLastTopic(topicDetected);
                return sentimentPrefix + _responseService.GetRandomResponse(topicDetected);
            }

            return "I am not sure I understand. Can you try rephrasing? You can ask about passwords, phishing, scams, privacy, or safe browsing.";
        }

        private string ExtractInterest(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("privacy")) return "privacy";
            if (lowerInput.Contains("password")) return "password";
            if (lowerInput.Contains("phishing")) return "phishing";
            if (lowerInput.Contains("scam")) return "scam";
            if (lowerInput.Contains("browsing")) return "browsing";

            return "cybersecurity";
        }

        private string GetTopicTip(string topic)
        {
            return _responseService.GetRandomResponse(topic);
        }
    }
}