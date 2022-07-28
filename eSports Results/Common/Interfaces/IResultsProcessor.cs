namespace Common.Interfaces
{
    public interface IResultsProcessor
    {
        void Init(Models.EventProcessorConfiguration configuration, Models.RawEventResults rawEventResults);

        Task<Dictionary<int, IEnumerable<Models.IndividualResult>>> GetIndividualResultsAsync();

        Task<Dictionary<int, IEnumerable<Models.TeamResult>>> GetTeamResultsAsync();
    }
}