using Common.Models;
using Common.Models.ViewModels;

namespace eSports_Results_API.Services
{
    public class ResultsService
    {
        private Common.Interfaces.IResultsProcessor _processor;
        private Common.Interfaces.IResultsDataSource _resultsDataSource;

        public ResultsService(Common.Interfaces.IResultsProcessor processor, Common.Interfaces.IResultsDataSource resultsDataSource)
        {
            _processor = processor;
            _resultsDataSource = resultsDataSource;
        }

        public async Task<ResultsViewModel> GetEventResults(SeriesProcessorConfiguration seriesConfiguration, string eventId)
        {
            RawEventResults rawResults = null;

            // check db for existing copy of the results
            //rawResults = await _dataStore.GetResultsAsync(rawResults);(eventId);

            // if not found in app db go get them.
            if (rawResults is null)
            {
                rawResults = await _resultsDataSource.GetRawResultsFromEventAsync(eventId);

                // save for next time
                // await _dataStore.SaveResultsAsync(rawResults);
            }


            var results = new ResultsViewModel() { EventId = eventId,
                SeriesId = seriesConfiguration.Id,
                SeriesTitle = seriesConfiguration.Title,
                EventTitle = (seriesConfiguration.EventIds.ToList().IndexOf(eventId) + 1).ToString(),
            };

            results.IndividualResults = await _processor.GetIndividualResultsAsync(seriesConfiguration.EventConfiguration, rawResults);
            results.TeamResults = await _processor.GetTeamResultsAsync(seriesConfiguration.EventConfiguration, rawResults);

            return results;
        }
    }
}
