using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kohonen.Lib
{
    public class Neuron
    {
        public const int RADIUS = 7;

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
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Top;
            ellipse.Margin = new Thickness(X, Y, 0, 0);
        }

        public int ID { get { return id; } }
        public Dictionary<string, double> Attributes { get { return attributes; } }
        public List<Axon> Axons { get { return axons; } }
        public Ellipse Ellipse { get { return ellipse; } }
        public double X
        {
            get
            {
                return Attributes.ContainsKey("x") ? Attributes["x"]: 0;
            }
            set
            {
                Attributes["x"] = value;
            }
        }
        public double Y
        {
            get
            {
                return Attributes.ContainsKey("y") ? Attributes["y"] : 0;
            }
            set
            {
                Attributes["y"] = value;
            }
        }

        public void Move(double x, double y)
        {
            X += x;
            Y += y;
            Ellipse.Margin = new Thickness(X, Y, 0, 0);

            foreach (Axon a in Axons)
            {
                a.UpdateNeuronPosition(ID);
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
