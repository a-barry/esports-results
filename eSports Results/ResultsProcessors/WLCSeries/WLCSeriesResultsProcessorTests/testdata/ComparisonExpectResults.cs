using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLCSeriesResultsProcessorTests.testdata
{
    internal class ComparisonExpectResults
    {
        public IEnumerable<TeamResult> TeamOverall { get; set; }
        public IEnumerable<TeamResult> TeamA { get; set; }
        public IEnumerable<TeamResult> TeamB { get; set; }
        public IEnumerable<TeamResult> TeamC { get; set; }
        public IEnumerable<TeamResult> TeamD { get; set; }
        public IEnumerable<IndividualResult> IndividualA { get; set; }
        public IEnumerable<IndividualResult> IndividualB { get; set; }
        public IEnumerable<IndividualResult> IndividualC { get; set; }
        public IEnumerable<IndividualResult> IndividualD { get; set; }
    }
}
