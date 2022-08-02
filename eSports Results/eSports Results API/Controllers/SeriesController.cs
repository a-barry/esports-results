using Common.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eSports_Results_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeriesController : ControllerBase
    {
        private readonly ILogger<ResultsController> _logger;

        private Services.SeriesService _seriesService { get; set; }

        public SeriesController(ILogger<ResultsController> logger, Services.SeriesService seriesService)
        {
            _logger = logger;
            _seriesService = seriesService;
        }

        [HttpGet()]
        public async Task<IEnumerable<SeriesViewModel>> GetAll()
        {
            var series = await _seriesService.GetAllSeriesAsync();

            return series.Select(s => new SeriesViewModel() { Id = s.Id, Title = s.Title, Events = s.EventIds });
        }


        [HttpGet("{id}")]
        public async Task<SeriesViewModel> Get(string id)
        {
            var series = await _seriesService.GetSeriesAsync(id);

            return new SeriesViewModel() { Id = series.Id, Title = series.Title, Events = series.EventIds };
        }
    }
}