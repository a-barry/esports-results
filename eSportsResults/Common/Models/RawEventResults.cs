namespace Common.Models
{
    public class RawEventResults
    {
        public string EventID { get; set; }

        public IEnumerable<RawResult> Results { get; set; }

        public IEnumerable<RawDualRecordingResult> DualRecordResults { get; set; }

        public bool GotData
        {
            get
            {
                return Results is not null && Results.Any();
            }
        }
    }
}
