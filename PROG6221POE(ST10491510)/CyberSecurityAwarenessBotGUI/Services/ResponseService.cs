using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBotGUI.Services
{
    public delegate string ResponseDelegate(string input);

    public class ResponseService
    {
        private readonly Dictionary<string, List<string>> _responses;
        private readonly Random _random;

        public ResponseService()
        {
            _random = new Random();
            _responses = new Dictionary<string, List<string>>
            {
                {
                    "password",
                    new List<string>
                    {
                        "Use strong passwords with uppercase letters, lowercase letters, numbers, and symbols.",
                        "Avoid using your name, birth date, or simple words as passwords.",
                        "Use different passwords for different accounts.",
                        "A password manager can help you store strong passwords safely."
                    }
                },
                {
                    "phishing",
                    new List<string>
                    {
                        "Phishing is when criminals pretend to be trusted organizations to steal your information.",
                        "Always check the sender's email address before clicking links.",
                        "Do not open attachments from unknown senders.",
                        "Be careful of messages that create fear or urgency."
                    }
                },
                {
                    "scam",
                    new List<string>
                    {
                        "Online scams often ask for money, passwords, banking details, or OTPs.",
                        "Never share your OTP with anyone, even if they claim to be from the bank.",
                        "If an offer sounds too good to be true, it may be a scam.",
                        "Always verify suspicious messages before responding."
                    }
                },
                {
                    "privacy",
                    new List<string>
                    {
                        "Protect your privacy by checking app permissions regularly.",
                        "Avoid sharing too much personal information online.",
                        "Use privacy settings on social media accounts.",
                        "Think carefully before posting your location or personal details."
                    }
                },
                {
                    "browsing",
                    new List<string>
                    {
                        "Use secure websites that begin with HTTPS.",
                        "Avoid downloading files from unknown websites.",
                        "Keep your browser updated.",
                        "Do not click suspicious pop-ups or adverts."
                    }
                }
            };
        }

        public string GetTopicFromInput(string input)
        {
            string lowerInput = input.ToLower();

            foreach (var topic in _responses.Keys)
            {
                if (lowerInput.Contains(topic))
                {
                    return topic;
                }
            }

            if (lowerInput.Contains("safe browsing") || lowerInput.Contains("browse"))
            {
                return "browsing";
            }

            return string.Empty;
        }

        public string GetRandomResponse(string topic)
        {
            if (_responses.ContainsKey(topic))
            {
                List<string> topicResponses = _responses[topic];
                int index = _random.Next(topicResponses.Count);
                return topicResponses[index];
            }
            return "I am not sure I understand. Can you try rephrasing?";
        }

        public string GetGeneralResponse(string input, string userName)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("how are you"))
            {
                return $"I am doing well, {userName}. I am ready to help you learn about cybersecurity.";
            }

            if (lowerInput.Contains("purpose"))
            {
                return "My purpose is to teach users about cybersecurity risks and safe online behaviour.";
            }

            if (lowerInput.Contains("what can i ask"))
            {
                return "You can ask me about passwords, phishing, scams, privacy, and safe browsing.";
            }

            if (lowerInput.Contains("help"))
            {
                return "Try asking about password safety, phishing, scams, privacy, or safe browsing.";
            }

            return string.Empty;
        }
    }
}