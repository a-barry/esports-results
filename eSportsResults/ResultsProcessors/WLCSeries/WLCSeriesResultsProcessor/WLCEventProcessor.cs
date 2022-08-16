using Common.Models;

namespace WLCSeriesResultsProcessor
{
    public class WLCEventProcessor : Common.Interfaces.IResultsProcessor
    {
        public async Task<Dictionary<int, IEnumerable<IndividualResult>>> GetIndividualResultsAsync(EventProcessorConfiguration configuration, RawEventResults rawEventResults)
        {
            return await ProcessIndividualResults(configuration, rawEventResults);
        }

        public async Task<Dictionary<int, IEnumerable<TeamResult>>> GetTeamResultsAsync(EventProcessorConfiguration configuration, RawEventResults rawEventResults)
        {
            return await ProcessTeamsResults(configuration, rawEventResults);
        }


        //public void InitSeries(EventProcessorConfiguration configuration, IEnumerable<RawEventResults> rawSeriesResults)
        //{
        //    _eventScoringConfig = configuration;
        //    _rawResults = rawSeriesResults;
        //}

        public async Task<Dictionary<int, IEnumerable<IndividualResult>>> GetSeriesIndividualResultsAsync(SeriesProcessorConfiguration configuration, IEnumerable<RawEventResults> rawSeriesResults)
        {
            // extract pens found in results (yes probably 4!)
            var pens = rawSeriesResults.First().Results.Select(r => r.Pen).Distinct();

            var penIndividualResults = new Dictionary<int, IEnumerable<IndividualResult>>();

            foreach (int p in pens)
            {
                penIndividualResults.Add(p, new List<IndividualResult>());
            }

            foreach (RawEventResults eventResults in rawSeriesResults)
            {
                foreach (int p in pens)
                {
                    var l = penIndividualResults[p].ToList();
                    l.AddRange(await ProcessIndividualPen(p, configuration.EventConfiguration, eventResults));
                    penIndividualResults[p] = l;
                }
            }

            foreach (int p in pens)
            {
                penIndividualResults[p] = penIndividualResults[p].GroupBy(r => r.Id)
                                                                .Select(i => new IndividualResult()
                                                                {
                                                                    Id = i.First().Id,
                                                                    TeamId = i.First().TeamId,
                                                                    TeamName = i.First().TeamName,
                                                                    Points = i.OrderByDescending(j => j.Points).Take(configuration.QualifyingScoresPerRider).Sum(v => v.Points),
                                                                    HasDuals = i.First().HasDuals,
                                                                    Name = i.First().Name
                                                                }).ToList();
            }

            return penIndividualResults;
        }

        public async Task<Dictionary<int, IEnumerable<TeamResult>>> GetSeriesTeamResultsAsync(SeriesProcessorConfiguration configuration, IEnumerable<RawEventResults> rawSeriesResults)
        {
            // extract pens found in results (yes probably 4!)
            var pens = rawSeriesResults.First().Results.Select(r => r.Pen).Distinct();

            // extract team names
            var teams = rawSeriesResults.SelectMany(e => e.Results.Select(r => r.Team)).GroupBy(t => t.Id).Select(g => g.First()).ToDictionary(t => t.Id, t => t);

            var penTeamResults = new Dictionary<int, IEnumerable<TeamResult>>();

            foreach (int p in pens)
            {
                penTeamResults.Add(p, new List<TeamResult>());
            }

            foreach (RawEventResults eventResults in rawSeriesResults)
            {
                foreach (int p in pens)
                {
                    var l = penTeamResults[p].ToList();
                    l.AddRange(await ProcessTeamPen(p, configuration.EventConfiguration, eventResults));

                    //foreach (var t in l)
                    //{
                    //    t.Name = teams[t.Id].Name;
                    //    t.Colour1 = teams[t.Id].Colour1;
                    //    t.Colour2 = teams[t.Id].Colour2;
                    //    t.Colour3 = teams[t.Id].Colour3;
                    //}

                    penTeamResults[p] = l;
                }
            }

            foreach (int p in pens)
            {
                penTeamResults[p] = penTeamResults[p].GroupBy(r => r.Id)
                                                                .Select(i =>
                                                                {
                                                                    var tDetails = teams[i.First().Id];

                                                                    return new TeamResult()
                                                                    {
                                                                        Id = tDetails.Id,
                                                                        Points = i.Sum(v => v.Points),
                                                                        Name = tDetails.Name,
                                                                        Colour1 = tDetails.Colour1,
                                                                        Colour2 = tDetails.Colour2,
                                                                        Colour3 = tDetails.Colour3
                                                                    };
                                                                }
                                                                ).ToList();
            }

            // combine scores from all pens to make overall team score
            penTeamResults.Add(0, CombineTeamPensIntoOverall(penTeamResults));

            return penTeamResults;
        }

        #region Individual
        private async Task<Dictionary<int, IEnumerable<IndividualResult>>> ProcessIndividualResults(EventProcessorConfiguration configuration, RawEventResults eventResults)
        {
            var penIndividualResults =new Dictionary<int, IEnumerable<IndividualResult>>();

            // extract pens found in results (yes probably 4!)
            var pens = eventResults.Results.Select(r => r.Pen).Distinct();

            // process pen '0' (i.e. the whole event)
            //penIndividualResults.Add(0,await ProcessIndividualPen(0));

            // process the other pens
            foreach (int p in pens)
            {
                penIndividualResults.Add(p, await ProcessIndividualPen(p, configuration, eventResults));
            }

            return penIndividualResults;
        }

        private async Task<IEnumerable<IndividualResult>> ProcessIndividualPen(int pen, EventProcessorConfiguration configuration, RawEventResults eventResults)
        {
            var results = eventResults.Results.Where(r => pen == 0 || r.Pen == pen).Select(r => new IndividualResult()
            {
                Id = r.Id,
                Name = r.Name,
                Position = pen == 0 ? r.PositionOverall : r.PositionInPen,
                Points = CalcRiderPoints(configuration, pen == 0 ? r.PositionOverall : r.PositionInPen),
                TeamId = r.Team.Id,
                TeamName = r.Team.Name
            });

            return results;
        }

        private int CalcRiderPoints(EventProcessorConfiguration configuration, int position)
        {
            var points = (configuration.PointsForFirst + 1) - (position * configuration.PointStep);

            if(points <= 0)
            {
                return configuration.PointsForParticipation;
            }
            else
            {
                return points;
            }
        }
        #endregion

        #region Team
        private async Task<Dictionary<int, IEnumerable<TeamResult>>> ProcessTeamsResults(EventProcessorConfiguration configuration, RawEventResults eventResults)
        {
            var penTeamResults = new Dictionary<int, IEnumerable<TeamResult>>();

            // extract pens found in results (yes probably 4!)
            var pens = eventResults.Results.Select(r => r.Pen).Distinct();

            // extract team names
            var teams = eventResults.Results.Select(r => r.Team).GroupBy(t => t.Id).Select(g => g.First()).ToDictionary(t => t.Id, t => t);

            // process pen '0' (i.e. the whole event)
            // no, overall is sum of all cats
            // penTeamResults.Add(0, await ProcessTeamPen(0));

            // process all pens
            foreach (int p in pens)
            {
                var penTeamResult = await ProcessTeamPen(p, configuration, eventResults);

                if (penTeamResult != null)
                {
                    foreach (var t in penTeamResult)
                    {
                        t.Name = teams[t.Id].Name;
                        t.Colour1 = teams[t.Id].Colour1;
                        t.Colour2 = teams[t.Id].Colour2;
                        t.Colour3 = teams[t.Id].Colour3;
                    }

                    penTeamResults.Add(p, penTeamResult);
                }
            }

            // combine scores from all pens to make overall team scores
            penTeamResults.Add(0, CombineTeamPensIntoOverall(penTeamResults));

            return penTeamResults;
        }

        private IEnumerable<TeamResult> CombineTeamPensIntoOverall(Dictionary<int, IEnumerable<TeamResult>> penTeamResults)
        {
            return penTeamResults.SelectMany(p => p.Value) // flatten all pen results into a single list
                    .GroupBy(t => t.Id) // group by each team
                    .Select<IGrouping<string, TeamResult>, TeamResult>(gtr => new TeamResult()
                    {
                        Id = gtr.First().Id,
                        Name = gtr.First().Name,
                        Colour1 = gtr.First().Colour1,
                        Colour2 = gtr.First().Colour2,
                        Colour3 = gtr.First().Colour3,
                        Points = gtr.Sum(tr => tr.Points)
                    }).OrderByDescending(t => t.Points).ToList(); // aggregate team points from each pen into a single result per team for the whole event
        }

        /// <summary>
        /// Calcs team points for a pen
        /// 
        /// A teams points in a
        /// </summary>
        /// <param name="pen"></param>
        /// <returns></returns>
        private async Task<IEnumerable<TeamResult>> ProcessTeamPen(int pen, EventProcessorConfiguration configuration, RawEventResults eventResults)
        {
            if (pen == 0) throw new ArgumentException("Overall pen is calculate from the sum on the individual pen points.");

            // First get the individual results for this pen
            var individualResults = (await ProcessIndividualPen(pen, configuration, eventResults)).Where(r => r.TeamId != "0"); // exclude non team riders,
                                                                                                   // but because we calculated individual points first we
                                                                                                   // have taken into account the finish positions of non-team riders

            // get the full points from the first X riders of each team
            var topXPerTeam = individualResults.GroupBy(i => i.TeamId) // group by team so we can look at each team's riders
                .SelectMany(g => g.OrderByDescending(i => i.Points) // order the teams riders by the points they scored from high to low
                                .Take(configuration.MaxScorersPerTeam)); // take the top X results from each teams

            var othersPerTeam = individualResults.GroupBy(i => i.TeamId) // group by team so we can look at each team's riders
                .SelectMany(g => g.OrderByDescending(i => i.Points) // order the teams riders by the points they scored from high to low
                .Skip(configuration.MaxScorersPerTeam)) // skip over the first X riders (accounted for above)
                .Select(i => new IndividualResult()
                {
                    Id = i.Id,
                    TeamId = i.TeamId,
                    Points = configuration.PointsForParticipation,
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
                                    })
                                    .Where(t => !string.IsNullOrEmpty(t.Id)) // get rid of team less points
                                    .OrderByDescending(r => r.Points).ToList(); // aggregate team points into a single result per team

            // add in team names (lets not acutally and do this outside the processor so it applies to all)
            //foreach (TeamResult tr in teamResults) {
            //    tr.Name = _rawEventResults.Teams[tr.Id];
            //}

            return teamResults;
        }
        #endregion
    }
}