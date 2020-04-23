using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.Models
{
    public class Graph
    {
        private readonly int _v; // Number of vertices. Which is the nodes in the graph
        private readonly LinkedList<int>[] _adjacencyList; // An array of doubly linked lists of integers, which represents the nabors of each node.

        public Graph(int numberOfVertices) {
            _v = numberOfVertices;
            _adjacencyList = new LinkedList<int>[_v];
            for (int i = 0; i < _v; i++) {
                // Creates a list for each node/vertices
                _adjacencyList[i] = new LinkedList<int>();
            }
        }

        public void AddEdge(int firstNode, int connectedNode) {
            _adjacencyList[firstNode].AddLast(connectedNode);
        }

        public Tuple<List<int>, List<int>> BreathFirstSearch(int startNode, int targetNode) {
            ValidateInput(startNode, targetNode);

            // Creates a dictionary of which node was visited from which to enable shortes path
            var previous = new Dictionary<int, int>();

            // Creates the route and path list to be returned in a tuple.
            var searchedOrder = new List<int>();
            var path = new List<int>();
            var result = new Tuple<List<int>, List<int>>(searchedOrder, path);

            // creates an array of boolean values with elements equal to the number of nodes. Default value is false.
            var visited = new bool[_v];

            // Create a queue for Breath First Search
            Queue<int> queue = new Queue<int>();

            // Mark the current node as visited and enqueue it.
            visited[startNode] = true;
            queue.Enqueue(startNode);

            while (queue.Count() != 0) {
                // Dequeue a vertex from queue and add it to the result list.
                int current = queue.Dequeue();
                searchedOrder.Add(current);

                // Go over all nodes in the nodes corresponding linked list (nabors to the node)
                // If a node in that list has not been visited, then mark it visited and enqueue it.
                // For each node queue the node is setting the current node as the one it was visited from.
                foreach (var node in _adjacencyList[current]) {
                    if (!visited[node]) {
                        visited[node] = true;
                        previous[node] = current;
                        queue.Enqueue(node);
                    }
                }
            }

            // As we now know the searched notes aswell as which node was being hit by which nabour 
            // we can make a path starting from the end of the route.
            var currentPathNode = targetNode;

            while (currentPathNode != startNode) {
                path.Add(currentPathNode);                      // Adding the node to the path
                currentPathNode = previous[currentPathNode];    // getting the node which was this node was visited from
            }

            path.Add(currentPathNode); // Should be the startnode we add here.

            path.Reverse();
            return result;
        }

        private void ValidateInput(int startNode, int targetNode) {
            if (startNode < 0 || startNode > _v) {
                throw new KeyNotFoundException("The startNode is not a member of the nodes in the graph.");
            }
            if (targetNode < 0 || targetNode > _v) {
                throw new KeyNotFoundException("The targetNode is not a member of the nodes in the graph.");
            }
        }

        public Tuple<List<int>, List<int>> DepthFirstSearch(int startNode, int targetNode) {
            ValidateInput(startNode, targetNode);
            var previous = new Dictionary<int, int>();
            var searchedOrder = new List<int>();
            var path = new List<int>();
            var result = new Tuple<List<int>, List<int>>(searchedOrder, path);
            var visited = new bool[_v];

            // Depth First uses a stack instead of a queue - otherwise it is semilar.
            Stack<int> stack = new Stack<int>();

            visited[startNode] = true;
            stack.Push(startNode);

            while (stack.Count != 0) {
                int current = stack.Pop();
                searchedOrder.Add(current);

                foreach (var node in _adjacencyList[current]) {
                    if (!visited[node]) {
                        visited[node] = true;
                        previous[node] = current;
                        stack.Push(node);
                    }
                }
            }

            var currentPathNode = targetNode;

            while (currentPathNode != startNode) {
                path.Add(currentPathNode);                      // Adding the node to the path
                currentPathNode = previous[currentPathNode];    // getting the node which was this node was visited from
            }

            path.Add(currentPathNode); // Should be the startnode we add here.
            path.Reverse();
            return result;
        }
    }
}
