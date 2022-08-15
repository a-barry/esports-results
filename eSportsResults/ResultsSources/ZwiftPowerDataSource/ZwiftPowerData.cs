using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwiftPowerDataSource
{
    internal class ZwiftPowerData
    {
        public IEnumerable<ZwiftPowerDataItem> data { get; set; }
    }
    internal class ZwiftPowerDataItem
    {
        /// <summary>
        /// ??
        /// </summary>
        public string DT_RowId { get; set; }
        public string ftp { get; set; }
        public int friend { get; set; }
        /// <summary>
        /// this might be zift verified!
        /// </summary>
        public object pt { get; set; }
        public string label { get; set; }
        /// <summary>
        /// zwift event id
        /// </summary>
        public string zid { get; set; }
        public int pos { get; set; }
        public int position_in_cat { get; set; }
        public string name { get; set; }
        public int cp { get; set; }
        public int zwid { get; set; }
        public string res_id { get; set; }
        public int lag { get; set; }
        public string uid { get; set; }
        public float[] time { get; set; }
        public float time_gun { get; set; }
        public float gap { get; set; }
        public object vtta { get; set; }
        public object vttat { get; set; }
        /// <summary>
        /// Male or female
        /// </summary>
        public int male { get; set; }
        /// <summary>
        /// team id
        /// </summary>
        public string tid { get; set; }
        /// <summary>
        /// Team is recruiting?
        /// </summary>
        public string topen { get; set; }
        /// <summary>
        /// team name
        /// </summary>
        public string tname { get; set; }
        /// <summary>
        /// Team colour 1
        /// </summary>
        public string tc { get; set; }
        /// <summary>
        /// Team colour 3
        /// </summary>
        public string tbc { get; set; }
        /// <summary>
        /// Team colour 3
        /// </summary>
        public string tbd { get; set; }
        public int zeff { get; set; }
        /// <summary>
        /// Caterory entered
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// Rider height, array for some reason,  value normally in index 0
        /// </summary>
        public int[] height { get; set; }
        public string flag { get; set; }
        /// <summary>
        /// Average HR in event, array for some reason,  value normally in index 0
        /// </summary>
        public int[] avg_hr { get; set; }
        /// <summary>
        /// Max HR inevent, array for some reason,  value normally in index 0
        /// </summary>
        public int[] max_hr { get; set; }
        /// <summary>
        /// unknown, array for some reason,  value normally in index 0
        /// </summary>
        public int[] hrmax { get; set; }
        /// <summary>
        /// Has HRM daat
        /// </summary>
        public int hrm { get; set; }
        /// <summary>
        /// Weight, array for some reason,  value normally in index 0
        /// </summary>
        public object[] weight { get; set; }
        /// <summary>
        /// what type of power measurement 1=?, 2=? 3=smart trainer, may be others
        /// </summary>
        public int power_type { get; set; }
        public int display_pos { get; set; }
        /// <summary>
        /// Maybe indication if power data is from live or fit file
        /// </summary>
        public int src { get; set; }
        /// <summary>
        /// age category
        /// </summary>
        public string age { get; set; }
        public int zada { get; set; }
        public string note { get; set; }
        public int div { get; set; }
        public int divw { get; set; }
        public object skill { get; set; }
        public object skill_b { get; set; }
        public object skill_gain { get; set; }
        /// <summary>
        /// Normalised power, array for some reason,  value normally in index 0
        /// </summary>
        public int[] np { get; set; }
        /// <summary>
        /// Heart rate recovery??(guess), array for some reason,  value normally in index 0
        /// </summary>
        public object[] hrr { get; set; }
        public object[] hreff { get; set; }
        /// <summary>
        /// Average power in event, array for some reason,  value normally in index 0
        /// </summary>
        public int[] avg_power { get; set; }
        /// <summary>
        /// Average wpkg in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] avg_wkg { get; set; }
        /// <summary>
        /// FTP wpkg in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] wkg_ftp { get; set; }
        /// <summary>
        /// FTP power in event, array for some reason,  value normally in index 0
        /// </summary>
        public int[] wftp { get; set; }
        public int wkg_guess { get; set; }
        /// <summary>
        /// Wpkg 1200s (20 mins) in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] wkg1200 { get; set; }
        /// <summary>
        /// Wpkg 300s (5 mins) in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] wkg300 { get; set; }
        /// <summary>
        /// Wpkg 120s (2 mins) in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] wkg120 { get; set; }
        /// <summary>
        /// Wpkg 60s (1 min) in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] wkg60 { get; set; }
        /// <summary>
        /// Wpkg 30s in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] wkg30 { get; set; }
        /// <summary>
        /// Wpkg 15s in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] wkg15 { get; set; }
        /// <summary>
        /// Wpkg 5s in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] wkg5 { get; set; }
        /// <summary>
        /// Power 1200s (20 mins) in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] w1200 { get; set; }
        /// <summary>
        /// Power 300s (5 mins) in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] w300 { get; set; }
        /// <summary>
        /// Power 120s (2 mins) in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] w120 { get; set; }
        /// <summary>
        /// Power 60s (1 min) in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] w60 { get; set; }
        /// <summary>
        /// Power 30s in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] w30 { get; set; }
        /// <summary>
        /// Power 15s in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] w15 { get; set; }
        /// <summary>
        /// Power 5s in event, array for some reason,  value normally in index 0
        /// </summary>
        public object[] w5 { get; set; }
        public int is_guess { get; set; }
        /// <summary>
        /// Needs to upgrade or has upgraded in event??
        /// </summary>
        public int upg { get; set; }
        public string penalty { get; set; }
        /// <summary>
        /// Maybe is registered with ZP
        /// </summary>
        public int reg { get; set; }
        public string fl { get; set; }
        public string pts { get; set; }
        public string pts_pos { get; set; }
        public int info { get; set; }
        public object[] info_notes { get; set; }
        public int log { get; set; }
        /// <summary>
        /// Ride leader
        /// </summary>
        public int lead { get; set; }
        /// <summary>
        /// Sweeper
        /// </summary>
        public int sweep { get; set; }
        public string actid { get; set; }
        public int anal { get; set; }
        public string notes { get; set; }
    }

}
