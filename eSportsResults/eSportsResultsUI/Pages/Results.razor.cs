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
    public partial class Results
    {
        [Inject]
        public HttpClient http { get; set; }

        //[Parameter] public string SeriesId { get; set; }

        [Parameter] public string EventId { get; set; }

        protected ResultsViewModel? eventResults;

        protected override async Task OnInitializedAsync()
        {
            eventResults = await http.GetFromJsonAsync<ResultsViewModel>($"/results/{EventId}");

            //eventResults = await helper.GetEventResults(SeriesId, EventId);
        }
    }
}