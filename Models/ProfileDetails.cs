﻿namespace SpotifyClone.Models
{
    public class ProfileDetails
    {
        public string display_name { get; set; }
        public externalUrls external_urls { get; set; }
        public string href { get; set; }    
        public string id { get; set; }  
        public List<spotifyDP> images { get; set; }  
        public string type { get; set; }
        public string uri { get; set; }
        public spotifyProfileFollowers followers { get; set; }
        public string country { get; set; }
        public string product { get; set; }
        public ExContent explicit_content { get; set; }
        public string email { get; set; }
    }
}
