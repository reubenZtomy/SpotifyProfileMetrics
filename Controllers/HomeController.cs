using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SpotifyClone.Models;
using SpotifyClone.Models.OtherModels;
using System.Diagnostics;
using System.Web;
using Method = RestSharp.Method;

namespace SpotifyClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Profile(string access_token)
        {
            if(access_token!=null)
            {
                HttpContext.Session.SetString("Accesstoken", access_token);
            }

            var options = new RestClientOptions("https://api.spotify.com/v1")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/me", Method.Get);
            request.AddHeader("Accept", "text/plain");
            request.AddHeader("Authorization", "Bearer " + access_token);
            RestResponse response = await client.ExecuteAsync(request);
            ProfileDetails ProfileDetails = JsonConvert.DeserializeObject<ProfileDetails>(response.Content);

            ProfileDetails.href = "https://open.spotify.com/user/" + ProfileDetails.id;
            

            var allDetails = new DetailsTracksArtists();

            allDetails.ProfileDetails = ProfileDetails;
            allDetails.TopArtists = await GetTopArtists(); 
            allDetails.TopTracks = await GetTopTracks(); 


            return View(allDetails);
        }

        public async Task<string> GetProfileUrl(string profileUrl)
        {

            string access_token = HttpContext.Session.GetString("Accesstoken");
            var options = new RestClientOptions("https://api.spotify.com/v1")
            {
                MaxTimeout = -1,
            };
            var urlClient = new RestClient(options);
            var urlRequest = new RestRequest(profileUrl, Method.Get);
            urlRequest.AddHeader("Accept", "text/plain");
            urlRequest.AddHeader("Authorization", "Bearer "+ access_token);
            RestResponse response = await urlClient.ExecuteAsync(urlRequest);
            ProfileDetails ProfileDetails = JsonConvert.DeserializeObject<ProfileDetails>(response.Content);

            return ("True");
        }
        
        
        public async Task<spotifyTopArtists> GetTopArtists()
        {
            string access_token = HttpContext.Session.GetString("Accesstoken");
            var options = new RestClientOptions("https://api.spotify.com/v1")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/me/top/artists", Method.Get);
            request.AddHeader("Authorization", "Bearer "+ access_token);
            RestResponse response = await client.ExecuteAsync(request);
            spotifyTopArtists TopArtists = JsonConvert.DeserializeObject<spotifyTopArtists>(response.Content);

            return TopArtists;
        }

        public async Task<spotifyTopTracks> GetTopTracks()
        {
            string access_token = HttpContext.Session.GetString("Accesstoken");
            var options = new RestClientOptions("https://api.spotify.com/v1")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/me/top/tracks", Method.Get);
            request.AddHeader("Authorization", "Bearer " + access_token);
            RestResponse response = await client.ExecuteAsync(request);
            spotifyTopTracks TopTracks = JsonConvert.DeserializeObject<spotifyTopTracks>(response.Content);

            return TopTracks;
        }
    }
}