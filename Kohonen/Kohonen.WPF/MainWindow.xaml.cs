using Kohonen.Lib;
using System.Linq;
using System.Windows;

namespace Kohonen.WPF
{
    public partial class MainWindow : Window
    {
        private SelfOrganizingMap map = new SelfOrganizingMap();
        //private bool isRunning = false;

        public MainWindow()
        {
            InitializeComponent();

            map.LoadSampleData(networkGrid.Width, networkGrid.Height);

            foreach (IrisLib i in map.IrisLib)
            {
                networkGrid.Children.Add(i.Ellipse);
            }

            map.GenerateRegularMap(networkGrid.Width, networkGrid.Height, 16);

            foreach (Neuron n in map.NeuronMap)
            {
                networkGrid.Children.Add(n.Ellipse);

                foreach (Axon axon in n.Axons.Where(a => a.NeuronA.ID == n.ID))
                {
                    networkGrid.Children.Add(axon.Line);
                }
            }
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            map.Algorithm();
        }

        private void Button_Step(object sender, RoutedEventArgs e)
        {
        }
    }
}
