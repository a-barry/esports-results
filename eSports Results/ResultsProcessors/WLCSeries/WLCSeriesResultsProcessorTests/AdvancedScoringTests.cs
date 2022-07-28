using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WLCSeriesResultsProcessorTests.testdata;
using WonderTools.JsonSectionReader;

namespace WLCSeriesResultsProcessorTests
{
    [TestClass]
    public class AdvancedScoringTests
    {
        [TestMethod]
        public async Task Event_2425294()
        {
            var config = new EventProcessorConfiguration()
            {
                MaxScorersPerTeam = 3,
                PointsForFirst = 50,
                PointStep = 1,
                ScorePrimes = false,
                PointsForParticipation = 1
            };

            var rawData = JSectionReader.Section("2425294.json").GetObject<ComparisonIndividualResultCollection>();

            // yes this could be done with automapper but thats overkill in a test.
            var convertedData = rawData.Results.Select(r => new RawResult() {
                Id = r.zwid.ToString(),
                Name = r.name,
                Pen = r.penInt,
                TeamId = r.teamId.ToString(),
                PositionInPen = r.positionInPen,
                OriginalPositionInPen = r.originalPositionInPen,
                PositionOverall = r.positionOverall }).ToList();

            var processor = new WLCSeriesResultsProcessor.WLCEventProcessor();

            processor.Init(config, new RawEventResults() { Results = convertedData });

            var processedResults = await processor.GetIndividualResultsAsync();


        }
    }
}