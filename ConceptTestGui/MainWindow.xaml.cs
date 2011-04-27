using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ecologylab.serialization;
using wikxplorer.messages;
using System.Threading.Tasks;
using Net.Kniaz.LMA;
using Net.Kniaz.LMA.Tests;
using DotNetMatrix;

namespace ConceptTestGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TranslationScope ts;
        OODSSClient client;
        public MainWindow()
        {
            InitializeComponent();


            System.Console.WriteLine("Here be the start");

            //AsynchronousClient client = new AsynchronousClient("127.0.0.1",7833);
            
            
            //client = new OODSSClient("achilles.cse.tamu.edu", 11355);
            client = new OODSSClient("ecolab-chevron-1.cse.tamu.edu", 11355);
            
            

            ElementState s = null;// client.GetResponse(new InitConnectionRequest()).Result;
            //Console.WriteLine(s.GetType());
            Console.WriteLine("Press enter to update context ");            

            ts = WikxplorerMessageTranslationScope.Get();
            /*
            UpdateContextRequest uc = new UpdateContextRequest();
            uc.Action = 1;//add
            uc.Title = "Creativity";
            s = client.GetResponse(uc, ts).Result;
            Console.WriteLine(s.GetType());

            Console.WriteLine("Press enter to get a related request");
            Console.ReadLine();

            RelatednessRequest rr = new RelatednessRequest();
            rr.Source = "Cognitive science";
            s = client.GetResponse(rr, ts).Result;
            Console.WriteLine(s.GetType());


            Console.WriteLine("Press enter to get a suggestion request");
            Console.ReadLine();

            SuggestionRequest sr = new SuggestionRequest();
            sr.Source = "Information visualization";
            s = client.GetResponse(sr, ts).Result;
            Console.WriteLine(s.GetType());
             */
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Adding "+textBox1.Text);
            UpdateContextRequest uc = new UpdateContextRequest();
            uc.Action = 1;//add
            uc.Title = textBox1.Text;//"Creativity";
            Task<ElementState> theTask = client.GetResponse(uc, ts);
            ElementState st = theTask.Result;
            Console.WriteLine(st.GetType());
            
        }


        

        List<TextBlock> onPlate = new List<TextBlock>();



        public class SingleRelivant : LMAFunction
        {
            
            public List<Point> pos;
            public List<double> rel;
            public double rad;
            
            /// <summary>
            /// Returns Gaussian values
            /// </summary>
            /// <param name="x">x value</param>
            /// <param name="a">parameters</param>
            /// <returns></returns>
            public override double GetY(double x, double[] a)
            {
                double y = 0;
                double px = Math.Cos(a[0]) * rad;
                double py = Math.Sin(a[0]) * rad;

                for (int i = 0; i < pos.Count; i++)
                {
                    double dist = (px-pos[i].X)*(px-pos[i].X) + (py-pos[i].Y)*(py-pos[i].Y);
                    //dist = Math.Log10(dist);
                    dist = (dist * dist) / (rad * 5 * rad * 5);
                    y += dist*rel[i];
                }
Console.WriteLine(y + "    " + a[0]);
                return y;
            }
        }


        private double getAngle(List<Point> pos, List<double> rel, double rad)
        {
            double[] x = { 0 , 0};//these are all minimized...  each would add another constraint?
            double[] a = { .35 , 0 };//these are all the initial position

            double[] aderp = { .35, 0 };//these are all the initial position

            SingleRelivant sr = new SingleRelivant();
            LMAFunction f = sr;
            sr.rad = rad;
            sr.rel = rel;
            sr.pos = pos;

            Random r = new Random();
            double best = 10000000;
            double bestangle = 0;
            for(int i=0; i<100; i++)
            {
                double ang = r.NextDouble() *2.0 *Math.PI - Math.PI;

                aderp[0] = ang;
                double res = sr.GetY(0, aderp);
                if (res < best)
                {
                    best = res;
                    bestangle = ang;
                }
            }
            /*

            //double[][] dataPoints = f.GenerateData(a, x);
            double[][] dataPoints = f.GenerateData(a, x);

            LMA algorithm = new LMA(f, new double[] { .5, -1 },
                dataPoints, null, new GeneralMatrix(2, 2), .2 , 15);//1d - 20

            //new double[][] {xValues, yValues};

            algorithm.Fit();
            */

            return bestangle;// a[0];
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Checking Related to "+textBox2.Text);
            RelatednessRequest rr = new RelatednessRequest();
            rr.Source = textBox2.Text;//"Cognitive science";
            Task<ElementState> theTask = client.GetResponse(rr, ts);
            RelatednessResponse s = (RelatednessResponse)theTask.Result;
            
            String ret = "";
            foreach (String c in s.Targets.Keys)
            {
                ret += s.Targets[c].Title + " " + s.Targets[c].Relatedness+",";
            }
            label1.Content = ret;
            Console.WriteLine(s.GetType());

            if (onPlate.Count > 1)
            {
                for (int i = 0; i < onPlate.Count; i++)
                {
                    canvas.Children.Remove(onPlate[i]);
                }
                onPlate.Clear();
            }

            TextBlock tb = new TextBlock();
            
            tb.Text = textBox2.Text;
            double xcent = circle.Margin.Left;
            double ycent = circle.Margin.Top;

            xcent += .5 * circle.Width;
            ycent += .5 * circle.Height;

            Console.WriteLine("x y " + xcent + " " + ycent);
            canvas.Children.Add(tb);

            
            onPlate.Add(tb);
            //tb.Arrange(new Rect(xcent, ycent, 30, 100));


            double hyp = 250;//Math.Sqrt(circle.Width * circle.Width + circle.Height * circle.Height) + 20.0;
            Random r = new Random();
            List<Point> pos = new List<Point>();
            List<double> rel = new List<double>();
               
            foreach (String c in s.Targets.Keys)
            {
                tb = new TextBlock();
                tb.Text = s.Targets[c].Title + " " + s.Targets[c].Relatedness;
                
                canvas.Children.Add(tb);
                double ang = r.NextDouble() *2.0 *Math.PI - Math.PI;
                Point p = new Point(xcent + Math.Cos(ang)*hyp, ycent + Math.Sin(ang) * hyp);
                Point p2 = new Point(p.X-xcent,p.Y-ycent);
                pos.Add(p2);
                rel.Add(s.Targets[c].Relatedness);
                tb.SetValue(LeftProperty, p.X);
                tb.SetValue(TopProperty, p.Y);                
                onPlate.Add(tb);
                
            }
            
            double rad = 180.0;
            double newang = getAngle(pos, rel, rad);
            
            onPlate[0].SetValue(LeftProperty, xcent+rad*Math.Cos(newang));
            onPlate[0].SetValue(TopProperty, ycent + rad * Math.Sin(newang));



        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Requesting suggestions from " + textBox3.Text);
            SuggestionRequest sr = new SuggestionRequest();
            sr.Source = "Information visualization";
            Task<ElementState> theTask = client.GetResponse(sr, ts);
            SuggestionResponse s = (SuggestionResponse)theTask.Result;

            var collection = from cg in s.Groups orderby cg.Concepts.Values.ElementAt(0).Relatedness select cg;


            foreach (ConceptGroup cg in collection)
            {
                String ret = "";
                TextBlock someTextBox = new TextBlock();
                ret += cg.TopTitle+": \n";
                foreach (String c in cg.Concepts.Keys)
                {
                    ret += "     " + Math.Round(cg.Concepts[c].Relatedness, 4) + " " + cg.Concepts[c].Title + "\n";
                }
                someTextBox.Text = ret;
                listBox1.Items.Add(someTextBox);
            }
            
            
            Console.WriteLine(s.GetType());
        }

        public class SimpleSolveme : LMAFunction
        {
            /// <summary>
            /// Returns Gaussian values
            /// </summary>
            /// <param name="x">x value</param>
            /// <param name="a">parameters</param>
            /// <returns></returns>
            public override double GetY(double x, double[] a)
            {
                double y = 0;
                for (int i = 0; i < a.Length; i++)
                    y = (a[i] - (double)(i)) * (a[i] - (double)(i));
                return y;
            }
        }

        /// <summary>
        /// Represents Lorenzian function. Derivative is calculated
        /// using a default f(x+dx)/dx method in the base class
        /// </summary>
        public class DatFunc : LMAFunction
        {
            public override double GetY(double x, double[] a)
            {
                //Console.WriteLine(x + " " + a[0] + " " + a[1]);
                return (a[0]-5) * a[1];
            }


        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            double[] x = { 0, 0 };//these are all minimized...  each would add another constraint?
            double[] a = { 2, 1 };//these are all the initial position

            LMAFunction f = new DatFunc();

            //double[][] dataPoints = f.GenerateData(a, x);
            double[][] dataPoints = f.GenerateData(a, x);

            LMA algorithm = new LMA(f, new double[] { .5, -1 },
                dataPoints, null, new GeneralMatrix(2, 2), 1d - 20, 15);

            //new double[][] {xValues, yValues};

            algorithm.Fit();

            for (int i = 0; i < algorithm.Parameters.Length; i++)
                Console.WriteLine(algorithm.Parameters[i]);
            /*for (int i = 0; i < a.Length; i++)
            {
                Assert.IsTrue(System.Math.Abs(algorithm.Parameters[i] - a[i]) < 0.0001);
                Trace.WriteLine("Parameter" + i.ToString() + " " + algorithm.Parameters[i].ToString());
            }

            Trace.WriteLine("# of iterations =" + algorithm.Iterations.ToString());
            */
        }
    }
}
