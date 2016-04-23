using Kohonen.Data;
using System.Collections.Generic;
using System.Linq;

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
            foreach (Kohonen.Data.Iris i in dataContext.Iris)
            {
                irisData.Add(new IrisLib(i));
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
                    Dictionary<string, double> attributes = new Dictionary<string, double>();
                    attributes.Add("x", x * step + 15);
                    attributes.Add("y", y * step + 15);

                    Neuron neuron = new Neuron(id, attributes);
                    id++;
                    neuronMap.Add(neuron);

                    if (y > 0 && y < size)
                    {
                        Neuron neighbor1 = neuronMap
                            .Where(n => n.Attributes["y"] == neuron.Attributes["y"] - step && n.Attributes["x"] == neuron.Attributes["x"])
                            .FirstOrDefault();

                        neuron.AddAxon(180, neighbor1);
                    }

                    if (x > 0 && x < size)
                    {
                        Neuron neighbor2 = neuronMap
                            .Where(n => n.Attributes["y"] == neuron.Attributes["y"] && n.Attributes["x"] == neuron.Attributes["x"] - step)
                            .FirstOrDefault();

                        neuron.AddAxon(90, neighbor2);
                    }
                }
            }
        }
    }
}
