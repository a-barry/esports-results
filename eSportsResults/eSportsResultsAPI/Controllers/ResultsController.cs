using Common.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace eSports_Results_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly ILogger<ResultsController> _logger;
        private Services.ResultsService _resultsService { get; set; }

        private Services.SeriesService _seriesService { get; set; }

        public ResultsController(ILogger<ResultsController> logger, Services.ResultsService resultsService, Services.SeriesService seriesService)
        {
            _logger = logger;
            _resultsService = resultsService;
            _seriesService = seriesService;
        }

        [HttpGet("event/{id}")]
        public async Task<ResultsViewModel> GetEvent(string id)
        {
            var seriesConfig = await _seriesService.GetSeriesFromEventAsync(id);

            return await _resultsService.GetEventResults(seriesConfig, id);
        }

        [HttpGet("series/{id}")]
        public async Task<ResultsViewModel> GetSeries(string id)
        {
            var seriesConfig = await _seriesService.GetSeriesAsync(id);

            return await _resultsService.GetSeriesResults(seriesConfig, id);
        }

        [HttpGet("tests")]
        public async Task<bool> zplogintests()
        {
            var zpLoginURL = new Uri("https://zwiftpower.com/ucp.php?mode=login&login=external&oauth_service=oauthzpsso");

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, UseCookies = true })
            using (var client = new HttpClient(handler) { })
            {

                //            var content = new FormUrlEncodedContent(new[]
                //            {
                //    new KeyValuePair<string, string>("foo", "bar"),
                //    new KeyValuePair<string, string>("baz", "bazinga"),
                //});
                //            cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
                var result = await client.GetAsync(zpLoginURL);
                result.EnsureSuccessStatusCode();

                var html = await result.Content.ReadAsStringAsync();

                var loginURL = "";

                var loginContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", ""),
                    new KeyValuePair<string, string>("password", ""),
                    new KeyValuePair<string, string>("rememberMe", "on"),
                });

                var loginResult = await client.PostAsync(loginURL, loginContent);

                var htmlLogin = await result.Content.ReadAsStringAsync();
            }
            return true;

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.zwift.com/eu/sign-in?redirect_uri=https://www.zwift.com/feed?auth_redirect=true");
            //request.CookieContainer = new CookieContainer();
            //request.Method = "GET";
            ////request.ContentType = "application/x-www-form-urlencoded";
            //request.AllowAutoRedirect = true;

            ////using (var stream = request.GetRequestStream())
            ////{
            ////    stream.Write(data, 0, data.Length);
            ////}

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //var cookies = new CookieContainer();
            //cookies.Add(response.Cookies);


            //foreach (Cookie cook in response.Cookies)
            //{
            //    //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@desktop + "\\cookie.html", true))
            //    //{
            //    //    file.WriteLine(cook.ToString());
            //    //}
            //    // Show the string representation of the cookie.                
            //}

            //HttpWebRequest requestNext = (HttpWebRequest)WebRequest.Create("####");
            ////requestNext.CookieContainer.Add(cookies);
            //requestNext.Method = "POST";

            //HttpWebResponse responseNext = (HttpWebResponse)requestNext.GetResponse();

        }
    }
}