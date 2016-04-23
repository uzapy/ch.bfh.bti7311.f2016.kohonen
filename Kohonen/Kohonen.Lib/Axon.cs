using System.Windows.Media;
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
            line.X1 = NeuronA.X + Neuron.RADIUS;
            line.Y1 = NeuronA.Y + Neuron.RADIUS;
            line.X2 = NeuronB.X + Neuron.RADIUS;
            line.Y2 = NeuronB.Y + Neuron.RADIUS;
        }

        public int Angle { get { return angle; } }
        public Neuron NeuronA { get { return neuronA; } }
        public Neuron NeuronB { get { return neuronB; } }
        public Line Line { get { return line; } }

        internal void UpdateNeuronPosition(int neuronID)
        {
            if (neuronA.ID == neuronID)
            {
                Line.X1 = NeuronA.X + Neuron.RADIUS;
                Line.Y1 = NeuronA.Y + Neuron.RADIUS;
            }
            else if (NeuronB.ID == neuronID)
            {
                Line.X2 = NeuronB.X + Neuron.RADIUS;
                Line.Y2 = NeuronB.Y + Neuron.RADIUS;
            }
        }
    }
}