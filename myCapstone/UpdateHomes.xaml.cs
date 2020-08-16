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
        HomeCollection homeCollection;
        HomeSalesCollection homeSalesCollection;
        RealEstateCompanyCollection realEstateCompaniesCollection;


        public UpdateHomes(PeopleCollection people, HomeCollection homes, HomeSalesCollection homeSales,
            RealEstateCompanyCollection realEstateCompanies)
        {
            InitializeComponent();
            peopleUd = people;
            HomeUd = homes;
            homeSalesCollection = homeSales;
            Loaded += UpdateHomes_Loaded ;
        }

        private void UpdateHomes_Loaded(object sender, RoutedEventArgs e)
        {
            LoadHomesList();
        }

        private void LoadHomesList()

        {
            using (var db = new HomeTrackerModel1())
            {
                //var homeList = db.Homes.OrderBy(h => h.HomeID).Select(h => new Home(){ HomeID = h.HomeID, Address = h.Address }).ToList();
                //this.HomeListBox.DisplayMemberPath = "Address";
                //this.HomeListBox.SelectedValuePath = "HomeID";
                //this.HomeListBox.ItemsSource = homeList;

                homeCollection = new HomeCollection(db.Homes.ToList());
                this.HomeListBox.DisplayMemberPath = "Address";
                this.HomeListBox.SelectedValuePath = "HomeID";
                this.HomeListBox.ItemsSource = homeCollection;

                realEstateCompaniesCollection = new RealEstateCompanyCollection(db.RealEstateCompanies.ToList());
                this.CompanyListBox.DisplayMemberPath = "CompanyName";
                this.CompanyListBox.SelectedValuePath = "CompanyId";
                this.CompanyListBox.ItemsSource = realEstateCompaniesCollection;


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAgent.IsChecked.HasValue && (bool)CheckAgent.IsChecked)
            {
                Person personObject = new Person();
                HomeTrackerDatamodelLibrary.Agent agentObject = new HomeTrackerDatamodelLibrary.Agent();
                personObject.FirstName = firstName.Text;
                personObject.LastName = lastName.Text;
                personObject.Phone = phoneNumber.Text;
                personObject.Email = email.Text;
                decimal commPerc;

                if (!decimal.TryParse(comissionPercentage.Text, out commPerc))
                {
                    // TODO: Notify user of failure
                    return;
                }

                agentObject.CommissionPercent = commPerc;
                //todo: 1. add agent company to UI, listbox
                //2. use a similar pattern to the house list box. get the DisplayValuePath will be Company Name
                //    DisplayValuePath will be CompanyID
                //3. will be similar to the comment below:
                //if (HomeListBox.SelectedIndex == -1)
                //{
                //    return;
                //    //todo tell user we cannot add the home
                //}
                //agentObject.CompanyID = (int)HomeListBox.SelectedValue;
                if (CompanyListBox.SelectedIndex == -1)
                {
                    return;
                   
                }
                agentObject.CompanyID = (int)CompanyListBox.SelectedValue;// is returning a null value...why?


                personObject.Agent = agentObject;
                peopleUd.Add(personObject);

                HomeSale homeSalesObject = new HomeSale();
                if(HomeListBox.SelectedIndex == -1)
                {
                    return;
                    //todo tell user we cannot add the home
                }
                homeSalesObject.HomeID = (int)HomeListBox.SelectedValue;
                homeSalesObject.AgentID = personObject.Agent.AgentID;
                homeSalesObject.MarketDate = DateTime.Now;
                //homeSalesObject.CompanyID = personObject.Agent.CompanyID; 
                homeSalesObject.CompanyID = CompanyListBox.SelectedIndex;//fix this
                decimal SAmount;

                if (!decimal.TryParse(SaleAmount.Text, out SAmount))
                {
                    // TODO: Notify user of failure
                    return;
                }

               
                homeSalesObject.SaleAmount = SAmount;

                homeSalesCollection.Add(homeSalesObject);
            }
            else if(CheckBuyer.IsChecked.HasValue && (bool)CheckBuyer.IsChecked)
            {
                Person personObject = new Person();
                HomeTrackerDatamodelLibrary.Buyer buyerObject = new HomeTrackerDatamodelLibrary.Buyer();
                personObject.FirstName = firstNameB.Text;
                personObject.LastName = lastNameB.Text;
                personObject.Phone = phoneNumberB.Text;
                personObject.Email = emailB.Text;
                int credRate;
                if(! int.TryParse(creditRatingB.Text, out credRate))
                {
                    return;
                }
                buyerObject.CreditRating = credRate;

                personObject.Buyer = buyerObject;
                peopleUd.Add(personObject);

                HomeSale homeSalesObject = new HomeSale();
                if (HomeListBox.SelectedIndex == -1)
                {
                    return;
                    //todo tell user we cannot add the home
                }
                
                homeSalesObject.HomeID = (int)HomeListBox.SelectedValue;
                homeSalesObject.SoldDate = DateTime.Now;
                decimal SAmount;

                if (!decimal.TryParse(SaleAmount.Text, out SAmount))
                {
                    // TODO: Notify user of failure
                    return;
                }
                homeSalesObject.SaleAmount = SAmount;
                homeSalesObject.BuyerID = personObject.Buyer.BuyerID;
                homeSalesObject.CompanyID = CompanyListBox.SelectedIndex;//fix this
                homeSalesCollection.Add(homeSalesObject);



            }

        }
    }
}
