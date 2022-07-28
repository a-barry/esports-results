namespace Common.Models
{
    public class RawEventResults
    {
        public Enums.Platform Platform { get; set; }

        public string EventID { get; set; }

        public IEnumerable<RawResult> Results { get; set; }

        //public IDictionary<string, string> Teams { get; set; }
    }
}
