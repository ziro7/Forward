using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.Models
{
    public class Graph
    {
        private int _v; // Number of vertices. Which is the nodes in the graph
        private LinkedList<int>[] _adjacencyList; // An array of doubly linked lists of integers, which represents the nabors of each node.


        /*
         Tænker jeg skal lave en Class med noder - STog eller lign. med et navn og nummer.
         Så skal adjacencylist holde en list a tupler med stations nummer og distance.
         Vil det virke?
             */

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

        public Tuple<List<int>,List<int>> BreathFirstSearch(int startNode, int targetNode) {

            // Validate that targetNode is in the graph - sme with startnode
            //TODO

            // Creates a dictionary of which node was visited from which to enable shortes path
            var previous = new Dictionary<int, int>();
            
            // Creates the route and path list to be returned in a tuple.
            var searchedNodes = new List<int>();
            var path = new List<int>();
            var result = new Tuple<List<int>, List<int>>(searchedNodes,path);

            // creates an array of boolean values with elements equal to the number of nodes. Default value is false.
            Boolean[] visited = new Boolean[_v];

            // Create a queue for Breath First Search
            Queue<int> queue = new Queue<int>();

            // Mark the current node as visited and enqueue it.
            visited[startNode] = true;
            queue.Enqueue(startNode);

            while(queue.Count() != 0) {
                // Dequeue a vertex from queue and add it to the result list.
                int current = queue.Dequeue();
                searchedNodes.Add(current);

                if(current == targetNode) { break; }

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

            // As we now know the searched notes aswell as which node was being hit by which nabor 
            // we can make a path starting from the end of the route.
            var currentPathNode = targetNode;

            //do {
            //    path.Add(currentPathNode);                      // Adding the node to the path
            //    currentPathNode = previous[currentPathNode];    // getting the node which was this node was visited from
            //} while (currentPathNode != startNode);

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
