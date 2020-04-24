using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Core;
using Forward.Services;


namespace Forward.Pages.HackerRankPages
{
    public class GraphBase : ComponentBase
    {
        [Inject]
        public IGraphService GraphService { get; set; }
        public GraphInput GraphInput { get; set; }
        public bool PathIsFound { get; set; }
        public List<int> Path { get; set; }

        protected override async Task OnInitializedAsync() {

            GraphInput = new GraphInput(0,5,true);
            PathIsFound = false;
        }

        protected async Task HandleValidSubmit() {

            if (GraphInput.IsDFS) {
                Path = await GraphService.GetPathFromDFS(GraphInput.StartNode, GraphInput.TargetNode);
            } else {
                Path = await GraphService.GetPathFromBFS(GraphInput.StartNode, GraphInput.TargetNode);
            }
            PathIsFound = true;
        }

    }
}
