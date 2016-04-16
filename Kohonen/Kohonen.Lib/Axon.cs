namespace Kohonen.Lib
{
    public class Axon
    {
        private int angle;
        private Neuron neuronB;
        private Neuron neuronA;

        public Axon(int angle, Neuron neuronA, Neuron neuronB)
        {
            this.angle = angle;
            this.neuronA = neuronA;
            // (angle + 180) % 360,
            this.neuronB = neuronB;
        }

        public int Angle { get { return angle; } }
        public Neuron NeuronA { get { return neuronA; } }
        public Neuron NeuronB { get { return neuronB; } }
    }
}