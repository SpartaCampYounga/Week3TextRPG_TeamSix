using NAudio.Wave;
using System;
using System.IO;

namespace TextRPG_TeamSix.Utils
{
    public class LoopStream : WaveStream
    {
        private readonly WaveStream sourceStream;

        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
        }

        public override WaveFormat WaveFormat => sourceStream.WaveFormat;
        public override long Length => long.MaxValue;

        public override long Position
        {
            get => sourceStream.Position;
            set => sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int read = sourceStream.Read(buffer, offset, count);
            if (read == 0)
            {
                sourceStream.Position = 0;
                read = sourceStream.Read(buffer, offset, count);
            }
            return read;
        }
    }
    internal static class SoundManager
    {
        private static IWavePlayer outputDevice;
        private static AudioFileReader audioFile;

        public static void Play(string fileName, bool loop = false)
        {
            Stop();

            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound", fileName);

                audioFile = new AudioFileReader(path);
                outputDevice = new WaveOutEvent();

                if (loop)
                {
                    var loopStream = new LoopStream(audioFile);
                    outputDevice.Init(loopStream);
                }
                else
                {
                    outputDevice.Init(audioFile);
                }

                outputDevice.Play();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[음악 재생 오류]: {e.Message}");
            }
        }

        public static void Stop()
        {
            outputDevice?.Stop();
            outputDevice?.Dispose();
            outputDevice = null;

            audioFile?.Dispose();
            audioFile = null;
        }
    }
}
