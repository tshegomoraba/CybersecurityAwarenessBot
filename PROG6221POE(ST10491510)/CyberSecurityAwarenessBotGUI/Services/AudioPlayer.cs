using System;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;

namespace CyberSecurityAwarenessBotGUI.Services
{
    public class AudioPlayer
    {
        public void PlayGreeting()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return;
            }

            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");

                if (File.Exists(audioPath))
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.Play();
                    }
                }
            }
            catch
            {
                // The application must continue even if audio fails.
            }
        }
    }
}