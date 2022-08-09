using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwiftPowerDataSource
{
    internal class ZwiftPowerAnalysis
    {
        public IEnumerable<ZwiftPowerAnalysisItem> data { get; set; }
    }

    internal class ZwiftPowerAnalysisItem
    {
        public string name { get; set; }
        public int friend { get; set; }
        public string ftp { get; set; }
        public int zwid { get; set; }
        public string s { get; set; }
        public string pt { get; set; }
        public string tid { get; set; }
        public string topen { get; set; }
        public string tname { get; set; }
        public string tc { get; set; }
        public string tbc { get; set; }
        public string tbd { get; set; }
        public string flag { get; set; }
        public string w { get; set; }
        public string age { get; set; }
        public int height { get; set; }
        public string gender { get; set; }
        public int zada { get; set; }
        public string events { get; set; }
        public int div { get; set; }
        public int divw { get; set; }
        public string rank { get; set; }
        public int skill { get; set; }
        public int skill_race { get; set; }
        public int skill_seg { get; set; }
        public int skill_power { get; set; }
        public int skill_pos { get; set; }
        public int reg { get; set; }
        public string eff { get; set; }
        public string fl { get; set; }
        public string set_id { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public int p { get; set; }
        public string name1 { get; set; }
        public int[] power5 { get; set; }
        public int[] power15 { get; set; }
        public int[] power60 { get; set; }
        public int[] power300 { get; set; }
        public int[] power1200 { get; set; }
        public int[] missing { get; set; }
        public int[] zero { get; set; }
        public string name2 { get; set; }
    }

}
