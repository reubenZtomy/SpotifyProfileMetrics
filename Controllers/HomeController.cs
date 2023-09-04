using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SpotifyClone.Models;
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

            return View(ProfileDetails);
        }
    }
}