using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WLCSeriesResultsProcessorTests
{
    [TestClass]
    public class BasicTeamScoringTests
    {
      
        [TestMethod]
        public async Task SinglePenTeamScoring()
        {
            var config = new EventProcessorConfiguration()
            {
                MaxScorersPerTeam = 3,
                PointsForFirst = 30,
                PointStep = 1,
                ScorePrimes = false,
                PointsForParticipation = 1
            };

            var testResults = new List<RawResult>();
            var testEventResults = new RawEventResults() { Results = testResults };

            var riders = 10;
            var teams = 5;

            // create 5 teams with 10 riders each. 
            // each team will fill 10 postions in order each
            // team 1, postions 1-10
            // team 2, postions 10-20
            // team 3, postions 20-30
            // team 4, postions 30-40
            // team 5, postions 40-50
            for (int t = 1; t <= teams; t++)
            {
                for (int i = 1; i <= riders; i++)
                {
                    testResults.Add(new RawResult() { Id = $"t{t}_r{i}", Pen = 1, Team = new RawTeam() { Id = t.ToString(), Name = t.ToString()}, PositionInPen = (riders * (t - 1)) + i });
                }
            }

            var processor = new WLCSeriesResultsProcessor.WLCEventProcessor();

            var processedResults = await processor.GetTeamResultsAsync(config, testEventResults);

            // check only one entry per team
            for (int i = 1; i <= teams; i++)
            {
                Assert.IsTrue(processedResults.First().Value.Where(t => t.Id == i.ToString()).Count() == 1);
            }

            // totals for each team
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.IsTrue(processedResults[1].FirstOrDefault(t => t.Id == "1").Name == "1");
            Assert.IsTrue(processedResults[1].FirstOrDefault(t => t.Id == "1").Points == 30 + 29 + 28 + (7 * 1)); // score written like this shows top 3 scores from team + 7 participation points
            Assert.IsTrue(processedResults[1].FirstOrDefault(t => t.Id == "2").Points == 20 + 19 + 18 + (7 * 1)); 
            Assert.IsTrue(processedResults[1].FirstOrDefault(t => t.Id == "3").Points == 10 + 9 + 8 + (7 * 1)); 
            Assert.IsTrue(processedResults[1].FirstOrDefault(t => t.Id == "4").Points == 1 + 1 + 1 + (7 * 1)); // by this point all team riders are out of the major points
            Assert.IsTrue(processedResults[1].FirstOrDefault(t => t.Id == "5").Points == 1 + 1 + 1 + (7 * 1)); 
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        }

        [TestMethod]
        public async Task MultiPenTeamScoring()
        {
            var config = new EventProcessorConfiguration()
            {
                MaxScorersPerTeam = 3,
                PointsForFirst = 30,
                PointStep = 1,
                ScorePrimes = false,
                PointsForParticipation = 1
            };

            var testResults = new List<RawResult>();
            var testEventResults = new RawEventResults() { Results = testResults };

            var riders = 10;
            var teams = 5;
            var pens = 3;

            // create 5 teams with 10 riders each. 
            // each team will fill 10 postions in order each
            // team 1, postions 1-10
            // team 2, postions 10-20
            // team 3, postions 20-30
            // team 4, postions 30-40
            // team 5, postions 40-50
            for (int p = 1; p <= pens; p++)
            {
                for (int t = 1; t <= teams; t++)
                {
                    for (int i = 1; i <= riders; i++)
                    {
                        testResults.Add(new RawResult() { Id = $"t{t}_r{i}", Pen = p, Team = new RawTeam() { Id = t.ToString(), Name = t.ToString() }, PositionInPen = (riders * (t - 1)) + i });
                    }
                }
            }

            var processor = new WLCSeriesResultsProcessor.WLCEventProcessor();

            var processedResults = await processor.GetTeamResultsAsync(config, testEventResults);

            // check only one entry per team per pen
            for (int p = 0; p <= pens; p++) // starting p at 0 so we get the overall pen as well.
            {
                for (int i = 1; i <= teams; i++)
                {
                    Assert.IsTrue(processedResults[p].Where(t => t.Id == i.ToString()).Count() == 1);
                }
            }

            for (int p = 1; p <= pens; p++)
            {
                // totals for each team in each pen
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                Assert.IsTrue(processedResults[p].FirstOrDefault(t => t.Id == "1").Points == 30 + 29 + 28 + (7 * 1)); // score written like this shows top 3 scores from team + 7 participation points
                Assert.IsTrue(processedResults[p].FirstOrDefault(t => t.Id == "2").Points == 20 + 19 + 18 + (7 * 1));
                Assert.IsTrue(processedResults[p].FirstOrDefault(t => t.Id == "3").Points == 10 + 9 + 8 + (7 * 1));
                Assert.IsTrue(processedResults[p].FirstOrDefault(t => t.Id == "4").Points == 1 + 1 + 1 + (7 * 1)); // by this point all team riders are out of the major points
                Assert.IsTrue(processedResults[p].FirstOrDefault(t => t.Id == "5").Points == 1 + 1 + 1 + (7 * 1));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            // totals for each team overall
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.IsTrue(processedResults[0].FirstOrDefault(t => t.Id == "1").Points == (30 + 29 + 28 + (7 * 1)) * pens); // score written like this shows top 3 scores from team + 7 participation points
            Assert.IsTrue(processedResults[0].FirstOrDefault(t => t.Id == "2").Points == (20 + 19 + 18 + (7 * 1)) * pens);
            Assert.IsTrue(processedResults[0].FirstOrDefault(t => t.Id == "3").Points == (10 + 9 + 8 + (7 * 1)) * pens);
            Assert.IsTrue(processedResults[0].FirstOrDefault(t => t.Id == "4").Points == (1 + 1 + 1 + (7 * 1)) * pens); // by this point all team riders are out of the major points
            Assert.IsTrue(processedResults[0].FirstOrDefault(t => t.Id == "5").Points == (1 + 1 + 1 + (7 * 1)) * pens);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}