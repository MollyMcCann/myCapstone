using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;


//namespace myCapstone
//{
//      //Contains the People, Home, homeSales and RealEstateCompanies collections along with methods to search and manipulate those collections
//        public class Collections
//        {
//            //Connection to the database
//            private HomeTrackerModel1 db = new HomeTrackerModel1();

//            //Private variables
//            //private PeopleCollection _peopleCollection = new PeopleCollection();
//            //private HomeCollection HomeCollection = new HomeCollection();
//            //private HomeSalesCollection _homesalesCollection = new HomeSalesCollection();
//            //private RealEstateCompanyCollection _realEstateCompanyCollection = new RealEstateCompanyCollection();

//            //Public properties
//            public PeopleCollection PeopleCollection { get; set; } 
//            public HomeCollection HomeCollection { get; set; }
//            public HomeSalesCollection HomeSalesCollection { get; set; }
//             public RealEstateCompanyCollection RealEstateCompanyCollection { get; set; }


//        //Constructor
//        public Collections()
//            {
//            //Initialize the collections by loading them with the appropriate data from the database
//            PeopleCollection = new PeopleCollection();
//            HomeCollection = new HomeCollection();
//            HomeSalesCollection = new HomeSalesCollection();
//            RealEstateCompanyCollection = new RealEstateCompanyCollection();
//                LoadData();
//            }

//            //METHOD: Initialize the People,real estate companies, home sales and home collections
//            private void LoadData()
//            {
//                try
//                {
//                    //Clear all collections before (re)writing to them so there are no duplicates
//                    HomeCollection.Clear();
//                    PeopleCollection.Clear();
//                    HomeSalesCollection.Clear();
//                    RealEstateCompanyCollection.Clear();

//                //Query the database to retrieve all People
//                var peopleObjects = from p in db.People select p;
//                    //Add each person to the PeopleCollection as their child type
//                    foreach (var p in peopleObjects)
//                    {
//                        if (p is Agent)
//                            PeopleCollection[0] = p as Agent;

//                        if (p is Buyer)
//                            PeopleCollection[0] = p as Buyer;

//                        if (p is Owner)
//                            PeopleCollection[0] = p as Owner;
//                    }
//                    //Now that all people have been added, perform a sort
//                    PeopleCollection.Sort();

//                    //Query the database to retrieve all Homes
//                    var homeObjects = from h in db.Homes select h;
//                //Add each home to the HomeCollection
//                foreach(Home h in homeObjects)
//                    {
//                         HomeCollection.Add(h);
//                       }
//                //Now that all homes have been added, perform a sort
//                HomeCollection.Sort();
//                //Query the database to retrieve all HomeSales
//                    var homeSalesObjects = from hs in db.HomeSales select hs;
//                    //Add each log to the HomeSalescollection
//                    foreach(HomeSale hs in homeSalesObjects)
//                    {
//                         HomeSalesCollection.Add(hs);
//                       }
//                    //Now that all the Home sales have been added, perform a sort
//                    HomeSalesCollection.Sort();

//                    //Query the database to retrieve all real Estate Companies
//                    var realEstateObject = from re in db.RealEstateCompanies select re;
//                //Add each company to the RealEstateCompanyCollection
//                foreach (RealEstateCompany re in realEstateObject)
//                {
//                    RealEstateCompanyCollection.Add(re);
//                }
//                //Now that all companies have been added, perform a sort
//                RealEstateCompanyCollection.Sort();
//            }
//                catch (Exception ex)
//                {
//                    //Write any exceptions to the ErrorLog.txt file
//                    var exHandler = new ExceptionHandler(ex);
//                    //should i take this out or leave it in?

//                }
//            }

//            //METHOD: Searches for a match in each book's Title, Author, Subject, and ISBN, and adds all matches to a BookSearchDisplay List
//            internal void HomeSearchAll(string query, out List<HomeSearchDisplay> resultsList)
//            {
//                //Ensure the given resultsList is clear of any items
//                resultsList = new List<HomeSearchDisplay>();

//                //Go through each book and search for a match between the given Query and the book's Title, Subject, or ISBN
//                foreach (Home h in HomeCollection)
//                {
//                    if ((h.HomeID().Contains(query.ToUpper()) || b.Subject.ToUpper().Contains(query.ToUpper()) || b.ISBN.ToString().Contains(query))
//                        && b.NumberOfCopies > 0)
//                    {
//                        //If a match is found, get that book's author
//                        foreach (var p in PeopleCollection)
//                        {
//                            if (p.OwnerID == p.PersonID)
//                            {
//                                //Add the matching result to the list
//                                var result = new HomeSearchDisplay(h, p);
//                                resultsList.Add(result);
//                            }
//                        }
//                    }
//                }
//                //Go through each author and search for a match between the given Query and the author's First or Last Name
//                foreach (People p in PeopleCollection)
//                {
//                    if (p is Authors)
//                    {
//                        if (p.FirstName.ToUpper().Contains(query.ToUpper()) || p.LastName.ToUpper().Contains(query.ToUpper()))
//                        {
//                            foreach (var b in BookCollection)
//                            {
//                                //If a match is found, get each book that the author has written
//                                if (b.AuthorID == p.PersonID && b.NumberOfCopies > 0)
//                                {
//                                    //Checks if the match already exists in the Results List (from the previously performed Title/Subject/ISBN search
//                                    bool duplicate = false;
//                                    foreach (var d in resultsList)
//                                    {
//                                        if (b.BookID == d.ID)
//                                            duplicate = true;
//                                    }
//                                    if (!duplicate)
//                                    {
//                                        //If the match does NOT already exist (therefore ensuring it is not a duplicate), add the
//                                        //result to the list
//                                        var result = new BookSearchDisplay(b, p);
//                                        resultsList.Add(result);
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }

//                //Perform a sort on the results list
//                resultsList.Sort();
//            }

//            //METHOD: Searches for a match in each books Title and adds all matches to a BookSearchDisplay List
//            internal void BookSearchByTitle(string query, out List<BookSearchDisplay> resultsList)
//            {
//                //Ensure the given resultsList is clear of any items
//                resultsList = new List<BookSearchDisplay>();

//                //Find any books that contain the incoming query in the Title
//                foreach (Books b in BookCollection)
//                {
//                    if (b.Title.ToUpper().Contains(query.ToUpper()) && b.NumberOfCopies > 0)
//                    {
//                        //If a book is found, get the book's author
//                        foreach (var a in PeopleCollection)
//                        {
//                            if (b.AuthorID == a.PersonID)
//                            {
//                                //Add the result to the BookSearchDisplay list
//                                var result = new BookSearchDisplay(b, a);
//                                resultsList.Add(result);
//                            }
//                        }
//                    }
//                }

//                //Perform a sort on the results list
//                resultsList.Sort();
//            }

//            //METHOD: Searches for a match in each books Author and adds all matches to a BookSearchDisplay List
//            internal void BookSearchByAuthor(string query, out List<BookSearchDisplay> resultsList)
//            {
//                //Ensure the given resultsList is clear of any items
//                resultsList = new List<BookSearchDisplay>();

//                //Find any authors that contain the incoming query in their first or last name
//                foreach (People p in PeopleCollection)
//                {
//                    if (p is Authors)
//                    {
//                        if (p.FirstName.ToUpper().Contains(query.ToUpper()) || p.LastName.ToUpper().Contains(query.ToUpper()))
//                        {
//                            //If an author was found, add each of their books to the results list
//                            foreach (var b in _bookCollection)
//                            {
//                                if (b.AuthorID == p.PersonID && b.NumberOfCopies > 0)
//                                {
//                                    var result = new BookSearchDisplay(b, p);
//                                    resultsList.Add(result);
//                                }
//                            }
//                        }
//                    }
//                }

//                //Perform a sort on the results list
//                resultsList.Sort();
//            }

//            //METHOD: Searches for a match in each books Author ID and adds all matches to a list of Books
//            internal void BookSearchByAuthor(int personID, out List<Books> resultsList)
//            {
//                //Ensure the given resultsList is clear of any items
//                resultsList = new List<Books>();

//                //Add each book to the results list where the Author ID is the same as the incoming Person ID
//                foreach (var b in BookCollection)
//                {
//                    if (b.AuthorID == personID)
//                    {
//                        resultsList.Add(b);
//                    }
//                }

//                //Perform a sort on the results list
//                resultsList.Sort();
//            }

//            //METHOD: Searches for a match in each books ISBN and adds all matches to a BookSearchDisplay List
//            internal void BookSearchByISBN(string query, out List<BookSearchDisplay> resultsList)
//            {
//                //Ensure the given resultsList is clear of any items
//                resultsList = new List<BookSearchDisplay>();

//                //Find any books that contain the incoming query in their ISBN
//                foreach (Books b in BookCollection)
//                {
//                    if (b.ISBN.ToString().Contains(query) && b.NumberOfCopies > 0)
//                    {
//                        //If a book match was found, get that book's author
//                        foreach (var a in _peopleCollection)
//                        {
//                            if (b.AuthorID == a.PersonID)
//                            {
//                                //Create a new BookSearchDisplay with the book and author info and add it to the results list
//                                var result = new BookSearchDisplay(b, a);
//                                resultsList.Add(result);
//                            }
//                        }
//                    }
//                }

//                //Perform a sort on the results list
//                resultsList.Sort();
//            }

//            //METHOD: Searches for a match in each books Subject and adds all matches to a BookSearchDisplay List
//            internal void BookSearchSubject(string query, out List<BookSearchDisplay> resultsList)
//            {
//                resultsList = new List<BookSearchDisplay>();

//                foreach (Books b in BookCollection)
//                {
//                    if (b.Subject.ToUpper().Contains(query.ToUpper()) && b.NumberOfCopies > 0)
//                    {
//                        foreach (var a in PeopleCollection)
//                        {
//                            if (b.AuthorID == a.PersonID)
//                            {
//                                var result = new BookSearchDisplay(b, a);
//                                resultsList.Add(result);
//                            }
//                        }
//                    }
//                }

//                //Perform a sort on the results list
//                resultsList.Sort();
//            }

//            //METHOD: Searches for a matching book from a given Book ID
//            internal Books BookSearchByID(int bookID)
//            {
//                //Find the book that has the incoming Book ID
//                foreach (Books b in BookCollection)
//                {
//                    if (b.BookID == bookID)
//                    {
//                        return b;
//                    }
//                }

//                //If no book with that Book ID was found, return null
//                return null;
//            }

//            //METHOD: Get a list of all books a cardholder has checked out using their Library Card ID, and add the results to a list of CheckInDisplays
//            internal bool LogSearchByLibraryCardID(string cardID, out List<CheckInDisplay> resultsList)
//            {
//                //Ensure the given resultsList is clear of any items
//                resultsList = new List<CheckInDisplay>();

//                bool idFound = false;

//                //Find the cardholder with a matching Library Card ID
//                foreach (var p in PeopleCollection)
//                {
//                    if (p is Cardholders)
//                    {
//                        Cardholders temp = p as Cardholders;
//                        if (cardID.Trim().ToUpper() == temp.LibraryCardID.ToUpper())
//                        {
//                            idFound = true;
//                            //If a matching cardholder was found, find what they have checked out in the CheckOutLog collection
//                            foreach (var co in CheckedOutCollection)
//                            {
//                                if (co.CardholderID == temp.PersonID)
//                                {
//                                    //If the cardholder has a book checked out, get that book's information
//                                    foreach (var b in BookCollection)
//                                    {
//                                        if (b.BookID == co.BookID)
//                                        {
//                                            string status;
//                                            if (co.IsOverdue)
//                                                status = "OVERDUE";
//                                            else
//                                                status = "On Time";
//                                            //Create a new CheckInDisplay with the appropriate information, and add it to the results list
//                                            resultsList.Add(new CheckInDisplay(co.CheckOutLogID, b.BookID, b.Title, status));
//                                            break;
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                        //If the given Library Card ID was found, return true
//                        if (idFound)
//                            return true;
//                    }
//                }
//                //If the given Library Card ID was not found, return false
//                return false;
//            }
//        }
//    }



