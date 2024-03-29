using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using eSportsResults.UI;
using eSportsResults.UI.Shared;
using Common.Models.ViewModels;

namespace eSportsResults.UI.Pages
{
    public partial class Index
    {
        [Inject]
        public HttpClient http { get; set; }

        protected IEnumerable<SeriesViewModel>? allSeries;

        protected override async Task OnInitializedAsync()
        {
            allSeries = await http.GetFromJsonAsync<IEnumerable<SeriesViewModel>>("/series");
        }
    }
}