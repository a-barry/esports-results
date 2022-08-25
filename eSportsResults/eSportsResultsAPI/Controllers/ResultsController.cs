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

        //[HttpGet("tests")]
        //public async Task<ResultsViewModel> zplogintests(string id)
        //{

        //    var h = new HttpClient();

        //    var l = await h.GetAsync("https://www.zwift.com/eu/sign-in?redirect_uri=https://www.zwift.com/feed?auth_redirect=true");


        //    var loginURL = new Uri("https://www.zwift.com/eu/sign-in?redirect_uri=https://www.zwift.com/feed?auth_redirect=true");
        //    var loginURL = new Uri("https://www.zwift.com/eu/sign-in?redirect_uri=https://www.zwift.com/feed?auth_redirect=true");
        //    var cookieContainer = new CookieContainer();
        //    using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
        //    using (var client = new HttpClient(handler) { })
        //    {

        //        //            var content = new FormUrlEncodedContent(new[]
        //        //            {
        //        //    new KeyValuePair<string, string>("foo", "bar"),
        //        //    new KeyValuePair<string, string>("baz", "bazinga"),
        //        //});
        //        //            cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
        //        var result = await client.GetAsync(loginURL);
        //        result.EnsureSuccessStatusCode();

        //        var login = new StringContent("{ \"email\":\"your.email@email.com\",\"password\":\"ssss\",\"rememberMe\":true,\"recaptchaVersion\":\"v3\",\"queryParams\":{ \"redirect_uri\":\"https://www.zwift.com/feed?auth_redirect=true\"},\"token\":\"\"}");

        //        var loginResult = await client.PostAsync(loginURL, login);
        //    }


        //    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.zwift.com/eu/sign-in?redirect_uri=https://www.zwift.com/feed?auth_redirect=true");
        //    //request.CookieContainer = new CookieContainer();
        //    //request.Method = "GET";
        //    ////request.ContentType = "application/x-www-form-urlencoded";
        //    //request.AllowAutoRedirect = true;

        //    ////using (var stream = request.GetRequestStream())
        //    ////{
        //    ////    stream.Write(data, 0, data.Length);
        //    ////}

        //    //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    //var cookies = new CookieContainer();
        //    //cookies.Add(response.Cookies);


        //    //foreach (Cookie cook in response.Cookies)
        //    //{
        //    //    //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@desktop + "\\cookie.html", true))
        //    //    //{
        //    //    //    file.WriteLine(cook.ToString());
        //    //    //}
        //    //    // Show the string representation of the cookie.                
        //    //}

        //    //HttpWebRequest requestNext = (HttpWebRequest)WebRequest.Create("####");
        //    ////requestNext.CookieContainer.Add(cookies);
        //    //requestNext.Method = "POST";

        //    //HttpWebResponse responseNext = (HttpWebResponse)requestNext.GetResponse();

        //}
    }
}