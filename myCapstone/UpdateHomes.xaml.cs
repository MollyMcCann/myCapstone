using HomeTrackerDatamodelLibrary;
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
    /// Interaction logic for UpdateHomes.xaml
    /// </summary>
    public partial class UpdateHomes : Window
    {
        PeopleCollection peopleUd;
        HomeCollection HomeUd;
        public UpdateHomes(PeopleCollection people, HomeCollection homes)
        {
            InitializeComponent();
            peopleUd = people;
            HomeUd = homes;
        }
    }
}
