using Kohonen.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kohonen.Lib
{
    public class SelfOrganizingMap
    {
        private IrisDataContext dataContext = new IrisDataContext();
        private List<IrisLib> irisData = new List<IrisLib>();
        private HashSet<Neuron> neuronMap = new HashSet<Neuron>();
        private Random random = new Random();

        private double learningRate = 0.75;
        private double blockRadius = 300;
        private bool showSteps = false;
        private double runs = 0;
        internal const double DISTANCE_FACTOR = 0.5;
        internal const double LEARNING_RATE_LOW_THRESHHOLD = 0.1;

        public List<IrisLib> IrisData { get { return irisData; } }
        public HashSet<Neuron> NeuronMap { get { return neuronMap; } }
        public double LearningRate
        {
            get { return learningRate * ((1000-runs) / 1000); }
            set { learningRate = value; }
        }
        public double BlockRadius
        {
            get { return blockRadius; }
            set { blockRadius = value; }
        }
        public bool ShowSteps
        {
            get { return showSteps; }
            set { showSteps = value; }
        }
        public double Runs { get { return runs; } }

        public void LoadSampleData(double width, double height, string horizontalData, string verticalData)
        {
            double sepalLengthMin = dataContext.Iris.Min(i => i.SepalLength);
            double sepalLengthMax = dataContext.Iris.Max(i => i.SepalLength);
            double sepalWidthMin = dataContext.Iris.Min(i => i.SepalWidth);
            double sepalWidthMax = dataContext.Iris.Max(i => i.SepalWidth);
            double petalLengthMin = dataContext.Iris.Min(i => i.PetalLength);
            double petalLengthMax = dataContext.Iris.Max(i => i.PetalLength);
            double petalWidthMin = dataContext.Iris.Min(i => i.PetalWidth);
            double petalWidthMax = dataContext.Iris.Max(i => i.PetalWidth);

            IrisData.Clear();

            foreach (Kohonen.Data.Iris i in dataContext.Iris)
            {
                double x = 0;
                double y = 0;
                switch (horizontalData)
                {
                    case "Sepal Length":
                        x = (i.SepalLength - sepalLengthMin) * (width / (sepalLengthMax - sepalLengthMin));
                        break;
                    case "Sepal Width":
                        x = (i.SepalWidth - sepalWidthMin) * (width / (sepalWidthMax - sepalWidthMin));
                        break;
                    case "Petal Length":
                        x = (i.PetalLength - petalLengthMin) * (width / (petalLengthMax - petalLengthMin));
                        break;
                    case "Petal Width":
                        x = (i.PetalWidth - petalWidthMin) * (width / (petalWidthMax - petalWidthMin));
                        break;
                    default:
                        continue;
                }
                switch (verticalData)
                {
                    case "Sepal Length":
                        y = (i.SepalLength - sepalLengthMin) * (height / (sepalLengthMax - sepalLengthMin));
                        break;
                    case "Sepal Width":
                        y = (i.SepalWidth - sepalWidthMin) * (height / (sepalWidthMax - sepalWidthMin));
                        break;
                    case "Petal Length":
                        y = (i.PetalLength - petalLengthMin) * (height / (petalLengthMax - petalLengthMin));
                        break;
                    case "Petal Width":
                        y = (i.PetalWidth - petalWidthMin) * (height / (petalWidthMax - petalWidthMin));
                        break;
                    default:
                        continue;
                }
                IrisData.Add(new IrisLib(i, x, y));
            }
        }

        public void GenerateRegularMap(double width, double height, int size)
        {
            int id = 0;
            double step = 200 / size;
            double leftOffset = (width / 2) - (size / 2) * step;
            double topOffset = (height / 2) - (size / 2) * step;

            NeuronMap.Clear();

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Neuron neuron = new Neuron(id, x * step + leftOffset, y * step + topOffset);
                    id++;
                    NeuronMap.Add(neuron);

                    if (y > 0 && y < size)
                    {
                        Neuron neighbor1 = NeuronMap.Where(n => n.Position.Y == neuron.Position.Y - step && n.Position.X == neuron.Position.X).FirstOrDefault();
                        neuron.AddAxon(180, neighbor1);
                    }

                    if (x > 0 && x < size)
                    {
                        Neuron neighbor2 = NeuronMap.Where(n => n.Position.Y == neuron.Position.Y && n.Position.X == neuron.Position.X - step).FirstOrDefault();
                        neuron.AddAxon(90, neighbor2);
                    }
                }
            }
        }

        public void Algorithm()
        {
            // Zufällig durch die Input-Daten gehen
            irisData = Shuffle(IrisData);
            foreach (IrisLib iris in IrisData)
            {
                // Input-Vektor markieren
                // iris.MarkAsCurrent();

                // Das Neuron mit der maximalen Erregung wird ermittelt. Minimaler Euklidischer Abstand zum Input-Vektor.
                Neuron closest = NeuronMap.OrderBy(n => (n.Position - iris.Position).Length).First();

                // Neuron Markieren
                //closest.MarkAsCurrent();

                // Neuron ein Stück in die Richtung des Input-Vektors bewegen
                closest.HasMoved = true;
                closest.Position -= LearningRate * (closest.Position - iris.Position);

                // Dessen Nachbarschaft ein Stück in die Richtung des Input-Vektors bewegen
                foreach (Neuron n in closest.Neighbours)
                {
                    n.MoveRecursively(iris.Position, LearningRate);
                }

                // Moved-Flag resetten
                foreach (Neuron neuron in NeuronMap)
                {
                    neuron.HasMoved = false;
                }

                // Input-Vektor nicht mehr markieren
                // iris.UnmarkAsCurrent();

            }
            // Increment run count
            runs++;
        }

        public void Redraw()
        {
            foreach (var n in NeuronMap)
            {
                n.Redraw();
            }
        }

        private List<T> Shuffle<T>(List<T> list)
        {
            int current = list.Count;
            while (current > 1)
            {
                current--;
                int other = random.Next(current + 1);
                T otherObject = list[other];
                list[other] = list[current];
                list[current] = otherObject;
            }
            return list;
        }
    }
}
