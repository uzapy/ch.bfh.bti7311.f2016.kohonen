using Kohonen.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Kohonen.Lib
{
    public class SelfOrganizingMap
    {
        private IrisDataContext dataContext = new IrisDataContext();
        private List<IrisLib> irisData = new List<IrisLib>();
        private HashSet<Neuron> neuronMap = new HashSet<Neuron>();
        private double learningRate = 0.5;

        public IEnumerable<IrisLib> IrisLib { get { return irisData; } }
        public HashSet<Neuron> NeuronMap { get { return neuronMap; } }

        public void LoadSampleData()
        {
            double sepalLengthMin = dataContext.Iris.Min(i => i.SepalLength);
            double sepalLengthMax = dataContext.Iris.Max(i => i.SepalLength);
            double sepalWidthMin = dataContext.Iris.Min(i => i.SepalWidth);
            double sepalWidthMax = dataContext.Iris.Max(i => i.SepalWidth);
            double petalLengthMin = dataContext.Iris.Min(i => i.PetalLength);
            double petalLengthMax = dataContext.Iris.Max(i => i.PetalLength);
            double petalWidthMin = dataContext.Iris.Min(i => i.PetalWidth);
            double petalWidthMax = dataContext.Iris.Max(i => i.PetalWidth);

            foreach (Kohonen.Data.Iris i in dataContext.Iris)
            {
                double x = (i.PetalLength - petalLengthMin) * (1000 / (petalLengthMax - petalLengthMin));
                double y = (i.SepalLength - sepalLengthMin) * (1000 / (sepalLengthMax - sepalLengthMin));
                irisData.Add(new IrisLib(i, x, y));
            }
        }

        public void GenerateRegularMap(int size)
        {
            int id = 0;
            double step = 1000 / size;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Neuron neuron = new Neuron(id, x * step + 30, y * step + 30);
                    id++;
                    neuronMap.Add(neuron);

                    if (y > 0 && y < size)
                    {
                        Neuron neighbor1 = neuronMap.Where(n => n.Y == neuron.Y - step && n.X == neuron.X).FirstOrDefault();
                        neuron.AddAxon(180, neighbor1);
                    }

                    if (x > 0 && x < size)
                    {
                        Neuron neighbor2 = neuronMap.Where(n => n.Y == neuron.Y && n.X == neuron.X - step).FirstOrDefault();
                        neuron.AddAxon(90, neighbor2);
                    }
                }
            }
        }

        public void Algorithm()
        {
            // Zufällig durch die Input-Daten gehen
            irisData = Shuffle(irisData);
            foreach (IrisLib iris in irisData)
            {
                // Input-Vektor markieren
                iris.MarkAsCurrent();

                // Das Neuron mit der maximalen Erregung wird ermittelt. Minimaler Euklidischer Abstand zum Input-Vektor.
                Neuron closest = neuronMap.OrderBy(n => Math.Sqrt(Math.Pow(n.X - iris.X, 2) + Math.Pow(n.Y - iris.Y, 2))).First();

                // Neuron Markieren
                closest.MarkAsCurrent();

                //// Eine Sekunde Pause
                //await Task.Delay(100);

                // Neuron und dessen Nachbarschaft ein Stück in die Richtung des Input-Vektors bewegen
                closest.UpdatePosition(iris, learningRate);

                // Moved-Flag resetten
                foreach (Neuron neuron in neuronMap)
                {
                    neuron.HasMoved = false;
                }

                // Input-Vektor nicht mehr markieren
                iris.UnmarkAsCurrent();
            }
        }

        private List<T> Shuffle<T>(List<T> list)
        {
            Random random = new Random();
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
