using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Xml.Linq;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace HomeTrackerDatamodelLibrary
{
    public class HomeTrackerInitializer : CreateDatabaseIfNotExists<HomeTrackerModel1>
    {
        protected override void Seed(HomeTrackerModel1 context)
        {
            base.Seed(context);
            CustomInitializer();
        }

         public  static void CustomInitializer()
        {
            using (HomeTrackerModel1 context = new HomeTrackerModel1())
            {
                //load data from xml
                List<Person> people = GetPeopleFromXML();

                foreach (Person p in people)
                {
                    context.People.Add(p);
                }
                List<Owner> owners = GetOwnersFromXML();

                foreach (Owner o in owners)
                {
                    context.Owners.Add(o);
                }

                List<Home> homes = GetHomesFromXML();

                foreach (Home h in homes)
                {
                    context.Homes.Add(h);
                }

                List<RealEstateCompany> realEstateCompanies = GetRealEstateCompaniesFromXML();

                foreach (RealEstateCompany re in realEstateCompanies)
                {
                    context.RealEstateCompanies.Add(re);
                }

                List<Agent> agents = GetAgentsFromXML();

                foreach (Agent a in agents)
                {
                    context.Agents.Add(a);
                }

                List<Buyer> buyers = GetBuyersFromXML();

                foreach (Buyer b in buyers)
                {
                    context.Buyers.Add(b);
                }


                List<HomeSale> homesales = GetHomeSalesFromXML();

                foreach (HomeSale hs in homesales)
                {
                    context.HomeSales.Add(hs);
                }

                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation(
                                  "Class: {0}, Property: {1}, Error: {2}",
                                  validationErrors.Entry.Entity.GetType().FullName,
                                  validationError.PropertyName,
                                  validationError.ErrorMessage);
                        }
                    }
                }

            }
        }

        static List<Agent> GetAgentsFromXML()
        {
            //get contents of xml file
            var agentXML = (from d in XDocument.Load(@"XMLFiles\Agents.xml").Descendants("Agent")
                            select d).ToList();

            //set up collection to store data from xml file
            List<Agent> agents = new List<Agent>(agentXML.Count);


            foreach (var a in agentXML)
            {
                Agent agent = new Agent();

                //Fill agent object
                agent.AgentID = int.Parse(a.Element("AgentID").Value);

                var CompIdElement = (a.Element("CompanyID")?.Value);

                if (CompIdElement == null)
                {
                    agent.CompanyID = null;
                }
                else
                {
                    agent.CompanyID = int.Parse(CompIdElement);
                }
                
                agent.CommissionPercent = decimal.Parse(a.Element("CommissionPercent").Value);
                
                //Add to list
                agents.Add(agent);
            }

            return agents;
        }

        static List<Buyer> GetBuyersFromXML()
        {
            var buyerXML = (from b in XDocument.Load(@"XMLFiles\Buyers.xml").Descendants("Buyer")
                            select b).ToList();

            List<Buyer> buyers = new List<Buyer>(buyerXML.Count);

            foreach (var b in buyerXML)
            {
                Buyer buyer = new Buyer();

                buyer.BuyerID = int.Parse(b.Element("BuyerID").Value);
                var CreditRateElement = (b.Element("Company")?.Value);

                if (CreditRateElement == null)
                {
                    buyer.CreditRating = null;
                }
                else
                {
                    buyer.CreditRating = int.Parse(CreditRateElement);
                }


   

                buyers.Add(buyer);
            }

            return buyers;
        }
        static List<Home> GetHomesFromXML()
        {
            var homesXML = (from h in XDocument.Load(@"XMLFiles\Homes.xml").Descendants("Home")
                            select h).ToList();

            List<Home> homes = new List<Home>(homesXML.Count);

            foreach (var h in homesXML)
            {
                Home home = new Home();

                home.HomeID = int.Parse(h.Element("HomeID").Value);
                home.Address = h.Element("Address")?.Value.Trim();
                home.City = h.Element("City").Value.Trim();
                home.State = h.Element("State").Value.Trim();
                home.Zip = h.Element("Zip").Value.Trim();
                home.OwnerID = int.Parse(h.Element("OwnerID").Value);

                homes.Add(home);
            }

            return homes;
        }
        static List<HomeSale> GetHomeSalesFromXML()
        {
            var homeSaleXML = (from hs in XDocument.Load(@"XMLFiles\HomeSales.xml").Descendants("HomeSale")
                               select hs).ToList();

            List<HomeSale> homeSales = new List<HomeSale>(homeSaleXML.Count);

            foreach (var hs in homeSaleXML)
            {
                HomeSale homeSale = new HomeSale();

                homeSale.SaleID = int.Parse(hs.Element("SaleID").Value);
                homeSale.HomeID = int.Parse(hs.Element("HomeID").Value);
                var SoldDateElement = (hs.Element("SoldDate")?.Value);

                if (SoldDateElement == null)
                {
                    homeSale.SoldDate = null;
                }
                else
                {
                    homeSale.SoldDate = DateTime.Parse(SoldDateElement);
                }
              
                homeSale.AgentID = int.Parse(hs.Element("AgentID").Value);
                homeSale.SaleAmount = Decimal.Parse(hs.Element("SaleAmount").Value);
                
                var BuyerIdElement = (hs.Element("BuyerID")?.Value);

                if (BuyerIdElement == null)
                {
                    homeSale.BuyerID = null;
                }
                else
                {
                    homeSale.BuyerID= int.Parse(BuyerIdElement);
                }


                homeSale.MarketDate = DateTime.Parse(hs.Element("MarketDate").Value);
                homeSale.CompanyID = int.Parse(hs.Element("CompanyID").Value);

                homeSales.Add(homeSale);
            }

            return homeSales;

        }
        static List<Owner> GetOwnersFromXML()
        {
            var ownerXML = (from o in XDocument.Load(@"XMLFiles\Owners.xml").Descendants("Owner")
                            select o).ToList();

            List<Owner> owners = new List<Owner>(ownerXML.Count);

            foreach (var o in ownerXML)
            {
                Owner owner = new Owner();

                owner.OwnerID = int.Parse(o.Element("OwnerID").Value);
                owner.PreferredLender = o.Element("PreferredLender")?.Value;

                owners.Add(owner);
            }

            return owners;

        }
        static List<Person> GetPeopleFromXML()
        {
            var peopleXML = (from p in XDocument.Load(@"XMLFiles\People.xml").Descendants("Person")
                             select p).ToList();

            List<Person> people = new List<Person>(peopleXML.Count);

            foreach (var p in peopleXML)
            {
                Person person = new Person();

                person.PersonID = int.Parse(p.Element("PersonID").Value);
                person.FirstName = p.Element("FirstName")?.Value;
                person.LastName = p.Element("LastName")?.Value;
                person.Phone = p.Element("Phone")?.Value;
                person.Email = p.Element("Email")?.Value;



                people.Add(person);
            }

            return people;

        }
        static List<RealEstateCompany> GetRealEstateCompaniesFromXML()
        {
            var realEstateCompanyXML = (from re in XDocument.Load(@"XMLFiles\RealEstateCompanies.xml").Descendants("RealEstateCompany")
                                        select re).ToList();

            List<RealEstateCompany> realEstateCompanies = new List<RealEstateCompany>(realEstateCompanyXML.Count);

            foreach (var re in realEstateCompanyXML)
            {
                RealEstateCompany realEstateCompany = new RealEstateCompany();

                realEstateCompany.CompanyID = int.Parse(re.Element("CompanyID").Value);
                realEstateCompany.CompanyName = re.Element("CompanyName")?.Value;
                realEstateCompany.Phone = re.Element("Phone").Value;

                realEstateCompanies.Add(realEstateCompany);
            }

            return realEstateCompanies;
        }
    }
}
