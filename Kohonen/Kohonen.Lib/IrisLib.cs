using Kohonen.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kohonen.Lib
{
    public class IrisLib
    {
        public const int RADIUS = 10;

        private Iris iris;
        private double x;
        private double y;
        private Ellipse ellipse;

        public IrisLib(Iris iris, double x, double y)
        {
            this.iris = iris;
            this.x = x;
            this.y = y;
        }

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

        public Ellipse Ellipse
        {
            get
            {
                if (ellipse == null)
                {
                    ellipse = new Ellipse();
                    ellipse.Width = IrisLib.RADIUS * 2;
                    ellipse.Height = IrisLib.RADIUS * 2;
                    switch (iris.Species)
                    {
                        case "Iris setosa":
                            ellipse.Fill = Brushes.Blue;
                            break;
                        case "Iris versicolor":
                            ellipse.Fill = Brushes.Red;
                            break;
                        case "Iris virginica":
                            ellipse.Fill = Brushes.Green;
                            break;
                    }
                    ellipse.Opacity = 0.5;
                    ellipse.HorizontalAlignment = HorizontalAlignment.Left;
                    ellipse.VerticalAlignment = VerticalAlignment.Top;
                    ellipse.Margin = new Thickness(X, Y, 0, 0);
                }
                return ellipse;
            }
        }
    }
}
