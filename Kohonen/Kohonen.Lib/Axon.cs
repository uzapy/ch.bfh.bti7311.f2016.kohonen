using System.Windows.Media;
using System.Windows.Shapes;

namespace Kohonen.Lib
{
    public class Axon
    {
        private int angle;
        private Neuron neuronB;
        private Neuron neuronA;
        private Line line = new Line();

        public Axon(int angle, Neuron neuronA, Neuron neuronB)
        {
            this.angle = angle;
            this.neuronA = neuronA;
            // (angle + 180) % 360,
            this.neuronB = neuronB;

            line.Stroke = Brushes.Black;
            line.StrokeThickness = 3;
            line.X1 = neuronA.X + 10;
            line.Y1 = neuronA.Y + 10;
            line.X2 = neuronB.X + 10;
            line.Y2 = neuronB.Y + 10;
        }

        public int Angle { get { return angle; } }
        public Neuron NeuronA { get { return neuronA; } }
        public Neuron NeuronB { get { return neuronB; } }
        public Line Line { get { return line; } }
    }
}