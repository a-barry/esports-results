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
        //public async Task<bool> zplogintests()
        //{
        //    var zpLoginURL = new Uri("https://zwiftpower.com/ucp.php?mode=login&login=external&oauth_service=oauthzpsso");

        //    var cookieContainer = new CookieContainer();
        //    using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer, UseCookies = true })
        //    using (var client = new HttpClient(handler) { })
        //    {
        //        var result = await client.GetAsync(zpLoginURL);
        //        result.EnsureSuccessStatusCode();

        //        var html = await result.Content.ReadAsStringAsync();

        //        var loginURL = "";

        //        var loginContent = new FormUrlEncodedContent(new[]
        //        {
        //            new KeyValuePair<string, string>("username", ""),
        //            new KeyValuePair<string, string>("password", ""),
        //            new KeyValuePair<string, string>("rememberMe", "on"),
        //        });

        //        var loginResult = await client.PostAsync(loginURL, loginContent);

        //        var htmlLogin = await result.Content.ReadAsStringAsync();
        //    }
        //    return true;

        //}
    }
}