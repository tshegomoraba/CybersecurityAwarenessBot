using CyberSecurityAwarenessBotGUI.Models;

namespace CyberSecurityAwarenessBotGUI.Services
{
    public class MemoryService
    {
        private UserProfile _userProfile;
        private string _lastTopic = string.Empty;

        public MemoryService(UserProfile userProfile)
        {
            _userProfile = userProfile;
        }

        public void RememberUserName(string name)
        {
            _userProfile.UserName = name;
        }

        public string GetUserName()
        {
            return _userProfile.UserName;
        }

        public void RememberFavoriteTopic(string topic)
        {
            _userProfile.FavoriteTopic = topic;
        }

        public string GetFavoriteTopic()
        {
            return _userProfile.FavoriteTopic;
        }

        public void RememberLastTopic(string topic)
        {
            _lastTopic = topic;
        }

        public string GetLastTopic()
        {
            return _lastTopic;
        }
    }
}