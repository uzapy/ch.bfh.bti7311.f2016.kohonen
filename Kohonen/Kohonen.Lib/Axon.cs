﻿using System.Windows.Media;
using System.Windows.Shapes;

namespace Kohonen.Lib
{
    public class Axon
    {
        private int angle;
        private Neuron neuronA;
        private Neuron neuronB;
        private Line line;

        public Axon(int angle, Neuron neuronA, Neuron neuronB)
        {
            this.angle = angle;
            this.neuronA = neuronA;
            // (angle + 180) % 360,
            this.neuronB = neuronB;

            line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;
            line.X1 = NeuronA.Position.X;
            line.Y1 = NeuronA.Position.Y;
            line.X2 = NeuronB.Position.X;
            line.Y2 = NeuronB.Position.Y;
        }

        public int Angle { get { return angle; } }
        public Neuron NeuronA { get { return neuronA; } }
        public Neuron NeuronB { get { return neuronB; } }
        public Line Line { get { return line; } }

        internal void UpdateNeuronPosition(int neuronID)
        {
            if (neuronA.ID == neuronID)
            {
                Line.X1 = NeuronA.Position.X;
                Line.Y1 = NeuronA.Position.Y;
            }
            else if (NeuronB.ID == neuronID)
            {
                Line.X2 = NeuronB.Position.X;
                Line.Y2 = NeuronB.Position.Y;
            }
        }

        internal Neuron GetNeighbourOf(int neuronID)
        {
            return NeuronA.ID == neuronID ? NeuronB : NeuronA;
        }
    }
}