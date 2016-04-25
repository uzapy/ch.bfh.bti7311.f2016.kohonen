using Kohonen.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Kohonen.Lib
{
    public class SelfOrganizingMap
    {
        private IrisDataContext dataContext = new IrisDataContext();
        private HashSet<IrisLib> irisData = new HashSet<IrisLib>();
        private HashSet<Neuron> neuronMap = new HashSet<Neuron>();

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
            
        }
    }
}
