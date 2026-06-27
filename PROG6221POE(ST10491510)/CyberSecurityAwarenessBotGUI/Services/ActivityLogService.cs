using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityAwarenessBotGUI.Services
{
    public class ActivityLogService
    {
        private readonly List<string> _log = new List<string>();

        public void Log(string action)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] {action}";
            _log.Add(entry);
        }

        public string GetRecentLog()
        {
            if (_log.Count == 0)
                return "No activity recorded yet.";

            // Show only the last 10 entries
            int startIndex = Math.Max(0, _log.Count - 10);
            var recent = _log.GetRange(startIndex, _log.Count - startIndex);

            var sb = new StringBuilder();
            sb.AppendLine("Here is a summary of recent actions:");
            for (int i = 0; i < recent.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {recent[i]}");
            }
            return sb.ToString();
        }
    }
}