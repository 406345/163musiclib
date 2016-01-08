using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace lib163music
{
    public class Music163
    {

        public enum SearchType
        {
            Music = 1,
            Alumb = 10,
            Singer = 100,
            MusicList = 1000,
            User = 1002,
            MV = 1004,
            Lyric = 1006,
            Radio = 1009
        }

        WebClient createWeb()
        {
            WebClient wc = new WebClient();
            wc.Headers["Cookie"] = "appver=1.5.0.75771;";
            wc.Headers["Referer"] = "http://music.163.com/";
            wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            wc.Encoding = Encoding.UTF8;
            return wc;
        }
        string post(string url, string data)
        {
            var wc = createWeb();
            return wc.UploadString(url, "post", data);
        }

        string get(string url)
        {
            var wc = createWeb();
            return wc.DownloadString(url);
        } 

        string dict2string( Dictionary<string,string> dict)
        {
            string ret = "";
            foreach (var kv in dict)
            {
                ret += kv.Key+"="+ System.Web.HttpUtility.UrlEncode( kv.Value)+"&";
            }

            return ret.Substring(0,ret.Length - 1);
        }

        public List<Model.Song> search(string key , int count , int page , SearchType type)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("s", key);
            dict.Add("offset", page.ToString());
            dict.Add("limit", count.ToString());
            dict.Add("type", ((int)type).ToString());
            dict.Add("hlpretag", "");
            dict.Add("hlposttag", "");
            dict.Add("total", "true");

            string data = dict2string(dict);
            var json = post("http://music.163.com/api/search/pc", data);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.MusicMeta>(json);

            if( result == null )
            {
                return new List<Model.Song>();
            }

            return result.result.songs.ToList();
        }

        public void play()
        {

        }
    }
}
