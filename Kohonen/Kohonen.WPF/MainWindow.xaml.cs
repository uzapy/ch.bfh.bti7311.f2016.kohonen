using Kohonen.Lib;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kohonen.WPF
{
    public partial class MainWindow : Window
    {
        private SelfOrganizingMap map = new SelfOrganizingMap();
        private bool isRunning = false;

        public MainWindow()
        {
            InitializeComponent();

            map.LoadSampleData(networkGrid.Width, networkGrid.Height);

            foreach (IrisLib i in map.IrisData)
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

            InitialLearningRate.Text = map.LearningRate.ToString("0.00");
            InitialBlockRadius.Text = map.BlockRadius.ToString("0.00");
            CurrentLearningRate.Text = map.LearningRate.ToString("0.00");
        }

        private async void Button_Play(object sender, RoutedEventArgs e)
        {
            isRunning = !isRunning;
            Play.Content = isRunning ? "Stop" : "Play";

            while (isRunning)
            {
                await Task.Run(() => map.Algorithm());
                NumberOfRuns.Text = map.Runs.ToString();
                CurrentLearningRate.Text = map.LearningRate.ToString("0.00");
                map.Redraw();
            }
        }

        private void Button_Step(object sender, RoutedEventArgs e)
        {
            map.ShowSteps = !map.ShowSteps;
        }
    }
}
