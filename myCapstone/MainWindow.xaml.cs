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


namespace myCapstone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomeCollection homeCollection;
        HomeSalesCollection homeSalesCollection;
        PeopleCollection peopleCollection;
        RealEstateCompanyCollection realEstateCompaniesCollection;
        AddHome addHomeWindow;
        UpdateHomes updateHomesWindow;
        removeAHome removeAHomeWindow;
        
        public MainWindow()
        {
            homeCollection = new HomeCollection();
            homeSalesCollection = new HomeSalesCollection();
            peopleCollection = new PeopleCollection();
            realEstateCompaniesCollection = new RealEstateCompanyCollection();
            InitializeComponent();
            DataContext = this;
            using (HomeTrackerModel1 db = new HomeTrackerModel1())
            {
                //retrieve data:
               homeCollection = new HomeCollection(db.Homes.ToList());
               homeSalesCollection = new HomeSalesCollection(db.HomeSales.ToList());
                peopleCollection = new PeopleCollection(db.People.ToList());
                realEstateCompaniesCollection = new RealEstateCompanyCollection(db.RealEstateCompanies.ToList());
                addHomeWindow = new AddHome(peopleCollection,homeCollection);
                updateHomesWindow = new UpdateHomes(peopleCollection, homeCollection, homeSalesCollection, realEstateCompaniesCollection);
                removeAHomeWindow = new removeAHome(peopleCollection, homeCollection);

            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            HomeDataGrid.DataContext = homeCollection;
           
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addHomeWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            updateHomesWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            removeAHomeWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (HomeTrackerModel1 db = new HomeTrackerModel1())
            {
                //retrieve data:
                homeCollection = new HomeCollection(db.Homes.ToList());
                homeSalesCollection = new HomeSalesCollection(db.HomeSales.ToList());
                peopleCollection = new PeopleCollection(db.People.ToList());
                realEstateCompaniesCollection = new RealEstateCompanyCollection(db.RealEstateCompanies.ToList());
            }

            HomeDataGrid.DataContext = homeCollection;
        }

            private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            HomeDataGrid.DataContext = peopleCollection;
            HomeDataGrid.Columns.Clear();
            DataGridTextColumn firstNameColumn = new DataGridTextColumn
            {
                Header = "First Name", // This is what the user sees
                Binding = new Binding("FirstName") //This is the property to display from the Person class
            };
            HomeDataGrid.Columns.Add(firstNameColumn);

            DataGridTextColumn lastNameColumn = new DataGridTextColumn
            {
                Header = "Last Name",
                Binding = new Binding("LastName")
            };
            HomeDataGrid.Columns.Add(lastNameColumn);
            DataGridTextColumn PersonIDColumn = new DataGridTextColumn
            {
                Header = "Person ID",
                Binding = new Binding("PersonID")

            };
            HomeDataGrid.Columns.Add(PersonIDColumn);
            DataGridTextColumn EmailColumn = new DataGridTextColumn
            {
                Header = "Email",
                Binding = new Binding("Email")

            };
            HomeDataGrid.Columns.Add(EmailColumn);
            DataGridTextColumn PhoneColumn = new DataGridTextColumn
            {
                Header = "Phone Number",
                Binding = new Binding("Phone")

            };
            HomeDataGrid.Columns.Add(PhoneColumn);


        }



        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            HomeDataGrid.DataContext = homeSalesCollection;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            HomeDataGrid.DataContext = realEstateCompaniesCollection;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            HomeDataGrid.DataContext = homeCollection;
        }
    }
}
