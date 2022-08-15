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
    public partial class Series
    {
        [Inject]
        public HttpClient http { get; set; }

        [Parameter] public string SeriesId { get; set; }

        //protected SeriesViewModel? series;

        protected ResultsViewModel? seriesResults;

        protected override async Task OnInitializedAsync()
        {
            seriesResults = await http.GetFromJsonAsync<ResultsViewModel>($"/results/series/{SeriesId}");

            //eventResults = await helper.GetEventResults(SeriesId, EventId);
        }
    }
}