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
        /// The team that the rider belongs to.
        /// </summary>
        public RawTeam? Team { get; set; }

        /// <summary>
        /// Has the rider posted dual recording data?
        /// </summary>
        public bool HasDualRecording { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }
        public int FTP { get; set; }

        public RawPower? power5s { get; set; }
        //public RawPower? power10s { get; set; }
        public RawPower? power15s { get; set; }
        public RawPower? power30s { get; set; }
        public RawPower? power1m { get; set; }
        public RawPower? power2m { get; set; }
        public RawPower? power5m { get; set; }
        public RawPower? power20m { get; set; }
        public RawPower? powerAvgEvent { get; set; }
        public RawPower? powerFTPEvent { get; set; }

        //public RawResult()
        //{
        //    power5s = new RawPower();
        //    power10s = new RawPower();
        //    power15s = new RawPower();
        //    power30s = new RawPower();
        //    power1m = new RawPower();
        //    power2m = new RawPower();
        //    power5m = new RawPower();
        //    power20m = new RawPower();
        //    powerAvgEvent = new RawPower();
        //    powerFTPEvent = new RawPower();

        //    Team = new RawTeam();
        //}
    }
}
