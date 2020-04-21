using System;
using System.Collections.Generic;
using System.Text;
using Route.Models;
using Xunit;

namespace Route.Tests
{
    public class GraphTests
    {
        [Fact]
        public void BreathFirstSearch_VariousInput_ReturnCorrectRouteTrue() {

            // Arrange
            var graph = new Graph(8);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 0);
            graph.AddEdge(1, 4);
            graph.AddEdge(4, 1);
            graph.AddEdge(4, 6);
            graph.AddEdge(6, 4);
            graph.AddEdge(6, 0);
            graph.AddEdge(0, 6);
            graph.AddEdge(1, 5);
            graph.AddEdge(5, 1);
            graph.AddEdge(5, 3);
            graph.AddEdge(3, 5);
            graph.AddEdge(3, 0);
            graph.AddEdge(0, 3);
            graph.AddEdge(5, 2);
            graph.AddEdge(2, 5);
            graph.AddEdge(2, 7);
            graph.AddEdge(7, 2);

            // Act
            var result = graph.BreathFirstSearch(0,5);

            // Assert
            Assert.Equal(new List<int> { 0,1,6,3,4,5 }, result.Item1);
            Assert.Equal(new List<int> { 0, 1, 5 }, result.Item2);

        }
    }
}
