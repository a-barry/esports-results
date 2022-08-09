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
        public string pt { get; set; }
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
        public string vtta { get; set; }
        public int vttat { get; set; }
        public int male { get; set; }
        public string tid { get; set; }
        public string topen { get; set; }
        public string tname { get; set; }
        public string tc { get; set; }
        public string tbc { get; set; }
        public string tbd { get; set; }
        public int zeff { get; set; }
        public string category { get; set; }
        public int[] height { get; set; }
        public string flag { get; set; }
        public int[] avg_hr { get; set; }
        public int[] max_hr { get; set; }
        public int[] hrmax { get; set; }
        public int hrm { get; set; }
        public object[] weight { get; set; }
        public int power_type { get; set; }
        public int display_pos { get; set; }
        public int src { get; set; }
        public string age { get; set; }
        public int zada { get; set; }
        public string note { get; set; }
        public int div { get; set; }
        public int divw { get; set; }
        public object skill { get; set; }
        public object skill_b { get; set; }
        public object skill_gain { get; set; }
        public int[] np { get; set; }
        public object[] hrr { get; set; }
        public object[] hreff { get; set; }
        public int[] avg_power { get; set; }
        public object[] avg_wkg { get; set; }
        public object[] wkg_ftp { get; set; }
        public int[] wftp { get; set; }
        public int wkg_guess { get; set; }
        public object[] wkg1200 { get; set; }
        public object[] wkg300 { get; set; }
        public object[] wkg120 { get; set; }
        public object[] wkg60 { get; set; }
        public object[] wkg30 { get; set; }
        public object[] wkg15 { get; set; }
        public object[] wkg5 { get; set; }
        public object[] w1200 { get; set; }
        public object[] w300 { get; set; }
        public object[] w120 { get; set; }
        public object[] w60 { get; set; }
        public object[] w30 { get; set; }
        public object[] w15 { get; set; }
        public object[] w5 { get; set; }
        public int is_guess { get; set; }
        public int upg { get; set; }
        public string penalty { get; set; }
        public int reg { get; set; }
        public string fl { get; set; }
        public string pts { get; set; }
        public string pts_pos { get; set; }
        public int info { get; set; }
        public object[] info_notes { get; set; }
        public int log { get; set; }
        public int lead { get; set; }
        public int sweep { get; set; }
        public string actid { get; set; }
        public int anal { get; set; }
        public string notes { get; set; }
    }

}
