using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Kohonen.Lib;

namespace Kohonen.Test
{
    [TestClass]
    public class NeuronTest
    {
        private SelfOrganizingMap som = new SelfOrganizingMap();
        private HashSet<Neuron> neurons = new HashSet<Neuron>();
        private Dictionary<Neuron, double> neighborhood = new Dictionary<Neuron, double>();

        /*
         * N0 - N1 - N2 - N3
         * |    |    |    |
         * N4 - N5 - N6 - N7
         * |    |    |    |
         * N8 - N9 - N10- N11
         * |    |    |    |
         * N12- N13- N14- N15
         **/
        [TestInitialize]
        public void BuildUpNeuronMap()
        {
            som.GenerateRegularMap(500, 500, 4);
            neurons = som.NeuronMap;
        }

        [TestMethod]
        public void GetNeighborhoodTest()
        {
            Assert.IsTrue(neurons.Count > 0);

            Neuron first = neurons.First();

            neighborhood = first.GetNeighborhood(2);
            Assert.IsTrue(neighborhood.Count == 6);

            neighborhood.Clear();

            neighborhood = first.GetNeighborhood(3);
            Assert.IsTrue(neighborhood.Count == 10);

            neighborhood.Clear();

            neighborhood = first.GetNeighborhood(4);
            Assert.IsTrue(neighborhood.Count == 13);

            neighborhood.Clear();

            neighborhood = first.GetNeighborhood(5);
            Assert.IsTrue(neighborhood.Count == 15);

            neighborhood.Clear();

            neighborhood = first.GetNeighborhood(6);
            Assert.IsTrue(neighborhood.Count == 16);
        }
    }
}
