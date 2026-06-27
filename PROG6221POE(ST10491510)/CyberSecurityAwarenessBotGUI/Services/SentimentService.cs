namespace CyberSecurityAwarenessBotGUI.Services
{
    public class SentimentService
    {
        public string DetectSentiment(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("worried") || lowerInput.Contains("anxious") || lowerInput.Contains("scared"))
            {
                return "worried";
            }
            else if (lowerInput.Contains("confused") || lowerInput.Contains("unclear") || lowerInput.Contains("puzzled"))
            {
                return "confused";
            }
            else if (lowerInput.Contains("frustrated") || lowerInput.Contains("annoyed") || lowerInput.Contains("irritated"))
            {
                return "frustrated";
            }
            else if (lowerInput.Contains("curious") || lowerInput.Contains("interested") || lowerInput.Contains("wondering"))
            {
                return "curious";
            }
            return string.Empty;
        }

        public string GetSentimentPrefix(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    return "It is understandable to feel worried. Cyber threats can be stressful, but learning simple safety steps can help you stay protected. ";
                case "confused":
                    return "No problem. I will explain it in a simple way. ";
                case "frustrated":
                    return "I understand that cybersecurity can feel frustrating sometimes. Let us take it step by step. ";
                case "curious":
                    return "That is a good topic to explore. Curiosity helps you become more aware online. ";
                default:
                    return string.Empty;
            }
        }
    }
}