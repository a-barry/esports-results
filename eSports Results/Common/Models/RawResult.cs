using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class RawResult
    {
        /// <summary>
        /// Id of the rider. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Riders name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Which pen did the rider enter.
        /// </summary>
        public int Pen { get; set; }

        /// <summary>
        /// Overall finishing position of the rider
        /// </summary>
        public int PositionOverall { get; set; }

        /// <summary>
        /// ?? maybe somehting to do with dq's
        /// </summary>
        public int OriginalPositionInPen { get; set; }
        
        /// <summary>
        /// Finishing position in the entered pen.
        /// </summary>
        public int PositionInPen { get; set; }

        /// <summary>
        /// Id of the team that the rider belongs to.
        /// </summary>
        public string TeamId { get; set; }

        /// <summary>
        /// Name of the team that the rider belongs to.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Has the rider posted dual recording data?
        /// </summary>
        public bool HasDualRecording { get; set; }

        public decimal power5s { get; set; }
        public decimal power10s { get; set; }
        public decimal power15s { get; set; }
        public decimal power30s { get; set; }
        public decimal power1m { get; set; }
        public decimal power5m { get; set; }
        public decimal power20m { get; set; }
        public decimal powerDuration { get; set; }
    }
}
