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
using System.Windows.Shapes;

namespace myCapstone
{
    /// <summary>
    /// Interaction logic for removeAHome.xaml
    /// </summary>
    public partial class removeAHome : Window
    {
        PeopleCollection peopleRM;
        HomeCollection homesRM;
        public removeAHome(PeopleCollection people, HomeCollection homes)
        {
            InitializeComponent();
            peopleRM = people;
            homesRM = homes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
