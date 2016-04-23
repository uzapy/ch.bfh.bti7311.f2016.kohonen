using Kohonen.Lib;
using System.Linq;
using System.Windows;

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
                networkGrid.Children.Add(n.Ellipse);

                foreach (Axon axon in n.Axons.Where(a => a.NeuronA.ID == n.ID))
                {
                    networkGrid.Children.Add(axon.Line);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Neuron n = map.NeuronMap.Where(m => m.ID == 30).FirstOrDefault();
            n.Move(10, 10);
        }
    }
}
