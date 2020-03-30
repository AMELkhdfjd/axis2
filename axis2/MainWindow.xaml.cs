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

namespace axis2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Draw a simple graph.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            const double margin =   30;
            double xmin = margin;
            double xmax = canGraph.Width - margin;
            double ymin = margin;
            double ymax = canGraph.Height - margin;
            const double step = 28;
            double min = 120;
            double max = 220;
            double a = (max - min) / 10;


            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymax), new Point(canGraph.Width, ymax)));
            for (double x = xmin + step; x <= canGraph.Width - step; x += step)
            {
                LineGeometry line = new LineGeometry(new Point(x, ymax - margin / 4),
                    new Point(x, ymax + margin / 4));
                xaxis_geom.Children.Add(line);

                 TextBlock textBlock = new TextBlock();
                 textBlock.Text = "amel";
                 textBlock.Foreground = Brushes.Black;
                 Canvas.SetLeft(textBlock,x );
                Canvas.SetTop(textBlock, ymax);
                 canGraph.Children.Add(textBlock);


               

            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            canGraph.Children.Add(xaxis_path);

            // Make the Y axis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, canGraph.Height)));
            for (double y = step; y <= canGraph.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 4, y),
                    new Point(xmin + margin / 4, y)));
                TextBlock textBlock = new TextBlock();
                if (y == step)
                {
                    textBlock.Text = Convert.ToString(min);
                }
                else
                {
                    min += a;
                    textBlock.Text = Convert.ToString(min );
                }
                textBlock.Foreground = Brushes.Black;
                Canvas.SetBottom(textBlock, y+12);
                yaxis_geom.Children.Add(new LineGeometry(
                   new Point(0, y),
                   new Point(canGraph.Width, y)));
                canGraph.Children.Add(textBlock);
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            canGraph.Children.Add(yaxis_path);

            // Make some data sets.
            Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Blue };
            Random rand = new Random();
            for (int data_set = 0; data_set < 3; data_set++)
            {
                int last_y = rand.Next((int)ymin, (int)ymax);

                PointCollection points = new PointCollection();
                for (double x = xmin; x <= xmax; x += step)
                {
                    last_y = rand.Next(last_y - 10, last_y + 10);
                    if (last_y < ymin) last_y = (int)ymin;
                    if (last_y > ymax) last_y = (int)ymax;
                    points.Add(new Point(x, last_y));
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brushes[data_set];
                polyline.Points = points;

                canGraph.Children.Add(polyline);
            }

            
        }

    }
}
