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
        HomeCollection listedHomes;
        HomeSale homeSaleToSold;


        public UpdateHomes(PeopleCollection people, HomeCollection homes, HomeSalesCollection homeSales,
            RealEstateCompanyCollection realEstateCompanies)
        {
            InitializeComponent();
            peopleUd = people;
            HomeUd = homes;
            homeSalesCollection = homeSales;
            IsVisibleChanged += UpdateHomes_IsVisibleChanged;
            Closing += UpdateHomes_Closing;
        }

        private void UpdateHomes_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void UpdateHomes_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoadHomesList();
        }

        private void LoadHomesList()

        {
            using (var db = new HomeTrackerModel1())
            {
                homeCollection = new HomeCollection(db.Homes.ToList());
                this.HomeListBox.DisplayMemberPath = "Address";
                this.HomeListBox.SelectedValuePath = "HomeID";
                this.HomeListBox.ItemsSource = homeCollection;

                realEstateCompaniesCollection = new RealEstateCompanyCollection(db.RealEstateCompanies.ToList());
                this.CompanyListBox.DisplayMemberPath = "CompanyName";
                this.CompanyListBox.SelectedValuePath = "CompanyID";
                this.CompanyListBox.ItemsSource = realEstateCompaniesCollection;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAgent.IsChecked.HasValue && (bool)CheckAgent.IsChecked)
            {
                try
                {
                    //TODO: only allow home to be listed once
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
                  
                    if (CompanyListBox.SelectedIndex == -1)
                    {
                        return;

                    }
                    agentObject.CompanyID = (int)CompanyListBox.SelectedValue;


                    personObject.Agent = agentObject;
                    peopleUd.Add(personObject);

                    HomeSale homeSalesObject = new HomeSale();
                    if (HomeListBox.SelectedIndex == -1)
                    {
                        return;
                        //todo tell user we cannot add the home
                    }
                    homeSalesObject.HomeID = (int)HomeListBox.SelectedValue;
                    homeSalesObject.AgentID = personObject.Agent.AgentID;
                    homeSalesObject.MarketDate = DateTime.Now;
                    //homeSalesObject.CompanyID = personObject.Agent.CompanyID; 
                    homeSalesObject.CompanyID = (int)CompanyListBox.SelectedValue;//fix this
                    decimal SAmount;

                    if (!decimal.TryParse(SaleAmount.Text, out SAmount))
                    {
                        // TODO: Notify user of failure
                        return;
                    }


                    homeSalesObject.SaleAmount = SAmount;

                    homeSalesCollection.Add(homeSalesObject);
                }
                catch(Exception ex)
                {
                 
                }
            }
            else if(CheckBuyer.IsChecked.HasValue && (bool)CheckBuyer.IsChecked)
            {
                try
                {

                    Person personObject = new Person();
                    HomeTrackerDatamodelLibrary.Buyer buyerObject = new HomeTrackerDatamodelLibrary.Buyer();
                    personObject.FirstName = firstNameB.Text;
                    personObject.LastName = lastNameB.Text;
                    personObject.Phone = phoneNumberB.Text;
                    personObject.Email = emailB.Text;
                    int credRate;
                    if (!int.TryParse(creditRatingB.Text, out credRate))
                    {
                        return;
                    }
                    buyerObject.CreditRating = credRate;

                    personObject.Buyer = buyerObject;
                    peopleUd.Add(personObject);

                    if (HomeListBox.SelectedIndex == -1)
                    {
                        return;
                        //todo tell user we cannot add the home
                    }

                    int selectedHomeID = (int)HomeListBox.SelectedValue;
                    
                    homeSaleToSold = (from hs in homeSalesCollection
                                     where hs.HomeID == selectedHomeID
                                      select hs).First();

                    homeSaleToSold.SoldDate = DateTime.Now;
                    homeSaleToSold.BuyerID = personObject.Buyer.BuyerID;

                    homeSalesCollection.Update(homeSaleToSold);
                }
                catch (Exception ex)
                {
                    //TODO: notify user
                }

            }

            this.Close();
        }

        private void CheckAgent_Checked(object sender, RoutedEventArgs e)
        {
          
            using (var db = new HomeTrackerModel1())
            {
                homeCollection = new HomeCollection(db.Homes.ToList());
                this.HomeListBox.DisplayMemberPath = "Address";
                this.HomeListBox.SelectedValuePath = "HomeID";
                this.HomeListBox.ItemsSource = homeCollection;
            }
        }

        private void CheckBuyer_Checked(object sender, RoutedEventArgs e)
        {
            var listedJoinQuerey = from home in homeCollection
                                   join homeSale in homeSalesCollection on home.HomeID equals homeSale.HomeID
                                   where homeSale.SoldDate == null
                                   select home;

            listedHomes = new HomeCollection(listedJoinQuerey.ToList());
            this.HomeListBox.DisplayMemberPath = "Address";
            this.HomeListBox.SelectedValuePath = "HomeID";
            this.HomeListBox.ItemsSource = listedHomes;
        }
    }
}
