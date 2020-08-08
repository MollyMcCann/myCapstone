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
using HomeTrackerDatamodelLibrary;
using HomeTrackerTest;

namespace myCapstone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomeCollection homeCollection;
        public MainWindow()
        {
              homeCollection = new HomeCollection();
            InitializeComponent();
            using (HomeTrackerModel1 db = new HomeTrackerModel1())//move this to form load
            {
                //retrieve data:
                homeCollection = new HomeCollection(db.Homes.ToList());
            }

            foreach (Home home in homeCollection)
            {
                //searchBox.Text = home.Address; //ADD THIS BACK EVENTUALLY, THIS WAS THE TEST TO SEE IF WE CAN CONNECT to db

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var homes = new List<Home>();
            //foreach(Home h in homeCollection)
            //{
            //    homes.Add(h);
            //}
            HomeDataGrid.DataContext = homeCollection;
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
