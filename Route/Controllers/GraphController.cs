using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Route.Models;

namespace Route.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly ILogger<GraphController> _logger;
        private readonly Graph _graph;

        public GraphController(ILogger<GraphController> logger) {
            _logger = logger;

            _graph = new Graph(8);
            _graph.AddEdge(0, 1);
            _graph.AddEdge(1, 2);
            _graph.AddEdge(2, 3);
            _graph.AddEdge(3, 2);
            _graph.AddEdge(2, 4);
            _graph.AddEdge(4, 2);
            _graph.AddEdge(3, 7);
            _graph.AddEdge(7, 4);
            _graph.AddEdge(7, 5);
            _graph.AddEdge(1, 5);
            _graph.AddEdge(5, 1);
            _graph.AddEdge(5, 0);
            _graph.AddEdge(0, 5);
            _graph.AddEdge(5, 6);
            _graph.AddEdge(6, 5);
            _graph.AddEdge(5, 7);

        }

        // GET: api/Graph/startPoint/endPoint
        [HttpGet("BFS/{startPoint}/{endPoint}")]
        public List<int> GetShortestPathByBFS(int startPoint, int endPoint) {
            Tuple<List<int>, List<int>> result = _graph.BreathFirstSearch(startPoint, endPoint);
            _logger.LogInformation("Path by Breath First search");
            return result.Item2;
        }

        // GET: api/Graph/startPoint/endPoint
        [HttpGet("DFS/{startPoint}/{endPoint}")]
        public List<int> GetShortestPathByDFS(int startPoint, int endPoint) {
            Tuple<List<int>, List<int>> result = _graph.DepthFirstSearch(startPoint, endPoint);
            _logger.LogInformation("Path by Depth First search");
            return result.Item2;
        }
    }
}
