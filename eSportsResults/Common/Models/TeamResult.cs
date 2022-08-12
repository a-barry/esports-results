using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class TeamResult : ResultsBase
    {
        /// <summary>
        /// Text colour
        /// </summary>
        public string Colour1 { get; set; }

        /// <summary>
        /// Background
        /// </summary>
        public string Colour2 { get; set; }

        /// <summary>
        /// Border
        /// </summary>
        public string Colour3 { get; set; }
    }
}
