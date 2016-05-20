using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kohonen.Lib
{
    public class Neuron
    {
        public const int RADIUS = 4;

        private int id;
        private Vector position = new Vector();
        private bool hasMoved = false;
        private Ellipse ellipse = new Ellipse();
        private List<Axon> axons = new List<Axon>();

        public Neuron(int id, double x, double y)
        {
            this.id = id;
            this.position.X = x;
            this.position.Y = y;

            ellipse.Height = Neuron.RADIUS * 2;
            ellipse.Width = Neuron.RADIUS * 2;
            ellipse.Fill = Brushes.Black;
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Top;
            ellipse.Margin = new Thickness(Position.X - Neuron.RADIUS, Position.Y - Neuron.RADIUS, 0, 0);
        }

        public int ID { get { return id; } }
        public Vector Position
        {
            get { return position; }
            set { position = value; }
        }
        public bool HasMoved
        {
            get { return hasMoved; }
            set { hasMoved = value; }
        }
        public Ellipse Ellipse { get { return ellipse; } }
        public List<Axon> Axons { get { return axons; } }
        public List<Neuron> Neighbours
        {
            get
            {
                List<Neuron> neighbours = new List<Neuron>();
                foreach (Axon a in Axons)
                {
                    neighbours.Add(a.GetNeighbourOf(ID));
                }
                return neighbours;
            }
        }

        public void Redraw()
        {
            Ellipse.Margin = new Thickness(Position.X - Neuron.RADIUS, Position.Y - Neuron.RADIUS, 0, 0);

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

        internal void MarkAsCurrent()
        {
            ellipse.Fill = Brushes.Aqua;
            ellipse.Stroke = Brushes.Black;
        }

        internal void UnmarkAsCurrent()
        {
            ellipse.Fill = Brushes.Black;
            ellipse.Stroke = null;
        }

        internal void MoveRecursively(Vector irisPosition, double learningRate)
        {
            double currentLearningRate = learningRate * SelfOrganizingMap.DISTANCE_FACTOR;
            if (!HasMoved && currentLearningRate > SelfOrganizingMap.LEARNING_RATE_END)
            {
                HasMoved = true;
                Position -= currentLearningRate * (Position - irisPosition);

                foreach (Neuron n in Neighbours)
                {
                    n.MoveRecursively(irisPosition, currentLearningRate);
                }
            }
        }
    }
}
