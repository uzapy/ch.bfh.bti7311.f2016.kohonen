using Kohonen.Lib;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

            foreach (Neuron n in map.NeuronMap)
            {
                networkCanvas.Children.Add(n.Ellipse);
                Canvas.SetTop(n.Ellipse, n.X);
                Canvas.SetLeft(n.Ellipse, n.Y);

                //TextBlock text = new TextBlock()
                //{
                //    Text = n.ID.ToString(),
                //    Foreground = Brushes.Yellow,
                //};
                //networkCanvas.Children.Add(text);
                //Canvas.SetTop(text, GetPosition(n).X);
                //Canvas.SetLeft(text, GetPosition(n).Y);

                foreach (Axon axon in n.Axons.Where(a => a.NeuronA.ID == n.ID))
                {
                    networkCanvas.Children.Add(axon.Line);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Neuron n = map.NeuronMap.Where(m => m.ID == 30).FirstOrDefault();
            n.X += 10;
            n.Y += 10;
            Canvas.SetTop(n.Ellipse, n.X);
            Canvas.SetLeft(n.Ellipse, n.Y);
        }
    }
}
