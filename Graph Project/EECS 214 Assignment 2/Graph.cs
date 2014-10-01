//Assignment 7 for EECS 214
//Out: May 30rd, 2014
//Due: June 6th, 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Assignment_7
{
    //public class Graph : IEnumerable<int>
    public class Graph
    {
        //ENSURE THAT YOU HAVE THE ":IComparable<GraphNode>" below
        public class GraphNode : IComparable<GraphNode>
        {
            //ALL THE STUFF FROM ASSIGNMENT 6 and ONE ADDITIONAL FIELD, cost 
            //Of course, if you need other variables, feel free to add them
            private object key;
            private List<int> weights = new List<int>();
            private List<GraphNode> neighbors = new List<GraphNode>();
            private int distance;
            private GraphNode predecessor;
            private bool visited = false;
            private int component;
            private int cost;
            private Point position;


            public Point Position
            {
                get { return position; }
                set { position = value; }
            }

            public GraphNode(object value)
            {
                Key = value;
            }

            public GraphNode()
            {
            }

            public object Key
            {
                get { return key; }
                set { key = value; }
            }

            public List<int> Weights
            {
                get { return weights; }
                set { weights = value; }
            }

            public List<GraphNode> Neighbors
            {
                get { return neighbors; }
                set { neighbors = value; }
            }

            public int Distance
            {
                get { return distance; }
                set { distance = value; }
            }

            public GraphNode Predecessor
            {
                get { return predecessor; }
                set { predecessor = value; }
            }

            public bool Visited
            {
                get { return visited; }
                set { visited = value; }
            }

            public int Component
            {
                get { return component; }
                set { component = value; }
            }
            //Property to get and set cost
            public int Cost
            {
                get { return cost; }
                set { cost = value; }
            }

            //YOU AVE TO INCLUDE THIS COMPARETO FUNCTION BECAUSE 
            //THE GRAPHNODE IMPLEMENTS THE ICOMPARABLE INTERFACE
            //DONT WORRY ABOUT IT, ITS A C# WAY OF COMPARING OBJECTS
            //TO RETURN IF AN OBJECT IS GREATER THAN OR LESS THAN ANOTHER OBJECT
            public int CompareTo(GraphNode other)
            {
                if (this.cost < other.cost) return -1;
                else if (this.cost > other.cost) return 1;
                else return 0;
            }
        }

        //YOU SHOULD HAVE ALL THE STUFF FROM ASSIGNMENT 6 and ADD DIJKSTRA'S ALGORITHM to the GRAPH CLASS
        private List<GraphNode> nodes = new List<GraphNode>();

        public Graph()
        {
            GraphNode NY = new GraphNode("New York");
            NY.Position = new Point(500, 50);
            Nodes.Add(NY);
            GraphNode CHI = new GraphNode("Chicago");
            CHI.Position = new Point(270, 50);
            Nodes.Add(CHI);
            GraphNode DEN = new GraphNode("Denver");
            DEN.Position = new Point(270, 175);
            Nodes.Add(DEN);
            GraphNode MIA = new GraphNode("Miami");
            MIA.Position = new Point(500, 300);
            Nodes.Add(MIA);
            GraphNode DAL = new GraphNode("Dallas");
            DAL.Position = new Point(270, 300);
            Nodes.Add(DAL);
            GraphNode SD = new GraphNode("San Diego");
            SD.Position = new Point(25, 300);
            Nodes.Add(SD);
            GraphNode LA = new GraphNode("Los Angeles");
            LA.Position = new Point(25, 175);
            Nodes.Add(LA);
            GraphNode SF = new GraphNode("San Francisco");
            SF.Position = new Point(25, 50);
            Nodes.Add(SF);

            AddDirectedEdge(NY, CHI, 80);
            AddDirectedEdge(NY, DEN, 100);
            AddDirectedEdge(NY, MIA, 90);
            AddDirectedEdge(NY, DAL, 125);
            AddDirectedEdge(CHI, DEN, 25);
            AddDirectedEdge(CHI, SF, 60);
            AddDirectedEdge(DEN, LA, 100);
            AddDirectedEdge(DEN, SF, 90);
            AddDirectedEdge(MIA, DAL, 50);
            AddDirectedEdge(DAL, SD, 80);
            AddDirectedEdge(DAL, LA, 80);
            AddDirectedEdge(SD, LA, 50);
            AddDirectedEdge(SF, LA, 45);

            foreach (GraphNode n in Nodes)
                LA.Visited = false;
        }

        public Graph(int node)
        {
            GraphNode a = new GraphNode(1);
            AddNode(a);
            GraphNode b = new GraphNode(2);
            AddNode(b);
            GraphNode c = new GraphNode(3);
            AddNode(c);
            GraphNode d = new GraphNode(9);
            AddNode(d);
            GraphNode e = new GraphNode(8);
            AddNode(e);
            GraphNode f = new GraphNode(4);
            AddNode(f);
            GraphNode g = new GraphNode(7);
            AddNode(g);
            GraphNode h = new GraphNode(5);
            AddNode(h);
            GraphNode i = new GraphNode(6);
            AddNode(i);
            GraphNode j = new GraphNode(11);
            AddNode(j);
            GraphNode k = new GraphNode(12);
            AddNode(k);
            GraphNode l = new GraphNode(13);
            AddNode(l);

            AddUndirectedEdge(a, b, 1);
            AddUndirectedEdge(a, f, 1);
            AddUndirectedEdge(b, c, 1);
            AddUndirectedEdge(b, e, 1);
            AddUndirectedEdge(b, f, 1);
            AddUndirectedEdge(c, d, 1);
            AddUndirectedEdge(d, e, 1);
            AddUndirectedEdge(g, h, 1);
            AddUndirectedEdge(h, i, 1);
            AddUndirectedEdge(h, j, 1);
            AddUndirectedEdge(i, j, 1);
            foreach (GraphNode n in Nodes)
                g.Visited = false;
        }

        /// <summary>
        /// Should add the node to the list of nodes
        /// </summary>
        public void AddNode(GraphNode node)
        {
            // adds a node to the graph
            Nodes.Add(node);
        }

        /// <summary>
        /// Creates a new graphnode from an input value and adds it to the list of graph nodes
        /// </summary>
        public void AddNode(int value)
        {
            GraphNode n = new GraphNode(value);
            Nodes.Add(n);
        }

        /// <summary>
        /// Adds a directed edge from one GraphNode to another. 
        /// Also insert the corresponding weight 
        /// </summary>
        public void AddDirectedEdge(GraphNode from, GraphNode to, int weight)
        {
            from.Neighbors.Add(to);
            from.Weights.Add(weight);
        }

        /// <summary>
        /// Adds an undirected edge from one GraphNode to another. 
        /// Remember this means that both the from and to are neighbors of each other
        /// Also insert the corresponding weight 
        /// </summary>
        public void AddUndirectedEdge(GraphNode from, GraphNode to, int cost)
        {
            from.Neighbors.Add(to);
            to.Neighbors.Add(from);
            from.Weights.Add(cost);
            to.Weights.Add(cost);
        }

        //Check if a value exists in the graph or not
        public bool Contains(object value)
        {
            foreach (GraphNode element in Nodes)
            {
                if (value == element.Key)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Should remove a node and all its edges and return true if successful
        /// </summary>
        public bool Remove(object value)
        {
            int counter = -1;
            foreach (GraphNode element in Nodes)
            {
                if (value == element.Key)
                {
                    foreach (GraphNode n in element.Neighbors)
                    {
                        foreach (GraphNode node in n.Neighbors)
                        {
                            counter++;
                            if (node.Key == element.Key)
                            {
                                node.Neighbors.Remove(element);
                                node.Weights.RemoveAt(counter);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Is a property and returns the list of nodes, should be a one-liner
        /// </summary>
        public List<GraphNode> Nodes
        {
            //fill this up, otherwise it'll generate a property/indexer error
            get { return nodes; }
            set { nodes = value; }
        }

        /// <summary>
        /// Is a property and returns the number of nodes in the graph, should be one-liner
        /// </summary>
        public int Count
        {
            //fill this up, otherwise it'll generate a property/indexer error 
            get { return Nodes.Count; }
        }

        public List<GraphNode> BFS(GraphNode start, GraphNode end)
        {
            List<GraphNode> PathList = new List<GraphNode>();

            foreach (GraphNode g in Nodes)
            {
                if ((string)g.Key == (string)start.Key)
                    start = g;
                else if ((string)g.Key == (string)end.Key)
                    end = g;
            }
            Queue<GraphNode> myQ = new Queue<GraphNode>();
            myQ.Enqueue(start);
            start.Distance = 0;
            start.Predecessor = null;

            foreach (GraphNode g in nodes)
            {
                g.Predecessor = null;
                g.Visited = false;
            }
            start.Visited = true;
            while (myQ.Count != 0)
            {
                GraphNode node = myQ.Dequeue();
                for (int i = 0; i < node.Neighbors.Count; i++)
                {
                    GraphNode n = node.Neighbors[i];
                    if (n.Visited == false)
                    {
                        myQ.Enqueue(n);
                        /*if (node.Predecessor != null)
                        {
                            foreach (GraphNode p in node.Predecessor)
                                n.Predecessor.Add(p);
                        }*/
                        n.Predecessor = node;
                        //n.Predecessor.Add(node);
                        n.Distance = node.Distance + node.Weights[i];
                        n.Visited = true;
                    }
                    if (n.Key == end.Key)
                    {
                        System.Diagnostics.Debug.WriteLine(end.Key.ToString() + ", " + n.Distance.ToString());
                        /*for (int a = 0; a < n.Predecessor.Count; a++)
                            System.Diagnostics.Debug.WriteLine(n.Predecessor[a].Key);
                        PathList = n.Predecessor;*/
                        PathList.Add(n);
                        while (n != null)
                        {
                            System.Diagnostics.Debug.WriteLine(n.Key.ToString());
                            if (n.Predecessor != null)
                                PathList.Add(n.Predecessor);
                            n = n.Predecessor;
                        }
                        return PathList;
                        //return (int)end.Key;
                    }
                }
            }
            MessageBoxResult m = MessageBox.Show("Something went wrong");
            return PathList;
            //return 0;
        }

        public void ConnComp()
        {
            int comp = 0;
            foreach (GraphNode n in Nodes)
            {
                if (n.Visited == false)
                {
                    Visit(n, comp);
                    comp++;
                }
            }
            foreach (GraphNode node in Nodes)
            {
                System.Diagnostics.Debug.WriteLine(node.Key + ", component = " + node.Component);
            }
            System.Diagnostics.Debug.WriteLine("# of Connected Components = " + comp);
        }

        public void Visit(GraphNode n, int c)
        {
            n.Visited = true;
            n.Component = c;
            foreach (GraphNode node in n.Neighbors)
            {
                if (node.Visited == false)
                {
                    Visit(node, c);
                }
            }
        }

        public List<int> values(GraphNode start, GraphNode end)
        {
            List<int> l = new List<int>();
            l.Clear();
            l.Add(getValues(start));
            l.Add(getValues(end));
            return l;
        }

        public int getValues(GraphNode g)
        {
            if ((string)g.Key == "San Francisco")
                return 0;
            if ((string)g.Key == "Los Angeles")
                return 1;
            if ((string)g.Key == "Chicago")
                return 2;
            if ((string)g.Key == "New York")
                return 3;
            if ((string)g.Key == "Miami")
                return 4;
            if ((string)g.Key == "Dallas")
                return 5;
            if ((string)g.Key == "Denver")
                return 6;
            if ((string)g.Key == "San Diego")
                return 7;
            return 10;
        }

        /// Implementation of Djikstra's Algorithm. 

        public List<GraphNode> DijkstraShortestPath(GraphNode start, GraphNode finish)
        {
            List<GraphNode> returnList = new List<GraphNode>(); //contains the list of nodes to go from start to finish
            PriorityQueue<GraphNode> PQ = new PriorityQueue<GraphNode>();
            foreach (GraphNode g in Nodes)
            {
                if ((string)g.Key == (string)start.Key)
                    g.Cost = 0;
                else
                    g.Cost = 9999;
                PQ.Insert(g);
                g.Predecessor = null;
            }
            //start.Cost = 0;
            //PQ.Insert(start);
            while (PQ.Count() != 0) 
            {
                GraphNode u = PQ.ExtractMin();
                if ((string)u.Key == (string)finish.Key) //&& u.Predecessor != null)
                {
                    System.Diagnostics.Debug.WriteLine(finish.Key.ToString() + ", " + u.Cost.ToString());
                    returnList.Add(u);
                    GraphNode z = u;
                    while (z != null)
                    {
                        System.Diagnostics.Debug.WriteLine(z.Key.ToString());
                        if (z.Predecessor != null)
                            returnList.Add(z.Predecessor);
                        z = z.Predecessor;
                    }
                    return returnList;
                }
                for (int i = 0; i < u.Neighbors.Count; i++)
                {
                    GraphNode v = u.Neighbors[i];
                    int w = u.Weights[i];
                    int newCost = u.Cost + w;
                    if (newCost < v.Cost)
                    {
                        v.Cost = newCost;
                        PQ.DecreaseKey(PQ.Data.IndexOf(v));
                        v.Predecessor = u;
                    }
                }
            }
            return returnList;

        }
    }
}
