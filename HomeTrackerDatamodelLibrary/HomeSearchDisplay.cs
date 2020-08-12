using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace HomeTrackerDatamodelLibrary
//{
//   public class HomeSearchDisplay : IComparable <HomeSearchDisplay>
//    {
//        //Public properties
//        public int HomeId { get; private set; }
//        public string Address { get; private set; }
//        public string City { get; private set; }
//        public int Zip { get; private set; }
//        public int OwnerID { get; private set; }

//        //Constructor that takes a Home and a Person
//        public HomeSearchDisplay(Home h, Person p)
//        {
//            //Set the incoming info to the appropriate properties
//            //what do I look for here?
            
//            HomeID = h.HomeID;
//            Address = h.ToString();
//            OwnerID = p.OwnerID;
//            Year = b.YearPublished;

//            //do I need this?
//            ////Set the Availability string based on how many books are available
//            //if (b.Availability == 0)
//            //{
//            //    //No copies are available
//            //    Availability = "Checked Out";
//            //}
//            //else if (b.Availability == 1)
//            //{
//            //    //Only one copy is available
//            //    Availability = "1 Copy Available";
//            //}
//            //else
//            //{
//            //    //More than one copy is available
//            //    Availability = string.Format("{0} Copies Available", b.Availability);
//            //}
//        }

//        //Implementation of IComparable
//        public int CompareTo(HomeSearchDisplay other)
//        {
//            //If the title being compared is null, the title that does exist is "greater"
//            if (other == null)
//                return 1;

//            //When comparing two BookSearchDisplay instances, sort by Title
//            return HomeId.CompareTo(other.HomeId);
//        }
//    }
//}

