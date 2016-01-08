using lib163music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace demo
{
    class Program
    {
        static void Main(string[] args)
        {
            lib163music.Music163 music = new Music163();
            var sss = music.search("超科学", 100, 0, Music163.SearchType.Music);

            MusicPlayer mp = new MusicPlayer();
            mp.Play(sss[0]);

            Console.ReadLine();

        }
    }
} 