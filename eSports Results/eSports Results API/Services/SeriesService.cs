using Common.Models;

namespace eSports_Results_API.Services
{
    public class SeriesService
    {
        private List<SeriesProcessorConfiguration> allSeries;

        public SeriesService()
        {
            allSeries = new List<SeriesProcessorConfiguration>();

            allSeries.Add(new SeriesProcessorConfiguration()
            {
                Id = "1",
                Title = "Wahoo Le Col Racing Series",
                QualifyingScoresPerRider = 4,
                EventIds = new List<string>() { "2425294", "2445599", "2471894", "2498214", "2525130", "2572480" },
                EventConfiguration = new EventProcessorConfiguration()
                {
                    MaxScorersPerTeam = 3,
                    PointsForFirst = 50,
                    PointStep = 1,
                    ScorePrimes = false,
                    PointsForParticipation = 1
                }
//                topColour	#f2329a
//middleColour	#1b93c9
//bottomColour	#f2329a

            });

            allSeries.Add(new SeriesProcessorConfiguration()
            {
                Id = "2",
                Title = "Wahoo Le Col Racing Series 2",
                QualifyingScoresPerRider = 6,
                EventIds = new List<string>() { "2596513", "2628997", "2660809", "2692552", "2724408", "2756973", "2786004", "2816374" },
                EventConfiguration = new EventProcessorConfiguration()
                {
                    MaxScorersPerTeam = 3,
                    PointsForFirst = 100,
                    PointStep = 1,
                    ScorePrimes = false,
                    PointsForParticipation = 1
                }
//                topColour	#f2329a
//middleColour	#1b93c9
//bottomColour	#f2329a

            });

            allSeries.Add(new SeriesProcessorConfiguration()
            {
                Id = "3",
                Title = "Wahoo Le Col Racing Series For Women",
                QualifyingScoresPerRider = 6,
                EventIds = new List<string>() { "2618305", "2629060", "2660874", "2692618", "2724470", "2757036", "2786067", "2816439" },
                EventConfiguration = new EventProcessorConfiguration()
                {
                    MaxScorersPerTeam = 3,
                    PointsForFirst = 50,
                    PointStep = 1,
                    ScorePrimes = false,
                    PointsForParticipation = 1
                }
//                topColour	#f2324f
//middleColour	#5ed2e7
//bottomColour	#f2329a

            });

            allSeries.Add(new SeriesProcessorConfiguration()
            {
                Id = "4",
                Title = "Wahoo Le Col Awesome Foursome",
                QualifyingScoresPerRider = 4,
                EventIds = new List<string>() { "2858644", "2873037", "2873049", "2873050" },
                EventConfiguration = new EventProcessorConfiguration()
                {
                    MaxScorersPerTeam = 3,
                    PointsForFirst = 50,
                    PointStep = 1,
                    ScorePrimes = false,
                    PointsForParticipation = 1
                }
//                topColour	#f2329a
//middleColour	#1b93c9
//bottomColour	#f2329a

            });

            allSeries.Add(new SeriesProcessorConfiguration()
            {
                Id = "5",
                Title = "Wahoo Le Col Racing Season 3",
                QualifyingScoresPerRider = 6,
                EventIds = new List<string>() { "2906197", "2906198", "2906200", "2906203", "2906204", "2906206" },
                EventConfiguration = new EventProcessorConfiguration()
                {
                    MaxScorersPerTeam = 3,
                    PointsForFirst = 100,
                    PointStep = 1,
                    ScorePrimes = false,
                    PointsForParticipation = 1
                }
//                topColour	#f2329a
//middleColour	#1b93c9
//bottomColour	#f2329a

            });

            allSeries.Add(new SeriesProcessorConfiguration()
            {
                Id = "6",
                Title = "Wahoo Le Col Racing Series For Women - S3",
                QualifyingScoresPerRider = 6,
                EventIds = new List<string>() { "2921808", "2921819", "2921821", "2921825", "2921826", "2921828" },
                EventConfiguration = new EventProcessorConfiguration()
                {
                    MaxScorersPerTeam = 3,
                    PointsForFirst = 50,
                    PointStep = 1,
                    ScorePrimes = false,
                    PointsForParticipation = 1
                }
//                topColour	#f2324f
//middleColour	#5ed2e7
//bottomColour	#f2329a

            });
        }

        public async Task<IEnumerable<SeriesProcessorConfiguration>> GetAllSeriesAsync()
        {
            return allSeries;
        }

        public async Task<SeriesProcessorConfiguration> GetSeriesAsync(string id)
        {
            return allSeries.FirstOrDefault(s => s.Id == id);
        }

        public async Task<SeriesProcessorConfiguration> GetSeriesFromEventAsync(string eventId)
        {
            return allSeries.FirstOrDefault(s => s.EventIds.Contains(eventId));
        }
    }
}
