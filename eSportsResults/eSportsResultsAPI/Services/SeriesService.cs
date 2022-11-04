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

            allSeries.Add(new SeriesProcessorConfiguration()
            {
                Id = "7",
                Title = "Wahoo Le Col 2022/23 Season 1 (Mixed)",
                QualifyingScoresPerRider = 4,
                EventIds = new List<string>() { "3113330", "3113338", "3113340", "3113346", "3113348", "3113351" },
                EventConfiguration = new EventProcessorConfiguration()
                {
                    MaxScorersPerTeam = 3,
                    PointsForFirst = 100,
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
                Id = "8",
                Title = "Wahoo Le Col 2022/23 Season 1 (Women)",
                QualifyingScoresPerRider = 4,
                EventIds = new List<string>() { "3113336", "3113339", "3113344", "3113347", "3113349", "3113357" },
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
                Id = "9",
                Title = "Wahoo Le Col 2022/23 Season 2 (Mixed)",
                QualifyingScoresPerRider = 4,
                EventIds = new List<string>() { "3157597", "3222198", "3222202", "3222208", "3222214", "3222218" },
                EventConfiguration = new EventProcessorConfiguration()
                {
                    MaxScorersPerTeam = 3,
                    PointsForFirst = 100,
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
                Id = "10",
                Title = "Wahoo Le Col 2022/23 Season 2 (Women)",
                QualifyingScoresPerRider = 4,
                EventIds = new List<string>() { "3157601", "3222224", "3222226", "3222227", "3222229", "3222232" },
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
