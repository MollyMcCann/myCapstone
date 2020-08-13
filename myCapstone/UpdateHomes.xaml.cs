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
        private void LoadHomesList()// see if I can get this to load to listbox, are more values needed?

        {
            using (var db = new HomeTrackerModel1())
            {
                var homeList = db.Homes.OrderBy(h => h.HomeID).Select(h => new { HomeID = h.HomeID,  }).ToList();
                this.HomeListBox.DisplayMemberPath = "HomeId";
                this.HomeListBox.SelectedValuePath = "Address";
                this.HomeListBox.ItemsSource = homeList;
            }
        }

    }
}
