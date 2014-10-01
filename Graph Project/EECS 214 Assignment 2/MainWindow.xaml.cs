//Assignment 7 for EECS 214
//Out: May 30rd, 2014
//Due: June 6th, 2014
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_7
{
    /// <summary>
    /// THIS SHOULD REMAIN VERY SIMILAR TO ASSIGNMENT 6, INSTEAD OF CALLING BFS for SHORTEST PATH, CALL DIJKSTRA's 
    /// ALGORITHM 
    /// </summary>
    ///Don't worry about this being defined as a partial class, a partial class just allows you to split up code in 
    ///different files while telling the compiler to splice them together during compilation
    public partial class MainWindow : Window
    {
        List<Point> position = new List<Point>();
        List<String> name = new List<String>();
        Graph myG = new Graph();
        Graph Conn = new Graph(1);
        public MainWindow()
        {
            //myG.BFS(myG.Nodes[0], myG.Nodes[6]);
            Conn.ConnComp();
            InitializeComponent();
            
            //Registering callback for the route finding button
            searchRouteBtn.Click += searchRouteBtn_Click;

            forDrawingExample();
            drawStructure();
        }

        //Remove this function, I just use this to add points and labels for the visualizer
        void forDrawingExample()
        {
            //Add coordinates for the drawing 
            position.Add(new Point(25, 50));
            position.Add(new Point(25, 175));
            position.Add(new Point(270, 50));
            position.Add(new Point(500, 50));
            position.Add(new Point(500, 300));
            position.Add(new Point(270, 300));
            position.Add(new Point(270, 175));
            position.Add(new Point(25, 300));

            //Add names of cities
            name.Add("San Francisco");
            name.Add("Los Angeles");
            name.Add("Chicago");
            name.Add("New York");
            name.Add("Miami");
            name.Add("Dallas");
            name.Add("Denver");
            name.Add("San Diego");
            
        }

        void searchRouteBtn_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            //Get the to and from airports from the fromInput and toInput textboxes in MainWindow.xaml.cs
            //Find the cheapest flight path and draw it using drawstructure
            Graph.GraphNode node = new Graph.GraphNode(fromInput.Text);
            Graph.GraphNode node2 = new Graph.GraphNode(toInput.Text);
            if (fromInput.Text != "" && toInput.Text != "")
            {
                SolidColorBrush hBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                List<Graph.GraphNode> myList = myG.DijkstraShortestPath(node, node2);
                List<Graph.GraphNode> myL = new List<Graph.GraphNode>();
                for (int c = (myList.Count - 1); c >= 0; c--)
                {
                    myL.Add(myList[c]);
                }
                for (int i = 0; i < myL.Count - 1; i++)
                {
                    for (int j = i + 1; j < myL.Count; j++)
                    {
                        List<int> list = myG.values(myL[i], myL[j]);
                        drawStructure();
                        drawStructure(hBrush, list[0], list[1], myL);
                        break;
                    }
                }
                fromInput.Text = "";
                toInput.Text = "";
            }
            else
            {
                MessageBoxResult m = MessageBox.Show("Type Value First Please");
            }
        }


        //Draw graph for the flight paths
        private void drawStructure()
        {
            canvas.Children.Clear();
            //How to draw lines. I've hardcoded one, you can an edge to calculate X1, X2, Y1, Y2   
            /*Line connector = new Line();
            connector.X1 = position[0].X + 30 / 2; //30 is the width of the circle/node drawn
            connector.Y1 = position[0].Y + 30 / 2; //30 is the height of the circle/node drawn;
            connector.X2 = position[2].X + 30 / 2;
            connector.Y2 = position[2].Y + 30 / 2; 
            connector.StrokeThickness = 2;
            connector.Stroke = new SolidColorBrush(Color.FromRgb(150, 150, 175));
            canvas.Children.Add(connector);*/

            //Feel free to use whatever colours you want
            //I've iterated through the position and name lists here just to give an example
            //you'll iterate through the graphnodes in your graph
            //int counter = 0;
            foreach (Graph.GraphNode p in myG.Nodes)
            {
                foreach (Graph.GraphNode n in p.Neighbors)
                {
                    Line connector = new Line();
                    connector.X1 = p.Position.X + 30 / 2; //30 is the width of the circle/node drawn
                    connector.Y1 = p.Position.Y + 30 / 2; //30 is the height of the circle/node drawn;
                    connector.X2 = n.Position.X + 30 / 2;
                    connector.Y2 = n.Position.Y + 30 / 2;
                    connector.StrokeThickness = 2;
                    connector.Stroke = new SolidColorBrush(Color.FromRgb(150, 150, 175));
                    canvas.Children.Add(connector);
                }
            }

            foreach (Graph.GraphNode p in myG.Nodes)
            {
                Ellipse e = new Ellipse();
                e.Height = 30;
                e.Width = 30;
                e.Fill = new SolidColorBrush(Color.FromRgb(139, 10, 205));
                e.StrokeThickness = 7.5;
                e.Stroke = new SolidColorBrush(Color.FromRgb(200, 200, 200));
                Canvas.SetLeft(e, p.Position.X);
                Canvas.SetTop(e, p.Position.Y);
                canvas.Children.Add(e);

                Label l = new Label();
                l.Content = p.Key;
                Canvas.SetLeft(l, p.Position.X);
                Canvas.SetTop(l, p.Position.Y - 20);

                canvas.Children.Add(l);
                //counter++;
            }

        }

        private void drawStructure(SolidColorBrush s, int i, int j, List<Graph.GraphNode> keys)
        {
            canvas.Children.Clear();
            //How to draw lines. I've hardcoded one, you can an edge to calculate X1, X2, Y1, Y2   
            /*Line connector = new Line();
            connector.X1 = position[0].X + 30 / 2; //30 is the width of the circle/node drawn
            connector.Y1 = position[0].Y + 30 / 2; //30 is the height of the circle/node drawn;
            connector.X2 = position[2].X + 30 / 2;
            connector.Y2 = position[2].Y + 30 / 2; 
            connector.StrokeThickness = 2;
            connector.Stroke = new SolidColorBrush(Color.FromRgb(150, 150, 175));
            canvas.Children.Add(connector);*/

            //Feel free to use whatever colours you want
            //I've iterated through the position and name lists here just to give an example
            //you'll iterate through the graphnodes in your graph
            //int counter = 0;
            foreach (Graph.GraphNode p in myG.Nodes)
            {
                foreach (Graph.GraphNode n in p.Neighbors)
                {
                    Line connector = new Line();
                    connector.X1 = p.Position.X + 30 / 2; //30 is the width of the circle/node drawn
                    connector.Y1 = p.Position.Y + 30 / 2; //30 is the height of the circle/node drawn;
                    connector.X2 = n.Position.X + 30 / 2;
                    connector.Y2 = n.Position.Y + 30 / 2;
                    connector.StrokeThickness = 2;
                    connector.Stroke = new SolidColorBrush(Color.FromRgb(150, 150, 175));
                    canvas.Children.Add(connector);
                }
            }

            int count = 0;
            Point start;
            Point end;
            foreach (Graph.GraphNode selectedNodes in keys)
            {
                count++;
                if (count < keys.Count)
                {
                    start = selectedNodes.Position;
                    end = keys[count].Position;
                    Line connector = new Line();
                    connector.X1 = start.X + 30 / 2; //30 is the width of the circle/node drawn
                    connector.Y1 = start.Y + 30 / 2; //30 is the height of the circle/node drawn;
                    connector.X2 = end.X + 30 / 2;
                    connector.Y2 = end.Y + 30 / 2;
                    connector.StrokeThickness = 5;
                    connector.Stroke = new SolidColorBrush(Color.FromRgb(255, 150, 175));
                    canvas.Children.Add(connector);
                }

            }

            foreach (Graph.GraphNode p in myG.Nodes)
            {
                foreach (Graph.GraphNode n in p.Neighbors)
                {

                }
            }

            foreach (Graph.GraphNode p in myG.Nodes)
            {
                Ellipse e = new Ellipse();
                e.Height = 30;
                e.Width = 30;
                foreach (Graph.GraphNode g in keys)
                {
                    if (g != null)
                    {
                        if ((string)p.Key == (string)g.Key)
                            e.Fill = s;
                    }
                }
                e.Fill = new SolidColorBrush(Color.FromRgb(139, 10, 205));
                e.StrokeThickness = 7.5;
                e.Stroke = new SolidColorBrush(Color.FromRgb(200, 200, 200));
                Canvas.SetLeft(e, p.Position.X);
                Canvas.SetTop(e, p.Position.Y);
                canvas.Children.Add(e);

                Label l = new Label();
                l.Content = p.Key;
                Canvas.SetLeft(l, p.Position.X);
                Canvas.SetTop(l, p.Position.Y - 20);

                canvas.Children.Add(l);
                //counter++;
            }
        }
    }
}
