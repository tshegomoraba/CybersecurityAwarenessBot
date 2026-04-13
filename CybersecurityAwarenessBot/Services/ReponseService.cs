using System;

namespace CybersecurityAwarenessBot.Services
{
    public static class ResponseService
    {
        private static Random random = new Random();

        public static string GetPasswordSafetyResponse()
        {
            string[] responses =
            {
                "Think of your password as your personal security lock. The stronger it is, the safer you are. A strong password should combine uppercase, lowercase, numbers, and symbols.\n\nFor example, instead of 'password123', use something like 'P@ssw0rd!2026'. Also, avoid reusing passwords across multiple accounts.\n\nQuiz: Which of the following is the strongest password?\nA) 123456\nB) myname2024\nC) P@ss!2026#Secure\nD) password"
                ,
                "Your password is your first line of defence online. Weak passwords are easy targets for hackers. Always create unique and complex passwords.\n\nExample: 'John123' is weak, but 'J0hn!Secure#99' is much stronger.\n\nQuiz: Why should you avoid using the same password for multiple accounts?\nA) It is harder to remember\nB) It saves time\nC) One breach can affect all accounts\nD) It looks unprofessional"
            };

            return responses[random.Next(responses.Length)];
        }

        public static string GetPhishingResponse()
        {
            string[] responses =
            {
                "Phishing is when attackers pretend to be trusted sources to steal your personal information. They often create urgency or fear.\n\nExample: An email saying 'Your bank account will be locked, click here now!' is likely fake.\n\nQuiz: What is a common sign of phishing?\nA) Friendly greeting\nB) Urgent request for personal info\nC) Proper grammar\nD) Official logo only"
                ,
                "Cybercriminals use phishing to trick you into revealing sensitive data like passwords or banking details.\n\nExample: A fake message from a delivery company asking you to 'confirm your address' via a link.\n\nQuiz: What should you do if you receive a suspicious email?\nA) Click immediately\nB) Ignore warning signs\nC) Verify the sender before acting\nD) Forward it to everyone"
            };

            return responses[random.Next(responses.Length)];
        }

        public static string GetSafeBrowsingResponse()
        {
            string[] responses =
            {
                "Safe browsing means being careful about where you click and what you download online.\n\nExample: Always check if a website uses 'https://' before entering personal information.\n\nQuiz: What does 'https://' indicate?\nA) A slow website\nB) A secure connection\nC) A fake website\nD) A broken link"
                ,
                "Browsing safely helps protect you from cyber threats. Avoid clicking unknown links and downloading from untrusted sources.\n\nExample: Downloading free software from random sites can expose your device to risks.\n\nQuiz: Which action is safest?\nA) Clicking random ads\nB) Downloading from unknown sites\nC) Using trusted websites only\nD) Ignoring security warnings"
            };

            return responses[random.Next(responses.Length)];
        }

        public static string GetMalwareResponse()
        {
            string[] responses =
            {
                "Malware is harmful software that can damage your device or steal your data.\n\nExample: Opening an unknown email attachment can install spyware or ransomware on your device.\n\nQuiz: What is ransomware?\nA) A game\nB) Software that speeds up your PC\nC) Malware that locks your files\nD) Antivirus software"
                ,
                "Malware includes viruses, worms, and spyware that secretly harm your system.\n\nExample: Installing fake apps from untrusted sources can infect your phone.\n\nQuiz: How can malware enter your device?\nA) Safe browsing\nB) Trusted apps only\nC) Suspicious downloads and links\nD) Updating your system"
            };

            return responses[random.Next(responses.Length)];
        }

        public static string GetScamResponse()
        {
            string[] responses =
            {
                "Online scams are tricks used to steal money or information by manipulating your emotions.\n\nExample: A message saying you've won money but need to pay a 'processing fee' first.\n\nQuiz: What is a red flag of a scam?\nA) Clear terms and conditions\nB) Requests for upfront payment\nC) Verified company details\nD) Official contact info"
                ,
                "Scammers often create offers that seem too good to be true.\n\nExample: A job offer promising high pay with little effort but asking for registration fees.\n\nQuiz: What should you do if an offer seems too good to be true?\nA) Accept immediately\nB) Share with friends\nC) Investigate and verify\nD) Ignore all offers"
            };

            return responses[random.Next(responses.Length)];
        }

        public static string GetSuspiciousLinksResponse()
        {
            string[] responses =
            {
                "Suspicious links are designed to trick you into visiting harmful websites.\n\nExample: A link like 'www.bank-login-secure123.com' may look real but is fake.\n\nQuiz: What should you check before clicking a link?\nA) Font style\nB) Link spelling and domain\nC) Colour of text\nD) Number of words"
                ,
                "Cyber attackers often disguise links to look legitimate.\n\nExample: Shortened links like 'bit.ly/xyz' can hide dangerous destinations.\n\nQuiz: What is the safest action?\nA) Click immediately\nB) Ignore link details\nC) Verify the link before clicking\nD) Share the link"
            };

            return responses[random.Next(responses.Length)];
        }
    }
}