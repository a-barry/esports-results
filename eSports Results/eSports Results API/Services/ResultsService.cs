using Common.Models;
using Common.Models.ViewModels;

namespace eSports_Results_API.Services
{
    public class ResultsService
    {
        private Common.Interfaces.IResultsProcessor _processor;
        private Common.Interfaces.IDataSource _dataSource;

        public ResultsService(Common.Interfaces.IResultsProcessor processor, Common.Interfaces.IDataSource dataSource)
        {
            _processor = processor;
            _dataSource = dataSource;
        }

        public async Task<ResultsViewModel> GetEventResults(SeriesProcessorConfiguration seriesConfiguration, string eventId)
        {
            // get raw results (dummy data)
            //var rawResults = createTestData();
            var rawResults = await _dataSource.GetRawResultsFromEventAsync(eventId);

            var results = new ResultsViewModel() { EventId = eventId, SeriesId = seriesConfiguration.Id, SeriesTitle = seriesConfiguration.Title };

            results.IndividualResults = await _processor.GetIndividualResultsAsync(seriesConfiguration.EventConfiguration, rawResults);
            results.TeamResults = await _processor.GetTeamResultsAsync(seriesConfiguration.EventConfiguration, rawResults);

            return results;
        }


        private RawEventResults createTestData()
        {
            var testResults = new List<RawResult>();
            var testEventResults = new RawEventResults() { Results = testResults };

            var riders = 50;
            var pens = 2;

            for (int p = 1; p <= pens; p++)
            {
                for (int i = 1; i <= riders; i++)
                {
                    testResults.Add(new RawResult() { Id = $"p{p}_r{i}", Pen = p, PositionInPen = i, PositionOverall = (riders * (p - 1)) + i });
                }
            }

            return testEventResults;
        }
    }
}
