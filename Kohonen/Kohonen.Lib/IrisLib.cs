using Kohonen.Data;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kohonen.Lib
{
    public class IrisLib
    {
        public const int RADIUS = 10;

        private Iris iris;
        //private Dictionary<string, double> attributes;
        private Ellipse ellipse;

        public IrisLib(Iris iris)
        {
            this.iris = iris;
        }

        //public Dictionary<string, double> Attributes
        //{
        //    get
        //    {
        //        if (attributes == null)
        //        {
        //            attributes = new Dictionary<string, double>();
        //            attributes.Add(nameof(iris.SepalLength), iris.SepalLength);
        //            attributes.Add(nameof(iris.SepalWidth), iris.SepalWidth);
        //            attributes.Add(nameof(iris.PetalLength), iris.PetalLength);
        //            attributes.Add(nameof(iris.PetalWidth), iris.PetalWidth);
        //        }
        //        return attributes;
        //    }
        //}

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
                    ellipse.Margin = new  Thickness(iris.PetalWidth * 100, iris.SepalLength * 100, 0, 0);
                }
                return ellipse;
            }
        }
    }
}
