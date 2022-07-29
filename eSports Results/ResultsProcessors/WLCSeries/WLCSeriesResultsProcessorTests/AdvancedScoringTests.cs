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

            var expectedResults = JSectionReader.Section("2425294_Results.json").GetObject<ComparisonExpectResults>();

            // compare A pen
            // total results
            Assert.IsTrue(processedResults[1].Count() == expectedResults.IndividualA.Count());
            //scores match
            CheckRiders(processedResults[1], expectedResults.IndividualA);

            // compare B pen
            // total results
            Assert.IsTrue(processedResults[2].Count() == expectedResults.IndividualB.Count());
            //scores match
            CheckRiders(processedResults[2], expectedResults.IndividualB);

            // compare C pen
            // total results
            Assert.IsTrue(processedResults[3].Count() == expectedResults.IndividualC.Count());
            //scores match
            CheckRiders(processedResults[3], expectedResults.IndividualC);

            // compare D pen
            // total results
            Assert.IsTrue(processedResults[4].Count() == expectedResults.IndividualD.Count());            //scores match
            CheckRiders(processedResults[4], expectedResults.IndividualD);

        }

        private void CheckRiders(IEnumerable<IndividualResult> processedResults, IEnumerable<IndividualResult> expectedResults)
        {
            foreach (var rider in processedResults)
            {
                var expectedRider = expectedResults.FirstOrDefault(r => r.Name == rider.Name);

                if (expectedRider is null)
                {
                    Assert.Fail($"Rider {rider.Name} not found.");
                }
                else
                {
                    Assert.IsTrue(expectedRider.Points == rider.Points, $"Rider {rider.Name} score mismatch");
                }
            }

        }
    }
}