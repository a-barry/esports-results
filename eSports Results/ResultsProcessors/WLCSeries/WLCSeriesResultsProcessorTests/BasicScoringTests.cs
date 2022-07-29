using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WLCSeriesResultsProcessorTests
{
    [TestClass]
    public class BasicScoringTests
    {
        [TestMethod]
        public async Task SinglePenOrder()
        {
            var config = new EventProcessorConfiguration()
            {
                //MaxScorersPerTeam = 3,
                PointsForFirst = 10,
                PointStep = 1,
                ScorePrimes = false,
                PointsForParticipation = 1
            };

            var testResults = new List<RawResult>();
            var testEventResults = new RawEventResults() { Results = testResults };

            var riders = 50;
            var pen = 1;

            for (int i = 1; i <= riders; i++)
            {
                testResults.Add(new RawResult() { Id = $"r{i}", Pen = pen, PositionInPen = i, PositionOverall = i });
            }

            var processor = new WLCSeriesResultsProcessor.WLCEventProcessor();

            var processedResults = await processor.GetIndividualResultsAsync(config, testEventResults);

            //// check the first 10 riders in test pen have points
            //for (int i = 0; i < config.PointsForFirst; i++)
            //{
            //    Assert.IsTrue(processedResults[pen].ToList()[i].Points != 0);
            //    Assert.IsTrue(processedResults[pen].ToList()[i].Points == config.PointsForFirst - i);
            //}

            //// check the last 40 riders in test pen have no points
            //for (int i = config.PointsForFirst; i < riders; i++)
            //{
            //    Assert.IsTrue(processedResults[pen].ToList()[i].Points == config.PointsForParticipation);
            //}

            // check the first 10 riders in pen have points
            for (int i = 0; i < config.PointsForFirst; i++)
            {
                Assert.IsTrue(processedResults[1].ToList()[i].Points != 0);
                Assert.IsTrue(processedResults[1].ToList()[i].Points == config.PointsForFirst - i);
            }

            // check the last 40 riders in pen have no points
            for (int i = config.PointsForFirst; i < riders; i++)
            {
                Assert.IsTrue(processedResults[1].ToList()[i].Points == config.PointsForParticipation);
            }
        }


        [TestMethod]
        public async Task MultiplePenOrderInPen()
        {
            var config = new EventProcessorConfiguration()
            {
                //MaxScorersPerTeam = 10,
                PointsForFirst = 10,
                PointStep = 1,
                ScorePrimes = false,
                PointsForParticipation = 1
            };

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

            var processor = new WLCSeriesResultsProcessor.WLCEventProcessor();

            var processedResults = await processor.GetIndividualResultsAsync(config, testEventResults);

            for (int p = 1; p <= pens; p++)
            {
                // check the first 10 riders in each pen have points
                for (int i = 0; i < config.PointsForFirst; i++)
                {
                    Assert.IsTrue(processedResults[p].ToList()[i].Points != 0);
                    Assert.IsTrue(processedResults[p].ToList()[i].Points == config.PointsForFirst - i);
                }

                // check the last 40 riders in each pen have participation points
                for (int i = config.PointsForFirst; i < riders ; i++)
                {
                    Assert.IsTrue(processedResults[p].ToList()[i].Points == config.PointsForParticipation);
                }
            }
        }

        // note for future that testing events where pens have split start times can end up with A cat finishing behind other cats because they went off later...
    }
}