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
        #region Series one
        [TestMethod]
        public async Task Event_2425294()
        {
            await TestEventIndividual("2425294");
        }

        [TestMethod]
        public async Task Event_2445599()
        {
            await TestEventIndividual("2445599");
        }

        [TestMethod]
        public async Task Event_2471894()
        {
            await TestEventIndividual("2471894");
        }

        [TestMethod]
        public async Task Event_2498214()
        {
            await TestEventIndividual("2498214");
        }

        [TestMethod]
        public async Task Event_2525130()
        {
            await TestEventIndividual("2525130");
        }

        [TestMethod]
        public async Task Event_2572480()
        {
            await TestEventIndividual("2572480");
        }

        [TestMethod]
        public async Task Event_2425294_Team()
        {
            await TestEventTeam("2425294");
        }

        [TestMethod]
        public async Task Event_2445599_Team()
        {
            await TestEventTeam("2445599");
        }
        
        [TestMethod]
        public async Task Event_2471894_Team()
        {
            await TestEventTeam("2471894");
        }

        [TestMethod]
        public async Task Event_2498214_Team()
        {
            await TestEventTeam("2498214");
        }

        [TestMethod]
        public async Task Event_2525130_Team()
        {
            await TestEventTeam("2525130");
        }

        [TestMethod]
        public async Task Event_2572480_Team()
        {
            await TestEventTeam("2572480");
        }

        [TestMethod]
        public async Task Series1()
        {
            await TestSeriesIndividual("Series1", new string[] { "2425294", "2445599", "2471894", "2498214", "2525130", "2572480" });
        }

        [TestMethod]
        public async Task Series1Team()
        {
            await TestSeriesTeam("Series1", new string[] { "2425294", "2445599", "2471894", "2498214", "2525130", "2572480" });
        }
        #endregion

        #region test helpers
        private async Task TestEventIndividual(string eventId)
        {
            var config = new EventProcessorConfiguration()
            {
                MaxScorersPerTeam = 3,
                PointsForFirst = 50,
                PointStep = 1,
                ScorePrimes = false,
                PointsForParticipation = 1
            };

            var rawData = JSectionReader.Section($"{eventId}.json").GetObject<ComparisonIndividualResultCollection>();

            // yes this could be done with automapper but thats overkill in a test.
            var convertedData = rawData.Results.Select(r => new RawResult()
            {
                Id = r.zwid.ToString(),
                Name = r.name,
                Pen = r.penInt,
                Team = new RawTeam()
                {
                    Id = r.teamId.ToString()
                },
                PositionInPen = r.positionInPen,
                OriginalPositionInPen = r.originalPositionInPen,
                PositionOverall = r.positionOverall
            }).ToList();

            var processor = new WLCSeriesResultsProcessor.WLCEventProcessor();

            var processedResults = await processor.GetIndividualResultsAsync(config, new RawEventResults() { Results = convertedData });

            var expectedResults = JSectionReader.Section($"{eventId}_Results.json").GetObject<ComparisonExpectResults>();

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
                    Assert.IsTrue(expectedRider.Points == rider.Points, $"Rider {rider.Name} score mismatch. Found {rider.Points}, expect {expectedRider.Points}");
                }
            }

        }

        private async Task TestEventTeam(string eventId)
        {
            var config = new EventProcessorConfiguration()
            {
                MaxScorersPerTeam = 3,
                PointsForFirst = 50,
                PointStep = 1,
                ScorePrimes = false,
                PointsForParticipation = 1
            };

            var rawData = JSectionReader.Section($"{eventId}.json").GetObject<ComparisonIndividualResultCollection>();

            // yes this could be done with automapper but thats overkill in a test.
            var convertedData = rawData.Results.Select(r => new RawResult()
            {
                Id = r.zwid.ToString(),
                Name = r.name,
                Pen = r.penInt,
                Team = new RawTeam()
                {
                    Id = r.teamId.ToString()
                },
                PositionInPen = r.positionInPen,
                OriginalPositionInPen = r.originalPositionInPen,
                PositionOverall = r.positionOverall
            }).ToList();

            var processor = new WLCSeriesResultsProcessor.WLCEventProcessor();

            var processedResults = await processor.GetTeamResultsAsync(config, new RawEventResults() { Results = convertedData });

            var expectedResults = JSectionReader.Section($"{eventId}_Results.json").GetObject<ComparisonExpectResults>();

            // compare overall
            // total results
            Assert.IsTrue(processedResults[0].Count() == expectedResults.TeamOverall.Count());
            //scores match
            CheckTeam(processedResults[0], expectedResults.TeamOverall);

            // compare A pen
            // total results
            Assert.IsTrue(processedResults[1].Count() == expectedResults.TeamA.Count());
            //scores match
            CheckTeam(processedResults[1], expectedResults.TeamA);

            // compare B pen
            // total results
            Assert.IsTrue(processedResults[2].Count() == expectedResults.TeamB.Count());
            //scores match
            CheckTeam(processedResults[2], expectedResults.TeamB);

            // compare C pen
            // total results
            Assert.IsTrue(processedResults[3].Count() == expectedResults.TeamC.Count());
            //scores match
            CheckTeam(processedResults[3], expectedResults.TeamC);

            // compare D pen
            // total results
            Assert.IsTrue(processedResults[4].Count() == expectedResults.TeamD.Count());            //scores match
            CheckTeam(processedResults[4], expectedResults.TeamD);

        }

        private void CheckTeam(IEnumerable<TeamResult> processedResults, IEnumerable<TeamResult> expectedResults)
        {
            foreach (var team in processedResults)
            {
                var expectedTeam = expectedResults.FirstOrDefault(r => r.Id == team.Id);

                if (expectedTeam is null)
                {
                    Assert.Fail($"Team {team.Id} not found.");
                }
                else
                {
                    Assert.IsTrue(expectedTeam.Points == team.Points, $"Team {team.Id} score mismatch");
                }
            }

        }

        private async Task TestSeriesIndividual(string seriesId, string[] eventIds)
        {
            var config = new SeriesProcessorConfiguration() {
                QualifyingScoresPerRider = 4,
                EventConfiguration = new EventProcessorConfiguration()
                                        {
                                            MaxScorersPerTeam = 3,
                                            PointsForFirst = 50,
                                            PointStep = 1,
                                            ScorePrimes = false,
                                            PointsForParticipation = 1
                                        }
            };

            var seriesData = new List<RawEventResults>();

            foreach(string eventId in eventIds)
            {
                // yes this could be done with automapper but thats overkill in a test.
                var convertedData = JSectionReader.Section($"{eventId}.json").GetObject<ComparisonIndividualResultCollection>().Results.Select(r => new RawResult()
                {
                    Id = r.zwid.ToString(),
                    Name = r.name,
                    Pen = r.penInt,
                    Team = new RawTeam()
                    {
                        Id = r.teamId.ToString()
                    },
                    PositionInPen = r.positionInPen,
                    OriginalPositionInPen = r.originalPositionInPen,
                    PositionOverall = r.positionOverall
                }).ToList();

                seriesData.Add(new RawEventResults() { Results = convertedData });
            }

            var processor = new WLCSeriesResultsProcessor.WLCEventProcessor();

            var processedResults = await processor.GetSeriesIndividualResultsAsync(config, seriesData);

            var expectedResults = JSectionReader.Section($"{seriesId}_Results.json").GetObject<ComparisonExpectResults>();

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

        private async Task TestSeriesTeam(string seriesId, string[] eventIds)
        {
            var config = new SeriesProcessorConfiguration()
            {
                QualifyingScoresPerRider = 4,
                EventConfiguration = new EventProcessorConfiguration()
                {
                    MaxScorersPerTeam = 3,
                    PointsForFirst = 50,
                    PointStep = 1,
                    ScorePrimes = false,
                    PointsForParticipation = 1
                }
            };

            var seriesData = new List<RawEventResults>();

            foreach (string eventId in eventIds)
            {
                // yes this could be done with automapper but thats overkill in a test.
                var convertedData = JSectionReader.Section($"{eventId}.json").GetObject<ComparisonIndividualResultCollection>().Results.Select(r => new RawResult()
                {
                    Id = r.zwid.ToString(),
                    Name = r.name,
                    Pen = r.penInt,
                    Team = new RawTeam()
                    {
                        Id = r.teamId.ToString()
                    },
                    PositionInPen = r.positionInPen,
                    OriginalPositionInPen = r.originalPositionInPen,
                    PositionOverall = r.positionOverall
                }).ToList();

                seriesData.Add(new RawEventResults() { Results = convertedData });
            }

            var processor = new WLCSeriesResultsProcessor.WLCEventProcessor();

            var processedResults = await processor.GetSeriesTeamResultsAsync(config, seriesData);

            var expectedResults = JSectionReader.Section($"{seriesId}_Results.json").GetObject<ComparisonExpectResults>();

            // compare overall
            // total results
            Assert.IsTrue(processedResults[0].Count() == expectedResults.TeamOverall.Count());
            //scores match
            CheckTeam(processedResults[0], expectedResults.TeamOverall);

            // compare A pen
            // total results
            Assert.IsTrue(processedResults[1].Count() == expectedResults.TeamA.Count());
            //scores match
            CheckTeam(processedResults[1], expectedResults.TeamA);

            // compare B pen
            // total results
            Assert.IsTrue(processedResults[2].Count() == expectedResults.TeamB.Count());
            //scores match
            CheckTeam(processedResults[2], expectedResults.TeamB);

            // compare C pen
            // total results
            Assert.IsTrue(processedResults[3].Count() == expectedResults.TeamC.Count());
            //scores match
            CheckTeam(processedResults[3], expectedResults.TeamC);

            // compare D pen
            // total results
            Assert.IsTrue(processedResults[4].Count() == expectedResults.TeamD.Count());            //scores match
            CheckTeam(processedResults[4], expectedResults.TeamD);

        }
        #endregion
    }
}