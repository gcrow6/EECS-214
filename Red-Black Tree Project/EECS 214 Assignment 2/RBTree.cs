//Assignment 4 for EECS 214
//Out: May 7th, 2014
//Due: May 16th, 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_4
{
    public class RBTree:BST 
    {
        //Ensure the following
        //a. Class called RBNode that derives from the BSTNode 
        //b. Have a Fix-up function that fixes up the tree after each insertion
        //c. Ensure that you know what you are doing with the Nil nodes.
        //d. Have a function that calculates and returns the black height in a MessageBox
        //The syntax for showing a messagebox is: MessageBoxResult m = MessageBox.Show("hello folks");
        //Instead of the messagebox, if you want to show the black height as a label inside the visualizer, feel free to add it to the
        //xaml file

        public RBTree()
        {
            RBNode a = new RBNode(2, "red");
            RBNode b = new RBNode(14, "black");
            RBNode c = new RBNode(1, "black");
            RBNode d = new RBNode(7, "black");
            RBNode e = new RBNode(15, "red");
            RBNode f = new RBNode(5, "red");
            RBNode g = new RBNode(8, "red");
            RBNode r = new RBNode(11, "black");
            Root = r;
            Root.Left = a;
            Root.Right = b;
            a.Parent = Root;
            b.Parent = Root;
            a.Left = c;
            a.Right = d;
            b.Right = e;

            c.Parent = a;
            d.Parent = a;
            e.Parent = b;
            d.Left = f;
            d.Right = g;
            f.Parent = d;
            g.Parent = d;
            b.Left = Nil;
            c.Left = Nil;
            c.Right = Nil;
            e.Left = Nil;
            e.Right = Nil;
            f.Right = Nil;
            f.Left = Nil;
            g.Right = Nil;
            g.Left = Nil;

            Inorder((RBNode)Root);
        }

        //private RBNode root;

        public RBNode Nil = new RBNode(-6, "black");

        //public enum myColor { red, black };

        public List<RBNode> Nodes = new List<RBNode>();

        /*public RBNode Root
        {
            get { return root; }
            set 
            { 
                root = value;
            }
        }*/

        public void Inorder(RBNode n)
        {
            if (n.Left != Nil)
                Inorder((RBNode)n.Left);
            System.Diagnostics.Debug.WriteLine(n.Key + " , " + n.Color);
            Nodes.Add(n);
            if (n.Right != Nil)
                Inorder((RBNode)n.Right);
        }

        public void Insert(RBTree t, RBNode n)
        {
            n = ((RBNode)base.Insert(t, n));
            n.Left = t.Nil;
            n.Right = t.Nil;
            if (n != t.Root)
                n.Color = "red";
            FixUp(t, n);
            Nodes.Clear();
            Nodes.Add(n);            
        }

        public void FixUp(RBTree t, RBNode n)
        {
            while (String.Equals((string)((RBNode)n.Parent).Color, "red"))
            {
                if (n.Parent == n.Parent.Parent.Left)
                {
                    RBNode y = (RBNode)n.Parent.Parent.Right;
                    if (String.Equals((string)y.Color, "red"))
                    {
                        ((RBNode)n.Parent).Color = "black";
                        y.Color = "black";
                        ((RBNode)n.Parent.Parent).Color = "red";
                        n = (RBNode)n.Parent.Parent;
                    }
                    else
                    {
                        if (n == n.Parent.Right)
                        {
                            n = (RBNode)n.Parent;
                            RotateLeft(t, n);
                        }
                        ((RBNode)n.Parent).Color = "black";
                        ((RBNode)n.Parent.Parent).Color = "red";
                        RotateRight(t, n.Parent.Parent);
                    }
                }
                else
                {
                    RBNode y = (RBNode)n.Parent.Parent.Left;
                    if (String.Equals((string)y.Color, "red"))
                    {
                        ((RBNode)n.Parent).Color = "black";
                        y.Color = "black";
                        ((RBNode)n.Parent.Parent).Color = "red";
                        n = (RBNode)n.Parent.Parent;
                    }
                    else
                    {
                        if (n == n.Parent.Left)
                        {
                            n = (RBNode)n.Parent;
                            RotateLeft(t, n);
                        }
                        ((RBNode)n.Parent).Color = "black";
                        ((RBNode)n.Parent.Parent).Color = "red";
                        RotateRight(t, n.Parent.Parent);
                    }
                }
            }
            RBNode root = (RBNode)t.Root;
            root.Color = "black";

        }

        public int BlackHeight(RBTree t, RBNode r)
        {
            int count = 0;
            RBNode n = (RBNode)t.Root;
            while ((int)n.Key != (int)r.Key)
            {
                if ((int)r.Key < (int)n.Key)
                {
                    n = (RBNode)n.Left;
                }
                else
                {
                    n = (RBNode)n.Right;
                }
            }
            while (n != Nil)
            {
                if (n.Left == Nil || n.Right == Nil)
                {
                    count++;
                    break;
                }
                else
                {
                    n = (RBNode)n.Right;
                    if (String.Equals((string)n.Color, "black"))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public class RBNode : BSTNode
        {
            private object color;

            public object Color
            {
                get { return color; }
                set { color = value; }
            }

            public RBNode()
            {
            }

            public RBNode(object val, object o)
            {
                Key = val;
                Color = o;
            }
        }
    }
}
