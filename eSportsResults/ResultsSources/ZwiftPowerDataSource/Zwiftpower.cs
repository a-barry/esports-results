using Common.Models;
using System.Linq;
using System.Net.Http.Json;

namespace ZwiftPowerDataSource
{
    public class Zwiftpower : Common.Interfaces.IResultsDataSource
    {
        const string cfKPId = "K2HE75OK1CK137";
        const string cfKPSignature = "7f6sOCtlQZRe90rpvVq9l84rRNbxl4HBsyEqJByfkMsXWzVe2JkpWGGMZ~KZ1W5w9lm~wYTaQM8ZHFO-MCBokO9EsyeeAcxFOd7gIu5OTcFFFankW4L6RBR-o3oS48dIi22zpWjLZAyku4SXlXbhtMUJrLsASioVG~UDe00Kvc3OWw~WAf0a8LkrdMxG4kIOlb7vvKYEKONo7IO5-JxNVq5WDwMJqB43qRrI2dSK3TMDUsugJHd6QmWpaPxQPHDxrH7MkuBaumdUS1138NbwLL8KwVfcLlV~VnooURL~ID~9lLQHfQ11tkASNzeJgYczkBXTGiKeOh9npbElvqlfOg__";
        const string cfKPPolicy = "eyJTdGF0ZW1lbnQiOlt7IlJlc291cmNlIjoiaHR0cHM6Ly96d2lmdHBvd2VyLmNvbS8qIiwiQ29uZGl0aW9uIjp7IkRhdGVMZXNzVGhhbiI6eyJBV1M6RXBvY2hUaW1lIjoyMTQ3NDgzNjQ3fX19XX0_";

        private HttpClient _httpClient;
        public Zwiftpower(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<RawEventResults> GetRawResultsFromEventAsync(string id)
        {
            var rawResults =  new RawEventResults() { EventID = id, 
                                                        Results = await GetRawResultsFromZPAsync(id),
                                                        //DualRecordResults = await GetRawDualRecordingFromZPAsync(id)
                                                    };

            // copy dual recording to raw results
            //foreach (var rr in rawResults.DualRecordResults)
            //{
            //    var rider = rawResults.Results.FirstOrDefault(r => r.Id == rr.Id);

            //    if (rider != default(RawResult))
            //    {
            //        rider.HasDualRecording = true;
            //    }
            //}

            return rawResults;
        }

        private async Task<IEnumerable<RawResult>> GetRawResultsFromZPAsync(string id)
        {
            // validate that the supplied id is an int
            int zwiftEventId = eventIdToInt(id);

            //signups
            //https://zwiftpower.com/cache3/results/2425294_zwift.json?Key-Pair-Id={cfKPId}&Signature={cfKPSignature}&Policy={cfKPPolicy}
            //results
            //https://zwiftpower.com/cache3/results/2425294_view.json?Key-Pair-Id={cfKPId}&Signature={cfKPSignature}&Policy={cfKPPolicy}
            //team riders
            //https://zwiftpower.com/cache3/teams/4516_riders.json
            var zpResults = await _httpClient.GetFromJsonAsync<ZwiftPowerData>($"https://zwiftpower.com/cache3/results/{zwiftEventId}_view.json?Key-Pair-Id={cfKPId}&Signature={cfKPSignature}&Policy={cfKPPolicy}");

            // map zpResults to our internal class
            return zpResults.data.Select(zpi => new RawResult()
            {
                Id = zpi.zwid.ToString(),
                Name = zpi.name,
                Team = new RawTeam()
                {
                    Id = zpi.tid,
                    Name = zpi.tname,
                    Colour1 = zpi.tc,
                    Colour2 = zpi.tbc,
                    Colour3 = zpi.tbd
                },
                PositionInPen = zpi.position_in_cat,
                PositionOverall = zpi.pos,
                Pen = CatToPen(zpi.category),
                power5s = zpToRawPowerConversion(zpi.w5, zpi.wkg5),
                power15s = zpToRawPowerConversion(zpi.w15, zpi.wkg15),
                power30s = zpToRawPowerConversion(zpi.w30, zpi.wkg30),
                power1m = zpToRawPowerConversion(zpi.w60, zpi.wkg60),
                power2m = zpToRawPowerConversion(zpi.w120, zpi.wkg120),
                power5m = zpToRawPowerConversion(zpi.w300, zpi.wkg300),
                power20m = zpToRawPowerConversion(zpi.w1200, zpi.wkg1200),
                powerAvgEvent = zpToRawPowerConversion(zpi.avg_power, zpi.avg_wkg),
                powerFTPEvent = zpToRawPowerConversion(zpi.wftp, zpi.wkg_ftp),
                Weight = objArrayToDecimal(zpi.weight),
                Height = zpi.height[0],
                FTP = stringToInt(zpi.ftp),
            });
        }

        private RawPower zpToRawPowerConversion(object[] watts, object[] wpkg)
        {
            int powerAsInt = 0;

            if (watts != null && watts.Length >= 1)
            {
                int.TryParse(watts[0].ToString(), out powerAsInt);
            }

            return zpToRawPowerConversion(powerAsInt, wpkg);
        }
        private RawPower zpToRawPowerConversion(int[] watts, object[] wpkg)
        {
            if (watts != null && watts.Length >= 1)
            {
                return zpToRawPowerConversion(watts[0], wpkg);
            }

            return zpToRawPowerConversion(0, wpkg);
        }

        private RawPower zpToRawPowerConversion(int watts, object[] wpkg)
        {
            return new RawPower() { power = watts, wpkg = objArrayToDecimal(wpkg) };
        }

        private decimal objArrayToDecimal(object[] objArr)
        {
            decimal result = 0;

            if (objArr != null && objArr.Length >= 1)
            {
                result = objToDecimal(objArr[0]);
            }
            return result;
        }

        private decimal objToDecimal(object obj)
        {
            decimal result = 0;

            if (obj != null)
            {
                decimal.TryParse(obj.ToString(), out result);
            }
            return result;
        }
        private int stringToInt(string str)
        {
            int result = 0;

            if (str != null)
            {
                int.TryParse(str, out result);
            }
            return result;
        }



        private async Task<IEnumerable<RawDualRecordingResult>> GetRawDualRecordingFromZPAsync(string id)
        {
            //&_=1659430847635

            // validate that the supplied id is an int
            int zwiftEventId = eventIdToInt(id);

            var zpAnalysisResults = await _httpClient.GetFromJsonAsync<ZwiftPowerAnalysis>($"https://zwiftpower.com/api3.php?do=analysis_event_list&zwift_event_id={zwiftEventId}");

            return zpAnalysisResults.data.Select(dr => new RawDualRecordingResult()
            {
                Id = dr.zwid.ToString()
            });
        }

        private int eventIdToInt(string eventId)
        {
            // validate that the supplied id is an int
            int zwiftEventId = 0;

            if (int.TryParse(eventId, out zwiftEventId))
            {
                return zwiftEventId;
            }
            else
            {
                throw new ArgumentException("Zwift event id must be an integer.");
            }
        }

        private int CatToPen(string cat)
        {
            switch (cat)
            {
                case "A": return 1;
                case "B": return 2;
                case "C": return 3;
                case "D": return 4;
                case "E": return 5;
                default: return -1;
            }             
        }
    }
}