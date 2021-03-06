﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kohonen.Lib
{
    public class Neuron
    {
        public const int RADIUS = 5;

        private int id;
        private Vector position = new Vector();
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

        public Dictionary<Neuron, double> GetNeighborhood(double blockRadius)
        {
            Dictionary<Neuron, double> neighborhood = new Dictionary<Neuron, double>();
            double currentRadius = 0;

            neighborhood.Add(this, currentRadius);
            List<Neuron> currentCircle = new List<Neuron>();
            List<Neuron> nextCircle = new List<Neuron>();
            currentCircle.AddRange(Neighbours);

            do
            {
                currentRadius++;
                foreach (Neuron n in currentCircle)
                {
                    neighborhood.Add(n, currentRadius);

                    foreach (Neuron m in n.Neighbours)
                    {
                        if (!neighborhood.Keys.Contains(m) && !nextCircle.Contains(m))
                        {
                            nextCircle.Add(m);
                        }
                    }
                }

                currentCircle.Clear();
                currentCircle.AddRange(nextCircle);
                nextCircle.Clear();

            } while (currentRadius < blockRadius);

            return neighborhood;
        }

        public override string ToString()
        {
            return String.Format("Neuron ID={0}, Position={1}/{2}, Neighbors={3}",
                ID, Math.Round(Position.X, 0), Math.Round(Position.Y, 0), string.Join(",",Neighbours.Select(n => n.ID).ToArray()));
        }
    }
}
