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
                string audioPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");

                if (File.Exists(audioPath))
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.Play();
                    }
                }
                else
                {
                    Console.WriteLine($"Audio file not found at: {audioPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audio playback error: {ex.Message}");
            }
        }

        public void PlayQuizStart()
        {
            PlaySound("greeting.wav");
        }

        public void PlayTaskAdded()
        {
            PlaySound("greeting.wav");
        }

        private void PlaySound(string fileName)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return;

            try
            {
                string audioPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Assets", fileName);

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
                // App continues even if audio fails
            }
        }
    }
}