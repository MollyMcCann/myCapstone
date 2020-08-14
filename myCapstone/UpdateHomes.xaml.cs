﻿using HomeTrackerDatamodelLibrary;
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
        HomeSalesCollection homesalesCollection;

        public UpdateHomes(PeopleCollection people, HomeCollection homes)
        {
            InitializeComponent();
            peopleUd = people;
            HomeUd = homes;
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


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Person personObject = new Person();
            HomeTrackerDatamodelLibrary.Agent agentObject = new HomeTrackerDatamodelLibrary.Agent();
            personObject.FirstName = firstName.Text;
            personObject.LastName = lastName.Text;
            personObject.Phone = phoneNumber.Text;
            personObject.Email = email.Text;
            decimal commPerc;

            if( !decimal.TryParse(comissionPercentage.Text, out commPerc))
            {
                // TODO: Notify user of failure
                return;
            }

            agentObject.CommissionPercent = commPerc;

            personObject.Agent = agentObject;
            peopleUd.Add(personObject);
        }
    }
}
