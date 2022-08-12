using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{

    public class IndividualResult : ResultsBase
    {
        public bool HasDuals { get; set; }
        public string TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
