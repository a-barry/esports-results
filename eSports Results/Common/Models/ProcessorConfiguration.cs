using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class SeriesProcessorConfiguration
    {
        public Enums.Platform Platform { get; set; } = Enums.Platform.Zwift;

        /// <summary>
        /// Series Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title of the whole series
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// THe miniumum number of events a rider has to complete to be included in the GC
        /// </summary>
        public int QualifyingEventsPerRider { get; set; } = 0;

        /// <summary>
        /// The number of events that contribute towards the riders GC score.
        /// </summary>
        public int QualifyingScoresPerRider { get; set; } = 0;

        /// <summary>
        /// The id of the events that make up the series
        /// </summary>
        public IEnumerable<string> EventIds { get; set; }

        public EventProcessorConfiguration EventConfiguration { get; set; }
    }

    public class EventProcessorConfiguration
    {
        /// <summary>
        /// The number of points earned by the first placed rider
        /// </summary>
        /// In future could make this an array for non-liner point steps
        public int PointsForFirst { get; set; }

        /// <summary>
        /// The step size used when calculating points.
        /// </summary>
        /// <example>
        ///     PointStep 1 would produce 1st = 10, 2nd = 9, 3rd = 8
        ///     PointStep 2 would produce 1st = 10, 2nd = 8, 3rd = 6
        ///     PointStep 3 would produce 1st = 10, 2nd = 7, 3rd = 4
        /// </example>
        public int PointStep { get; set; } = 1;

        /// <summary>
        /// The number of points earned by any rider just for making the finish line.
        /// </summary>
        /// <remarks>Can be zero.</remarks>
        public int PointsForParticipation { get; set; } = 0;

        /// <summary>
        /// For team results how many riders will score points. 0 == all riders.
        /// </summary>
        public int MaxScorersPerTeam { get; set; } = 0;
        public bool ScorePrimes { get; set; }
        //public bool ScorePrimes { get; set; }
    }
}
