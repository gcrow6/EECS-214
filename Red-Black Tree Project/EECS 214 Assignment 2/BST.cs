//Assignment 4 for EECS 214
//Out: May 7th, 2014
//Due: May 16th, 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_4
{
	public class BST 
	{
		//Put everything you wrote from assignment 3 here. But most importantly, have InOrder traversal.
		//Make sure you also have:
		//a. Class called BSTNode, make it public (and put it outside the BST class, so that the RBNode can derive from the BSTNode class) 
		//b. Write left-rotate and right-rotate functions
		//c. Write a function for inserting a node in the BST

		private BSTNode[] nodes;

		private BSTNode root;

		public BST()
			{
				nodes = new BSTNode[9];

				for (int i = 0; i < nodes.Length; i++)
				{
					BSTNode temp = new BSTNode();
					nodes[i] = temp;
				}
				nodes[0].Key = 1;
				nodes[1].Key = 2;
				nodes[2].Key = 3;
				nodes[3].Key = 4;
				nodes[4].Key = 5;
				nodes[5].Key = 6;
				nodes[6].Key = 7;
				nodes[7].Key = 8;
				nodes[8].Key = 9;

				root = nodes[7];

				nodes[1].Parent = nodes[7];
				nodes[8].Parent = nodes[7];
				nodes[0].Parent = nodes[1];
				nodes[5].Parent = nodes[1];
				nodes[3].Parent = nodes[5];
				nodes[6].Parent = nodes[5];
				nodes[2].Parent = nodes[3];
				nodes[4].Parent = nodes[3];

				nodes[7].Left = nodes[1];
				nodes[7].Right = nodes[8];
				nodes[1].Left = nodes[0];
				nodes[1].Right = nodes[5];
				nodes[5].Left = nodes[3];
				nodes[5].Right = nodes[6];
				nodes[3].Left = nodes[2];
				nodes[3].Right = nodes[4];
        }

		public BSTNode[] Nodes
		{
			get { return nodes; }
		}

		public BSTNode Root
		{
			get { return root; }
			set { root = value; }
		}

        public BSTNode Insert(BST t, BSTNode n)
        {
            if (t.Root == null)
            {
                t.Root = n;
                return n;
            }
            BSTNode parent = FindInsertionPoint(t.Root, n.Key);
            n.Parent = parent;
            if ((int)n.Key < (int)parent.Key)
            {
                parent.Left = n;
                n.Parent = parent;
            }
            else
            {
                parent.Right = n;
                n.Parent = parent;
            }
            return n;
        }

        public BSTNode FindInsertionPoint(BSTNode n, object k)
        {
            BSTNode parent = null;
            while (n != null && (int)n.Key != -6)
            {
                parent = n;
                if ((int)k < (int)n.Key)
                {
                    n = n.Left;
                }
                else
                {
                    n = n.Right;
                }
            }
            return parent;
        }

		public void Inorder(BSTNode n)
		{
			if (n.Left != null)
				Inorder(n.Left);
			System.Diagnostics.Debug.WriteLine(n.Key);
			if (n.Right != null)
				Inorder(n.Right);
		 }

		 public object Minimum(BSTNode n)
		 {
			while (n.Left != null)
				n = n.Left;
			return n.Key;
		 }

		public object Maximum(BSTNode n)
		{
			while (n.Right != null)
				n = n.Right;
			return n.Key;
		}

		public object Successor(BSTNode n)
		{
			if (n.Right != null)
				return Minimum(n.Right);
			BSTNode p = new BSTNode();
			p = n.Parent;
			while (p != null && n == p.Right)
			{
				n = p;
				p = n.Parent;
			}
			return p.Key;
		}

		public void RotateRight(BST t, BSTNode b)
		{
			BSTNode a = b.Left;
			b.Left = a.Right;
            if (a.Right != null)
                a.Right.Parent = b;
			a.Parent = b.Parent;
            if (b.Parent == null)
                t.Root = a;
            else if (b == b.Parent.Right)
                b.Parent.Right = a;
            else
                b.Parent.Left = a;
            a.Right = b;
            b.Parent = a;
		}

        public void RotateLeft(BST t, BSTNode a)
        {
            BSTNode b = a.Right;
            a.Right = b.Left;
            if (b.Left != null)
                b.Left.Parent = a;
            b.Parent = a.Parent;
            if (a.Parent == null)
                t.Root = b;
            else if (a == a.Parent.Left)
                a.Parent.Left = b;
            else
                a.Parent.Right = b;
            b.Left = a;
            a.Parent = b;
        }

		public class BSTNode
		{
			private BSTNode parent;
			private BSTNode left;
			private BSTNode right;
			private object key;

			public BSTNode Parent
			{
				get { return parent; }
				set { parent = value; }
			}

			public BSTNode Left
			{
				get { return left; }
				set { left = value; }
			}

			public BSTNode Right
			{
				get { return right; }
				set { right = value; }
			}

			public object Key
			{
				get { return key; }
				set { key = value; }
			}

		}
	}
}
