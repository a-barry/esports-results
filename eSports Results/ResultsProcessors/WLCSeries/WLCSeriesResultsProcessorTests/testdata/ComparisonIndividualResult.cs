using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLCSeriesResultsProcessorTests.testdata
{
    internal class ComparisonIndividualResultCollection
    {
        public IEnumerable<ComparisonIndividualResult> Results { get; set; }
    }


    internal class ComparisonIndividualResult
    {        
        public int zwid { get; set; }
        public string name { get; set; }
        public string pen { get; set; }
        public int penInt { 
            get {
                int result = 0;

                int.TryParse(pen, out result);
                return result;
            } 
        }
        public int positionOverall { get; set; }
        public int originalPositionInPen { get; set; }
        public int positionInPen { get; set; }
        public int teamId { get; set; }
    }

}
