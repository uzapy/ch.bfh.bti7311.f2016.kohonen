using Kohonen.Lib;
using System.Windows;

namespace Kohonen.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelfOrganizingMap map = new SelfOrganizingMap();

        public MainWindow()
        {
            InitializeComponent();

            map.GenerateRegularMap(32);
        }
    }
}
