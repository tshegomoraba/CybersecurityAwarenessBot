using System.Media;
using System.IO;

namespace CybersecurityAwarenessBot.Services
{
    public static class AudioPlayer
    {
        public static void PlayGreeting(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    SoundPlayer player = new SoundPlayer(filePath);
                    player.PlaySync();
                }
            }
            catch
            {
                // If audio fails, the program should continue running
            }
        }
    }
}