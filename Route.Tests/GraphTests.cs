﻿using System;
using System.Collections.Generic;
using System.Text;
using Route.Models;
using Xunit;

namespace Route.Tests
{
    public class GraphTests
    {
        [Fact]
        public void BreathFirstSearch_VariousInput_ReturnCorrectRoute() {

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
            Assert.Equal(new List<int> { 0,1,6,3,4,5, 2,7}, result.Item1);
            Assert.Equal(new List<int> { 0, 1, 5 }, result.Item2);

        }

        [Fact]
        public void DepthFirstSearch_VariousInput_ReturnCorrectRoute() {

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
            var result = graph.DepthFirstSearch(0, 5);

            // Assert
            Assert.Equal(new List<int> { 0, 3, 5, 2, 7, 6, 4,1 }, result.Item1);
            Assert.Equal(new List<int> { 0, 3, 5 }, result.Item2);

        }

        [Fact]
        public void DepthFirstSearch_InvalidInput_ReturnException() {

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
            Exception ex = Assert.Throws<KeyNotFoundException>(() => graph.DepthFirstSearch(0, 11));

            // Assert
            Assert.Equal("The targetNode is not a member of the nodes in the graph.", ex.Message);

        }

        [Fact]
        public void DepthFirstSearch_StartAndEndInDifferentEndsOfSearch_ReturnCorrectPath() {

            // Arrange
            var graph = new Graph(8);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 2);
            graph.AddEdge(2, 4);
            graph.AddEdge(4, 2);
            graph.AddEdge(3, 7);
            graph.AddEdge(7, 4);
            graph.AddEdge(7, 5);
            graph.AddEdge(1, 5);
            graph.AddEdge(5, 1);
            graph.AddEdge(5, 0);
            graph.AddEdge(0, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(6, 5);
            graph.AddEdge(5, 7);

            // Act
            var result = graph.DepthFirstSearch(2, 1);

            // Assert
            Assert.Equal(new List<int> { 2, 4, 3, 7, 5, 6, 0, 1 }, result.Item1);
            Assert.Equal(new List<int> { 2, 3, 7, 5, 1 }, result.Item2);
            //TODO 1 is not in the distionary as it is the last node hit?
        }
    }
}
