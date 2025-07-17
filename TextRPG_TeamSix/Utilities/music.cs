using System;
using System.Media;

namespace TextRPG_TeamSix.Utils
{
    internal static class SoundManager
    {
        public static void Play(string path)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(path);
                player.Play(); // PlaySync() 쓰면 기다림
            }
            catch (Exception e)
            {
                Console.WriteLine($"[사운드 재생 오류]: {e.Message}");
            }
        }

        public static void PlaySync(string path)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(path);
                player.PlaySync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[사운드 재생 오류]: {e.Message}");
            }
        }
    }
}
