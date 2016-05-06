using System;
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
        private double x;
        private double y;
        private bool hasMoved = false;
        private Ellipse ellipse = new Ellipse();
        private List<Axon> axons = new List<Axon>();

        public Neuron(int id, double x, double y)
        {
            this.id = id;
            this.x = x;
            this.y = y;

            ellipse.Height = Neuron.RADIUS * 2;
            ellipse.Width = Neuron.RADIUS * 2;
            ellipse.Fill = Brushes.Black;
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Top;
            ellipse.Margin = new Thickness(X, Y, 0, 0);
        }

        public int ID { get { return id; } }
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
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

        internal void UpdatePosition(IrisLib iris, double learningRate)
        {
            if (!HasMoved && learningRate > 0.1)
            {
                HasMoved = true;
                double deltaX = -(learningRate * (X - iris.X));
                double deltaY = -(learningRate * (Y - iris.Y));
                Move(deltaX, deltaY);

                foreach (Neuron n in Neighbours)
                {
                    n.UpdatePosition(iris, learningRate * 0.5);
                }
            }
        }
    }
}
