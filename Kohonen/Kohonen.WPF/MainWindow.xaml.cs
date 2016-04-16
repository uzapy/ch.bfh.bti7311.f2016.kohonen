using Kohonen.Lib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using System;

namespace Kohonen.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelfOrganizingMap map = new SelfOrganizingMap();

        public MainWindow()
        {
            InitializeComponent();

            map.GenerateRegularMap(16);

            foreach (Neuron n in map.Map)
            {
                Ellipse ellipse = new Ellipse()
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Black,
                };
                networkCanvas.Children.Add(ellipse);
                Canvas.SetTop(ellipse, GetPosition(n).X);
                Canvas.SetLeft(ellipse, GetPosition(n).Y);

                TextBlock text = new TextBlock()
                {
                    Text = n.ID.ToString(),
                    Foreground = Brushes.Yellow,
                };
                networkCanvas.Children.Add(text);
                Canvas.SetTop(text, GetPosition(n).X);
                Canvas.SetLeft(text, GetPosition(n).Y);

                foreach (Axon axon in n.Axons.Where(a => a.NeuronA.ID == n.ID))
                {
                    Point pointA = GetPosition(axon.NeuronA);
                    Point pointB = GetPosition(axon.NeuronB);

                    Line line = new Line()
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 3,
                        X1 = pointA.X + 10,
                        Y1 = pointA.Y + 10,
                        X2 = pointB.X + 10,
                        Y2 = pointB.Y + 10,
                    };
                    networkCanvas.Children.Add(line);
                    Canvas.SetZIndex(line, -1);
                }
            }
        }

        private Point GetPosition(Neuron n)
        {
            return new Point(n.Attributes["x"] * 30 + 30, n.Attributes["y"] * 30 + 30);
        }
    }
}
