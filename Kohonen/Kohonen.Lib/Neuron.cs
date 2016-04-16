using System;
using System.Collections.Generic;

namespace Kohonen.Lib
{
    public class Neuron
    {
        private int id;
        private Dictionary<string, double> attributes = new Dictionary<string, double>();
        private List<Axon> axons = new List<Axon>();

        public Neuron(int id, Dictionary<string, double> attributes)
        {
            this.id = id;
            this.attributes = attributes;
        }

        public int ID { get { return id; } }

        public Dictionary<string, double> Attributes { get { return attributes; } }

        public List<Axon> Axons { get { return axons; } }

        internal void AddAxons(int angle, Neuron n)
        {
            Axon axon = new Axon(angle, this, n);
            Axons.Add(axon);
            n.Axons.Add(axon);
        }
    }
}
