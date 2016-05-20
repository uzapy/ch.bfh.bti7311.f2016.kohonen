using Kohonen.Lib;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System;

namespace Kohonen.WPF
{
    public partial class MainWindow : Window
    {
        private SelfOrganizingMap map = new SelfOrganizingMap();
        private string horizontalData = String.Empty;
        private string verticalData = String.Empty;
        private bool isRunning = false;

        public MainWindow()
        {
            InitializeComponent();

            LoadSampleData();
            LoadNetworkData();

            InitialLearningRate.Text = SelfOrganizingMap.LEARNING_RATE_START.ToString("0.00");
            CurrentLearningRate.Text = SelfOrganizingMap.LEARNING_RATE_START.ToString("0.0000");
            InitialBlockRadius.Text = SelfOrganizingMap.BLOCK_RADIUS_START.ToString("0.00");
            CurrentBlockRadius.Text = SelfOrganizingMap.BLOCK_RADIUS_START.ToString("0.00");
            MaxSteps.Text = SelfOrganizingMap.STEPS_MAX.ToString();
        }

        private void HorizontalData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            if (selected != null && selected.Content != null && selected.Content is string)
            {
                horizontalData = (selected.Content as string);
                LoadSampleData(true);
            }
        }

        private void VerticalData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            if (selected != null && selected.Content != null && selected.Content is string)
            {
                verticalData = (selected.Content as string);
                LoadSampleData(true);
            }
        }

        private async void Button_Play(object sender, RoutedEventArgs e)
        {
            isRunning = !isRunning;
            Play.Content = isRunning ? "Stop" : "Play";

            double initialLearningRate = 0;
            if (double.TryParse(InitialLearningRate.Text, out initialLearningRate))
            {
                map.LearningRate = initialLearningRate;
            }

            double initialBlockRadius = 0;
            if (double.TryParse(InitialBlockRadius.Text, out initialBlockRadius))
            {
                map.BlockRadius = initialBlockRadius;
            }

            int maxSteps = 0;
            if (int.TryParse(MaxSteps.Text, out maxSteps))
            {
                map.MaxSteps = maxSteps;
            }

            while (isRunning && map.Steps <= map.MaxSteps)
            {
                await Task.Run(() => map.Algorithm());
                NumberOfRuns.Text = map.Steps.ToString();
                CurrentLearningRate.Text = map.LearningRate.ToString("0.0000");
                CurrentBlockRadius.Text = map.BlockRadius.ToString("0.00");
                map.Redraw();
            }
        }

        private void Button_Step(object sender, RoutedEventArgs e)
        {
            map.ShowSteps = !map.ShowSteps;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            LoadSampleData(true);
            LoadNetworkData(true);
            map.LearningRate = Double.Parse(InitialLearningRate.Text);
            map.BlockRadius = Double.Parse(InitialBlockRadius.Text);
            map.Steps = 0;

            NumberOfRuns.Text = map.Steps.ToString();
            CurrentLearningRate.Text = map.LearningRate.ToString();
            CurrentBlockRadius.Text = map.BlockRadius.ToString();
        }

        private void LoadSampleData(bool removeExisting = false)
        {
            if (removeExisting)
            {
                foreach (IrisLib i in map.IrisData)
                {
                    networkGrid.Children.Remove(i.Ellipse);
                }
            }

            map.LoadSampleData(networkGrid.Width, networkGrid.Height, horizontalData, verticalData);

            foreach (IrisLib i in map.IrisData)
            {
                networkGrid.Children.Add(i.Ellipse);
            }
        }

        private void LoadNetworkData(bool removeExisting = false)
        {
            if (removeExisting)
            {
                foreach (Neuron n in map.NeuronMap)
                {
                    networkGrid.Children.Remove(n.Ellipse);

                    foreach (Axon a in n.Axons.Where(a => a.NeuronA.ID == n.ID))
                    {
                        networkGrid.Children.Remove(a.Line);
                    }
                }
            }

            map.GenerateRegularMap(networkGrid.Width, networkGrid.Height, 16);

            foreach (Neuron n in map.NeuronMap)
            {
                networkGrid.Children.Add(n.Ellipse);

                foreach (Axon a in n.Axons.Where(a => a.NeuronA.ID == n.ID))
                {
                    networkGrid.Children.Add(a.Line);
                }
            }
        }
    }
}
