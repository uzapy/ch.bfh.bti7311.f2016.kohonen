using System;
using System.Collections.Generic;

namespace Kohonen.Lib
{
    public class Neuron
    {
        private int id;
        private Dictionary<double, Neuron> neigbours = new Dictionary<double, Neuron>();
        private Dictionary<string, double> attributes = new Dictionary<string, double>();

        public Neuron(int id, Dictionary<string, double> attributes)
        {
            this.id = id;
            this.attributes = attributes;
        }

        public int ID { get { return id; } }

        public Dictionary<string, double> Attributes { get { return attributes; } }

        public Dictionary<double, Neuron> Neighbours { get { return neigbours; } }

        internal void AddNeighbour(int angle, Neuron n)
        {
            neigbours.Add(angle, n);
            n.Neighbours.Add((angle + 180) % 360, this);
        }
    }
}
