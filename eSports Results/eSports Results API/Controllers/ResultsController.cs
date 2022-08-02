using Common.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        public async Task<ResultsViewModel> Get(string id)
        {
            var seriesConfig = await _seriesService.GetSeriesFromEventAsync(id);

            return await _resultsService.GetEventResults(seriesConfig, id);
        }
    }
}