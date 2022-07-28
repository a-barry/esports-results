using Common.Models;

namespace WLCSeriesResultsProcessor
{
    public class WLCEventProcessor : Common.Interfaces.IResultsProcessor
    {
        private RawEventResults _rawEventResults;
        private EventProcessorConfiguration _eventScoringConfig;

        public async Task<Dictionary<int, IEnumerable<IndividualResult>>> GetIndividualResultsAsync()
        {
            return await ProcessIndividualResults();
        }

        public async Task<Dictionary<int, IEnumerable<TeamResult>>> GetTeamResultsAsync()
        {
            return await ProcessTeamsResults();
        }

        public void Init(EventProcessorConfiguration configuration, RawEventResults rawEventResults)
        {
            _eventScoringConfig = configuration;
            _rawEventResults = rawEventResults;
        }

        #region Individual
        private async Task<Dictionary<int, IEnumerable<IndividualResult>>> ProcessIndividualResults()
        {
            var penIndividualResults =new Dictionary<int, IEnumerable<IndividualResult>>();

            // extract pens found in results (yes probably 4!)
            var pens = _rawEventResults.Results.Select(r => r.Pen).Distinct();

            // process pen '0' (i.e. the whole event)
            //penIndividualResults.Add(0,await ProcessIndividualPen(0));

            // process the other pens
            foreach (int p in pens)
            {
                penIndividualResults.Add(p, await ProcessIndividualPen(p));
            }

            return penIndividualResults;
        }

        private async Task<IEnumerable<IndividualResult>> ProcessIndividualPen(int pen)
        {
            var results = _rawEventResults.Results.Where(r => pen == 0 || r.Pen == pen).Select(r => new IndividualResult()
            {
                Id = r.Id,
                Name = r.Name,
                Position = pen == 0 ? r.PositionOverall : r.PositionInPen,
                Points = CalcRiderPoints(pen == 0 ? r.PositionOverall : r.PositionInPen),
                TeamId = r.TeamId
            });

            return results;
        }

        private int CalcRiderPoints(int position)
        {
            var points = (_eventScoringConfig.PointsForFirst + 1) - (position * _eventScoringConfig.PointStep);

            if(points <= 0)
            {
                return _eventScoringConfig.PointsForParticipation;
            }
            else
            {
                return points;
            }
        }
        #endregion


        #region Team
        private async Task<Dictionary<int, IEnumerable<TeamResult>>> ProcessTeamsResults()
        {
            var penTeamResults = new Dictionary<int, IEnumerable<TeamResult>>();

            // extract pens found in results (yes probably 4!)
            var pens = _rawEventResults.Results.Select(r => r.Pen).Distinct();

            // process pen '0' (i.e. the whole event)
            // no overall is sum of all cats
            //penTeamResults.Add(0, await ProcessTeamPen(0));

            // process all pens
            foreach (int p in pens)
            {
                penTeamResults.Add(p, await ProcessTeamPen(p));
            }

            // combine scores from all pens to make overall team scores
            var overallTeamResults = penTeamResults.SelectMany(p => p.Value) // flatten all pen results into a single list
                    .GroupBy(t => t.Id) // group by each team
                    .Select<IGrouping<string, TeamResult>, TeamResult>(gtr => new TeamResult()
                    {
                        Id = gtr.First().Id,
                        Points = gtr.Sum(tr => tr.Points)
                    }).ToList(); // aggregate team points from each pen into a single result per team for the whole event

            penTeamResults.Add(0, overallTeamResults);

            return penTeamResults;
        }

        /// <summary>
        /// Calcs team points.for a pen
        /// 
        /// A teams points in a
        /// </summary>
        /// <param name="pen"></param>
        /// <returns></returns>
        private async Task<IEnumerable<TeamResult>> ProcessTeamPen(int pen)
        {
            //// Note this calc does not work for the case where every rider should score team points.
            //var results = _rawEventResults.Results.Where(r => r.TeamId != "0" && pen == 0 || r.Pen == pen) // exclude non team riders
            //    .GroupBy(r => r.TeamId) // group riders by their teams
            //    .SelectMany(g => g.Take(_eventScoringConfig.MaxScorersPerTeam)) // trim so we have the top x from each team
            //    .OrderBy(r => pen == 0 ? r.PositionOverall : r.PositionInPen) // order the list by overall or pen finish position
            //    .Select((r, index) => new TeamResult()
            //    {
            //        Id = r.TeamId, // project each rider as their team
            //        //Name = String.Empty, // team name not known here
            //        Position = index + 1, // 'new' position is position in the ordered list
            //        Points = CalcRiderPoints(index + 1)
            //    })
            //    .GroupBy(tr => tr.Id) // group by team again
            //    .Select<IGrouping<string, TeamResult>, TeamResult>(gtr => new TeamResult()
            //    {
            //        Id = gtr.First().Id,
            //        //Name = String.Empty, // team name not known here
            //        Points = gtr.Sum(tr => tr.Points)
            //    }); // aggregate team points into a single result per team

            if (pen == 0) throw new ArgumentException("Overall pen is calculate from the sum on the individual pen points.");

            // First get the individual results for this pen
            var individualResults = (await ProcessIndividualPen(pen)).Where(r => r.TeamId != "0"); // exclude non team riders,
                                                                                                   // but because we calculated individual points first we
                                                                                                   // have taken into account the finish positions of non-team riders

            // get the full points from the first X riders of each team
            var topXPerTeam = individualResults.GroupBy(i => i.TeamId) // group by team so we can look at each team's riders
                .SelectMany(g => g.OrderByDescending(i => i.Points) // order the teams riders by the points they scored from high to low
                                .Take(_eventScoringConfig.MaxScorersPerTeam)); // take the top X results from each teams

            var othersPerTeam = individualResults.GroupBy(i => i.TeamId) // group by team so we can look at each team's riders
                .SelectMany(g => g.OrderByDescending(i => i.Points) // order the teams riders by the points they scored from high to low
                .Skip(_eventScoringConfig.MaxScorersPerTeam)) // skip over the first X riders (accounted for above)
                .Select(i => new IndividualResult()
                {
                    Id = i.Id,
                    TeamId = i.TeamId,
                    Points = _eventScoringConfig.PointsForParticipation,
                    Position =  i.Position,
                    HasDuals = i.HasDuals,
                    Name = i.Name
                }); // project each rider outside of the top X with just the points for participation.

            var teamResults = topXPerTeam.Union(othersPerTeam) // join top x and all the others
                                    .GroupBy(tr => tr.TeamId) // group by team
                                    .Select<IGrouping<string, IndividualResult>, TeamResult>(gtr => new TeamResult()
                                    {
                                        Id = gtr.First().TeamId,
                                        //Name = String.Empty, // team name not known here
                                        Points = gtr.Sum(tr => tr.Points)
                                    }).ToList(); // aggregate team points into a single result per team

            // add in team names (lets not acutally and do this outside the processor so it applies to all)
            //foreach (TeamResult tr in teamResults) {
            //    tr.Name = _rawEventResults.Teams[tr.Id];
            //}

            return teamResults;
        }

        //private int CalcRiderPoints(int position)
        //{
        //    var points = (_eventScoringConfig.PointsForFirst + 1) - (position * _eventScoringConfig.PointStep);

        //    if (points < 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return points;
        //    }
        //}
        #endregion
    }
}