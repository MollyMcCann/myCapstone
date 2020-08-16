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
using HomeTrackerDatamodelLibrary;

namespace myCapstone
{
    /// <summary>
    /// Interaction logic for removeAHome.xaml
    /// </summary>
    public partial class removeAHome : Window
    {
        PeopleCollection peopleRM;
        HomeCollection homesRM;
        HomeCollection homeCollection;
        //HomeSalesCollection homeSalesCollection;
        //RealEstateCompanyCollection realEstateCompaniesCollection;

        public removeAHome(PeopleCollection people, HomeCollection homes)
        {
            InitializeComponent();
            peopleRM = people;
            homesRM = homes;
            RemoveListBox.DataContext = this;

            Loaded += RemoveAHome_Loaded;
        }

        private void RemoveAHome_Loaded(object sender, RoutedEventArgs e)
        {
            RemoveHomeList();
        }

        public void RemoveHomeList()
        {
            using (var db = new HomeTrackerModel1())
            {
                homeCollection = new HomeCollection(db.Homes.ToList());
                this.RemoveListBox.DisplayMemberPath = "Address";
                this.RemoveListBox.SelectedValuePath = "HomeID";
                this.RemoveListBox.ItemsSource = homeCollection;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
              RemoveListBox.Items.RemoveAt

            (RemoveListBox.Items.IndexOf(RemoveListBox.DataContext.SelectedItem));

            RemoveListBox.Items.Refresh();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoveListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
