﻿namespace SpotifyClone.Models
{
    public class ExternalUrls
    {
        public string spotify { get; set; }
    }

    public class Followers
    {
        public object href { get; set; }
        public int total { get; set; }
    }

    public class Image
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Item
    {
        public ExternalUrls external_urls { get; set; }
        public Followers followers { get; set; }
        public List<string> genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class spotifyTopArtists
    {
        public List<Item> items { get; set; }
        public int total { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public string href { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
    }

}
