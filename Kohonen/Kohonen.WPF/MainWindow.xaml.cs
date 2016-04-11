using Kohonen.Lib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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

            map.GenerateRegularMap(32);

            foreach (Neuron n in map.Map)
            {
                Ellipse e = new Ellipse();
                e.Width = 15;
                e.Height = 15;
                e.Fill = Brushes.Black;

                networkCanvas.Children.Add(e);
                Canvas.SetTop(e, n.Attributes["y"] * 20 + 20);
                Canvas.SetLeft(e, n.Attributes["x"] * 20 + 20);

                Line l = new Line();
                //l.X1
                //l.Y1
                //l.X2
                //l.Y2
                l.Fill = Brushes.Red;
            }
        }
    }
}
