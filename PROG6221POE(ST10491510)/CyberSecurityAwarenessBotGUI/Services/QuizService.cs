using System.Collections.Generic;

namespace CyberSecurityAwarenessBotGUI.Services
{
    public class QuizQuestion
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; } = string.Empty;
        public bool IsTrueFalse { get; set; }
    }

    public class QuizService
    {
        private readonly List<QuizQuestion> _questions;
        private int _currentIndex = 0;
        private int _score = 0;

        public QuizService()
        {
            _questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report it as phishing", "D) Ignore it" },
                    CorrectIndex = 2,
                    Explanation = "Always report phishing emails to help prevent scams for others."
                },
                new QuizQuestion
                {
                    Question = "True or False: You should use the same password for all your accounts.",
                    Options = new List<string> { "A) True", "B) False" },
                    CorrectIndex = 1,
                    IsTrueFalse = true,
                    Explanation = "Using the same password means one breach exposes all your accounts."
                },
                new QuizQuestion
                {
                    Question = "What does HTTPS mean in a website URL?",
                    Options = new List<string> { "A) The site is popular", "B) The connection is encrypted and secure", "C) The site is government-owned", "D) The site loads faster" },
                    CorrectIndex = 1,
                    Explanation = "HTTPS means data between you and the site is encrypted."
                },
                new QuizQuestion
                {
                    Question = "True or False: It is safe to share your OTP with a bank employee who calls you.",
                    Options = new List<string> { "A) True", "B) False" },
                    CorrectIndex = 1,
                    IsTrueFalse = true,
                    Explanation = "Legitimate banks will NEVER ask for your OTP over the phone."
                },
                new QuizQuestion
                {
                    Question = "Which of these is a strong password?",
                    Options = new List<string> { "A) password123", "B) John1990", "C) T!g3r$ecure#99", "D) abcdef" },
                    CorrectIndex = 2,
                    Explanation = "Strong passwords combine uppercase, lowercase, numbers, and symbols."
                },
                new QuizQuestion
                {
                    Question = "What is social engineering?",
                    Options = new List<string> { "A) Building social media apps", "B) Manipulating people into revealing confidential information", "C) Engineering social networks", "D) A type of firewall" },
                    CorrectIndex = 1,
                    Explanation = "Social engineering tricks people psychologically rather than hacking systems."
                },
                new QuizQuestion
                {
                    Question = "True or False: Public Wi-Fi is always safe to use for online banking.",
                    Options = new List<string> { "A) True", "B) False" },
                    CorrectIndex = 1,
                    IsTrueFalse = true,
                    Explanation = "Public Wi-Fi can be monitored by attackers. Use a VPN or mobile data for banking."
                },
                new QuizQuestion
                {
                    Question = "What is two-factor authentication (2FA)?",
                    Options = new List<string> { "A) Logging in with two passwords", "B) An extra security step beyond just a password", "C) Having two email accounts", "D) A type of antivirus" },
                    CorrectIndex = 1,
                    Explanation = "2FA adds an extra layer of security such as an OTP sent to your phone."
                },
                new QuizQuestion
                {
                    Question = "Which action helps protect your privacy on social media?",
                    Options = new List<string> { "A) Share your location in every post", "B) Accept all friend requests", "C) Set your profile to private", "D) Use your full name as username" },
                    CorrectIndex = 2,
                    Explanation = "A private profile limits who can see your personal information."
                },
                new QuizQuestion
                {
                    Question = "True or False: Clicking unknown links in SMS messages is generally safe.",
                    Options = new List<string> { "A) True", "B) False" },
                    CorrectIndex = 1,
                    IsTrueFalse = true,
                    Explanation = "Unknown SMS links are a common phishing method known as smishing."
                },
                new QuizQuestion
                {
                    Question = "What should you do when a website pop-up says you have a virus and must call a number?",
                    Options = new List<string> { "A) Call the number immediately", "B) Click the pop-up to fix the virus", "C) Close the browser and run your real antivirus", "D) Share the page with friends" },
                    CorrectIndex = 2,
                    Explanation = "This is a scareware tactic. Always use your installed antivirus software."
                },
                new QuizQuestion
                {
                    Question = "How often should you update your software and operating system?",
                    Options = new List<string> { "A) Never, updates break things", "B) Only when forced to", "C) Regularly, as updates patch security vulnerabilities", "D) Once a year is enough" },
                    CorrectIndex = 2,
                    Explanation = "Regular updates close security gaps that attackers exploit."
                }
            };
        }

        public bool HasMoreQuestions => _currentIndex < _questions.Count;
        public int Score => _score;
        public int TotalQuestions => _questions.Count;
        public int CurrentQuestionNumber => _currentIndex + 1;

        public QuizQuestion GetCurrentQuestion() => _questions[_currentIndex];

        public string SubmitAnswer(int selectedIndex)
        {
            var q = _questions[_currentIndex];
            bool correct = selectedIndex == q.CorrectIndex;
            if (correct) _score++;
            _currentIndex++;

            string result = correct ? "✅ Correct! " : $"❌ Incorrect. The correct answer was: {q.Options[q.CorrectIndex]}. ";
            return result + q.Explanation;
        }

        public string GetFinalFeedback()
        {
            double percent = (double)_score / _questions.Count * 100;
            if (percent >= 80)
                return $"🎉 Great job! You're a cybersecurity pro! Score: {_score}/{_questions.Count}";
            else if (percent >= 50)
                return $"👍 Good effort! Keep learning to stay safe online. Score: {_score}/{_questions.Count}";
            else
                return $"📚 Keep learning to stay safe online! Score: {_score}/{_questions.Count}";
        }

        public void Reset()
        {
            _currentIndex = 0;
            _score = 0;
        }
    }
}
