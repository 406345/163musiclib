using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace lib163music
{
    public class MusicPlayer
    {
        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;

        public MusicPlayer()
        {
            waveOutDevice = new WaveOut();
        }

        public void Play(Model.Song song)
        { 
            if (song == null)
                return;

            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }

            var t = Mp3Frame.LoadFromStream(null);
          
            NAudio.Wave.AcmMp3FrameDecompressor dec = new AcmMp3FrameDecompressor(new Mp3WaveFormat(t.SampleRate, 2, t.FrameLength, t.BitRate));



            audioFileReader = new AudioFileReader(song.mp3Url);
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }

            waveOutDevice = new WaveOut();
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        public void Pause()
        {
            if (waveOutDevice == null)
            {
                return;
            }

            waveOutDevice.Pause();
        }

        public void Stop()
        {
            if (waveOutDevice == null)
            {
                return;
            }

            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }

            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }
        }

       
    }
}
