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
    /// Interaction logic for AddHome.xaml
    /// </summary>
    public partial class AddHome : Window
    {
        PeopleCollection peopleData;
        HomeCollection homeData;
        public AddHome(PeopleCollection people, HomeCollection homes)
        {
            InitializeComponent();
            peopleData = people;
            homeData = homes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Person personObject = new Person();
            HomeTrackerDatamodelLibrary.Owner ownerObject = new HomeTrackerDatamodelLibrary.Owner();
            personObject.FirstName = firstName.Text;
            personObject.LastName = LastName.Text;
            personObject.Phone = PhoneNumber.Text;
            personObject.Email = Email.Text;
            ownerObject.PreferredLender = PreferredLender.Text;
            
            personObject.Owner = ownerObject;
            peopleData.Add(personObject);

            Home homeObject = new Home();
            homeObject.Owner = ownerObject;
            //HomeTrackerDatamodelLibrary.= new HomeTrackerDatamodelLibrary.Home();
            //homeObject.HomeID = HomeId.Text;
            //int HID;

            //if (!int.TryParse(HomeId.Text, out HID))
            //{
            //    // TODO: Notify user of failure
            //    return;
            //}
            //homeObject.HomeID = HID;
            homeObject.Address = Address.Text;
            homeObject.City = City.Text;
            homeObject.State = State.Text;
            homeObject.Zip = ZipCode.Text;

            homeData.Add(homeObject);

        }

    }
}
