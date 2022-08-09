using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.ViewModels
{
    public class SeriesViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> Events { get; set; }

    }
}
