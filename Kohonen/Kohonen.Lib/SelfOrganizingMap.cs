using Kohonen.Data;
using System.Linq;
using System.Collections.Generic;

namespace Kohonen.Lib
{
    public class SelfOrganizingMap
    {
        private IrisDataContext dataContext = new IrisDataContext();
        private HashSet<Neuron> map = new HashSet<Neuron>();

        public HashSet<Neuron> Map { get { return map; } }

        public void GenerateRegularMap(int size)
        {
            int id = 0;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Dictionary<string, double> attributes = new Dictionary<string, double>();
                    attributes.Add("x", x);
                    attributes.Add("y", y);

                    Neuron neuron = new Neuron(id, attributes);
                    id++;
                    map.Add(neuron);

                    if (y > 0 && y < size)
                    {
                        Neuron neighbor1 = map
                            .Where(n => n.Attributes["y"] == neuron.Attributes["y"] - 1 && n.Attributes["x"] == neuron.Attributes["x"])
                            .FirstOrDefault();

                        neuron.AddAxons(180, neighbor1);
                    }

                    if (x > 0 && x < size)
                    {
                        Neuron neighbor2 = map
                            .Where(n => n.Attributes["y"] == neuron.Attributes["y"] && n.Attributes["x"] == neuron.Attributes["x"] - 1)
                            .FirstOrDefault();

                        neuron.AddAxons(90, neighbor2);
                    }
                }
            }
        }
    }
}
