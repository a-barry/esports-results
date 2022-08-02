using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.ViewModels
{
    public class ResultsViewModel
    {
        public string EventId { get; set; }
        public string EventTitle { get; set; }

        public string SeriesId { get; set; }
        public string SeriesTitle { get; set; }

        public Dictionary<int, IEnumerable<IndividualResult>> IndividualResults { get; set; }
        public Dictionary<int, IEnumerable<TeamResult>> TeamResults { get; set; }
    }
}
