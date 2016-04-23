using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kohonen.Lib
{
    public class Neuron
    {
        public const int RADIUS = 10;

        private int id;
        private Dictionary<string, double> attributes = new Dictionary<string, double>();
        private List<Axon> axons = new List<Axon>();
        private Ellipse ellipse = new Ellipse();

        public Neuron(int id, Dictionary<string, double> attributes)
        {
            this.id = id;
            this.attributes = attributes;

            ellipse.Height = Neuron.RADIUS * 2;
            ellipse.Width = Neuron.RADIUS * 2;
            ellipse.Fill = Brushes.Black;
        }

        public int ID { get { return id; } }
        public Dictionary<string, double> Attributes { get { return attributes; } }
        public List<Axon> Axons { get { return axons; } }
        public Ellipse Ellipse { get { return ellipse; } }
        public double X
        {
            get
            {
                return attributes.ContainsKey("x") ? attributes["x"] * 30 + 30 : 0;
            }
            set
            {
                double normalizedValue = (value - 30) / 30;
                Attributes["x"] = normalizedValue;
            }
        }
        public double Y
        {
            get
            {
                return attributes.ContainsKey("y") ? attributes["y"] * 30 + 30 : 0;
            }
            set
            {
                double normalizedValue = (value - 30) / 30;
                Attributes["y"] = normalizedValue;
            }
        }

        internal void AddAxon(int angle, Neuron n)
        {
            Axon axon = new Axon(angle, this, n);
            Axons.Add(axon);
            n.Axons.Add(axon);
        }
    }
}
