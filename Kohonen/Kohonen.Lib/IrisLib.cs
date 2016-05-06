using Kohonen.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System;

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
                    ellipse.Fill = GetBrushForSpecies(iris.Species);
                    ellipse.Opacity = 0.333;
                    ellipse.HorizontalAlignment = HorizontalAlignment.Left;
                    ellipse.VerticalAlignment = VerticalAlignment.Top;
                    ellipse.Margin = new Thickness(X, Y, 0, 0);
                }
                return ellipse;
            }
        }

        private Brush GetBrushForSpecies(string species)
        {
            switch (iris.Species)
            {
                case "Iris setosa":
                    return Brushes.Blue;
                case "Iris versicolor":
                    return Brushes.Red;
                case "Iris virginica":
                    return Brushes.Green;
                default :
                    return Brushes.Gray;
            }
        }

        internal void MarkAsCurrent()
        {
            if (ellipse != null)
            {
                ellipse.Opacity = 1;
                ellipse.Fill = Brushes.Violet;
                ellipse.Stroke = Brushes.Black;
            }
        }

        internal void UnmarkAsCurrent()
        {
            if (ellipse != null)
            {
                ellipse.Opacity = 0.33;
                ellipse.Fill = GetBrushForSpecies(iris.Species);
            }
        }
    }
}
