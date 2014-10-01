//Assignment 4 for EECS 214
//Out: May 7th, 2014
//Due: May 16th, 2014

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

namespace Assignment_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///Don't worry about this being defined as a partial class, a partial class just allows you to split up code in 
    ///different files while telling the compiler to splice them together during compilation
    public partial class MainWindow : Window
    {
        int top = 150, left = 0;
        RBTree myT = new RBTree();
        BST b = new BST();
        public MainWindow()
        {
            
            InitializeComponent();
            /*BST.BSTNode bn = new BST.BSTNode();
            bn.Key = 10;
            //b.Insert(b, bn);
            //b.Inorder(b.Root);
            RBTree.RBNode n = new RBTree.RBNode(4, "red");
            
            myT.Insert(myT, n);
            myT.Inorder(myT.Root);*/
                
            //Registering callbacks for the 3 buttons
            searchBtn.Click += searchBtn_Click;
            insertBtn.Click += insertBtn_Click;
            bhBtn.Click += bhBtn_Click;
            drawStructure();

            //Insert your code here, read the comments in BST.cs for implementation details
            //Things that should work
            //a. When the program runs, draw the tree as an inorder traversal (i.e. sorted list) of boxes horizontally. 
            //   Same as assignment 3
            //b. If I search for a node that doesn't exist in the tree, a Message Box should pop-up saying
            //   that the node was not found
            //c. If I search for a node that DOES exist in the tree, change its stroke color to highlight where it is
            //   in the sorted list (i.e. horizontal visualization of the inorder traversal of the RBT).
            //   N.B. don't change the colour of the fill(should be red or black), only change the outlines to highlight
            //d. Clicking on the insert button should insert the value/key in the input box. Do proper validation (numbers only, empty box etc.)
            //e. RECOMMENDATION: Use the red and black colors defined in the drawstructure function for the red and black nodes,
            //   While we encourage creative freedom, grading this assignment would kill me and Aleck if we
            //   see different shades of red and black from all of you :).
            
        }

        void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (input.Text == "")
            {
                MessageBoxResult m = MessageBox.Show("Type Value First Please");
            }
            else
            {
                RBTree.RBNode n = new RBTree.RBNode();
                n.Key = Convert.ToInt32(input.Text);
                myT.Insert(myT, n);
                myT.Nodes.Clear();
                myT.Inorder((RBTree.RBNode)myT.Root);
                drawStructure();
                input.Text = "";
            }
        }

        //Search for an element in the RBTree
        void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush hBrush = new SolidColorBrush(Color.FromArgb(250, 139, 0, 205));
            if (input.Text != "")
            {
                drawStructure(hBrush, Convert.ToInt32(input.Text));
                input.Text = "";
            }
            else
            {
                MessageBoxResult m = MessageBox.Show("Type Value First Please");
            }
        }

        void bhBtn_Click(object sender, RoutedEventArgs e)
        {
            if (input.Text == "")
            {
                MessageBoxResult message = MessageBox.Show("Type Value First"); //no input
            }
            else
            {
                RBTree.RBNode n = new RBTree.RBNode();
                n.Key = Convert.ToInt32(input.Text);
                string s = (myT.BlackHeight(myT, n)).ToString();
                MessageBoxResult m = MessageBox.Show(s);
                input.Text = "";
            }
        }

        //Draw the inorder traversal (i.e. sorted list) of BST you've defined
        //Parameterize it if necessary
        private void drawStructure()
        {
            canvas.Children.Clear();
            //Brush for Red nodes
            SolidColorBrush redBrush = new SolidColorBrush(Color.FromArgb(90, 201, 56, 76));
            //Brush for Black nodes
            SolidColorBrush blackBrush = new SolidColorBrush(Color.FromArgb(90, 11, 32, 56));
            SolidColorBrush rStrBrush = new SolidColorBrush(Color.FromArgb(100, 46, 59, 74));
            SolidColorBrush lBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //We are drawing a square block for each element in our Stack
            for (int i = 0; i < myT.Nodes.Count; i++)
            {
                //Think about this drawing exercise as your having brushes dipped in different types of paint
                //And you use those brushes to paint a rectangle
                Rectangle r = new Rectangle();
                r.Width = 40;
                r.Height = 40;

                if (String.Equals((string)(myT.Nodes[i].Color), "red")) //highlight the key
                    r.Fill = redBrush;
                else
                    r.Fill = blackBrush; //The brush and it's color is defined earlier (Line 82). This is the color inside the rectangle
                r.StrokeThickness = 2; //This defines the thickness of the outline of each rectangular block
                r.Stroke = rStrBrush; //Defines the color of the block

                Label value = new Label();//It's all about objects! A label is an object that can contain text
                value.Width = r.Width;//We have to define how wide the label can go, otherwise the text can overflow from the rectangle
                value.Height = r.Height;
                value.Content = myT.Nodes[i].Key;//Read the i-th element in the stack
                value.FontSize = 12;
                value.Foreground = lBrush; //Again, consider that text is also painted on, the paint color is specified in line 84
                value.HorizontalContentAlignment = HorizontalAlignment.Center; //We are just centering the text horizontally and vertically
                value.VerticalContentAlignment = VerticalAlignment.Center;

                canvas.Children.Add(r); //Add the rectangle 
                Canvas.SetLeft(r, left + i * r.Width); //Set the left (i.e. x-coordinate of the rectangle)
                Canvas.SetTop(r, top); //set the position of the rectangle in the canvas

                //Add the text. Note, if you've added the text before the rectangle, the text would have
                //been occluded by the rectangle. 
                //So the order of drawing things matter. The things you draw later, are the ones that are layered on top of the previous ones
                canvas.Children.Add(value);
                Canvas.SetTop(value, top);
                Canvas.SetLeft(value, Canvas.GetLeft(r));
            }
            //Brush for Red nodes
            //SolidColorBrush redBrush = new SolidColorBrush(Color.FromArgb(90, 201, 56, 76));
            //Brush for Black nodes
            //SolidColorBrush blackBrush = new SolidColorBrush(Color.FromArgb(90, 11, 32, 56));
        }

        private void drawStructure(SolidColorBrush hbrush, int key)
        {
            canvas.Children.Clear();
            //Brush for Red nodes
            SolidColorBrush redBrush = new SolidColorBrush(Color.FromArgb(90, 201, 56, 76));
            //Brush for Black nodes
            SolidColorBrush blackBrush = new SolidColorBrush(Color.FromArgb(90, 11, 32, 56));
            SolidColorBrush rStrBrush = new SolidColorBrush(Color.FromArgb(100, 46, 59, 74));
            SolidColorBrush lBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            SolidColorBrush b = hbrush;

            //We are drawing a square block for each element in our Stack
            for (int i = 0; i < myT.Nodes.Count; i++) //draw 9 squares
            {
                //Think about this drawing exercise as your having brushes dipped in different types of paint
                //And you use those brushes to paint a rectangle
                Rectangle r = new Rectangle();
                r.Width = 40;
                r.Height = 40;

                if ((int)myT.Nodes[i].Key == key)
                    r.Fill = b;
                else if (String.Equals((string)(myT.Nodes[i].Color), "red")) //highlight the key
                    r.Fill = redBrush;
                else if (String.Equals((string)(myT.Nodes[i].Color), "black"))
                    r.Fill = blackBrush; //The brush and it's color is defined earlier (Line 82). This is the color inside the rectangle
                r.StrokeThickness = 2; //This defines the thickness of the outline of each rectangular block
                r.Stroke = rStrBrush;
                Label value = new Label();//It's all about objects! A label is an object that can contain text
                value.Width = r.Width;//We have to define how wide the label can go, otherwise the text can overflow from the rectangle
                value.Height = r.Height;
                value.Content = myT.Nodes[i].Key;//Read the i-th element in the stack
                value.FontSize = 12;
                value.Foreground = lBrush; //Again, consider that text is also painted on, the paint color is specified in line 84
                value.HorizontalContentAlignment = HorizontalAlignment.Center; //We are just centering the text horizontally and vertically
                value.VerticalContentAlignment = VerticalAlignment.Center;

                canvas.Children.Add(r); //Add the rectangle 
                Canvas.SetLeft(r, left + i * r.Width); //Set the left (i.e. x-coordinate of the rectangle)
                Canvas.SetTop(r, top); //set the position of the rectangle in the canvas

                //Add the text. Note, if you've added the text before the rectangle, the text would have
                //been occluded by the rectangle. 
                //So the order of drawing things matter. The things you draw later, are the ones that are layered on top of the previous ones
                canvas.Children.Add(value);
                Canvas.SetTop(value, top);
                Canvas.SetLeft(value, Canvas.GetLeft(r));
            }
        }
    }
}
