using Common.Models;

namespace Common.Interfaces
{
    public interface IResultsProcessor
    {
        Task<Dictionary<int, IEnumerable<Models.IndividualResult>>> GetIndividualResultsAsync(EventProcessorConfiguration configuration, RawEventResults rawEventResults);

        Task<Dictionary<int, IEnumerable<Models.TeamResult>>> GetTeamResultsAsync(EventProcessorConfiguration configuration, RawEventResults rawEventResults);

        Task<Dictionary<int, IEnumerable<Models.IndividualResult>>> GetSeriesIndividualResultsAsync(SeriesProcessorConfiguration configuration, IEnumerable<RawEventResults> rawSeriesEventResults);

        Task<Dictionary<int, IEnumerable<Models.TeamResult>>> GetSeriesTeamResultsAsync(SeriesProcessorConfiguration configuration, IEnumerable<RawEventResults> rawSeriesEventResults);
    }
}