using SpotifyClone.Models.OtherModels;

namespace SpotifyClone.Models
{
    public class DetailsTracksArtists
    {
        public ProfileDetails? ProfileDetails { get; set; }
        public spotifyTopArtists? TopArtists { get; set; }
        public spotifyTopTracks? TopTracks { get; set; }
    }
}
