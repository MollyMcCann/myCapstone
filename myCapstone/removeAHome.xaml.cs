using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
        public removeAHome(PeopleCollection people, HomeCollection homes)
        {
            InitializeComponent();
            peopleRM = people;
            homesRM = homes;

            Loaded += RemoveAHome_Loaded;
            Closing += RemoveAHome_Closing;
        }

        private void RemoveAHome_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void RemoveAHome_Loaded(object sender, RoutedEventArgs e)
        {
            RemoveHomeList();
        }

        public void RemoveHomeList()
        {
            using (var db = new HomeTrackerModel1())
            {
                homesRM = new HomeCollection(db.Homes.ToList());
                this.RemoveListBox.DisplayMemberPath = "Address";
                this.RemoveListBox.SelectedValuePath = "HomeID";
                this.RemoveListBox.ItemsSource = homesRM;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            homesRM.Remove((Home)RemoveListBox.SelectedItem);
            this.RemoveListBox.ItemsSource = homesRM;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoveListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }

    internal class ObservableCollection : HomeCollection
    {
        private HomeCollection homeCollection;

        public ObservableCollection(HomeCollection homeCollection)
        {
            this.homeCollection = homeCollection;
        }
    }
}
