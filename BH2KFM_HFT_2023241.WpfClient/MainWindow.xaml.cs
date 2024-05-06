using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BH2KFM_HFT_2023241.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditorSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EditorSelector.SelectedIndex == 0)
            {
                SubjectGrid.Visibility = Visibility.Visible;
                RoomGrid.Visibility = Visibility.Hidden;
                CourseGrid.Visibility = Visibility.Hidden;
            }
            else if (EditorSelector.SelectedIndex == 1)
            {
                SubjectGrid.Visibility = Visibility.Hidden;
                RoomGrid.Visibility = Visibility.Visible;
                CourseGrid.Visibility = Visibility.Hidden;
            }
            else if (EditorSelector.SelectedIndex == 2)
            {
                SubjectGrid.Visibility = Visibility.Hidden;
                RoomGrid.Visibility = Visibility.Hidden;
                CourseGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
