using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;
using System.Data.Entity;

namespace myCapstone
{

    public class PeopleCollection
        : IEnumerator<Person>, IEnumerable<Person>


    {
        private List<Person> _peopleList;
        int position = -1;
        public PeopleCollection()
        {
            _peopleList = new List<Person>();
        }
        public PeopleCollection(List<Person> people)
        {
            _peopleList =  people;

            if (_peopleList.Count > 0)
            {
                position = 0;
            }
        }
        public void Sort()
        {
            _peopleList.Sort();
        }

        public int Count
        {
            get { return _peopleList.Count; }
        }

        public void Clear()
        {
            _peopleList.Clear();
        }


        public Person Current => _peopleList[position];

        object IEnumerator.Current => _peopleList[position];



        public void Add(Person person)
        {
            person.PersonID = getLastId() + 1;

            var p1 = _peopleList.SingleOrDefault(p => p.PersonID == person.PersonID);
            if (p1 == null)
            {
                _peopleList.Add(person);
                using (HomeTrackerModel1 db = new HomeTrackerModel1())
                {

                    try
                    {
                        
                        db.Entry(person).State = EntityState.Added;

                        if (person.Owner != null)
                        {
                            person.Owner.OwnerID = person.PersonID;
                            db.Entry(person.Owner).State = EntityState.Added;
                            
                        }
                        if (person.Agent != null)
                        {
                            person.Agent.AgentID = person.PersonID;
                            db.Entry(person.Agent).State = EntityState.Added;
                            
                        }
                        if (person.Buyer != null)
                        {
                            person.Buyer.BuyerID = person.PersonID;
                            db.Entry(person.Buyer).State = EntityState.Added;
                            
                        }

                        db.SaveChanges(); 
                    }
                    catch (Exception ex )
                    {
                        int i = 0;
                    }
                }
            }
        }

        private int getLastId()
        {
            int ret = -1;
            using (HomeTrackerModel1 db = new HomeTrackerModel1())
            {
                ret = db.People.Max(p => p.PersonID);
            }

            return ret;
        }




        public void Dispose()
        {
            //throw new NotImplementedException();
        }


        public Person Get(int id)
        {
            return _peopleList.SingleOrDefault(p => p.PersonID  == id);
        }

        public bool MoveNext()
        {
            position++;
            return (position < _peopleList.Count);
        }



        public void Reset()
        {
            position = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Person>)_peopleList).GetEnumerator();
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return ((IEnumerable<Person>)_peopleList).GetEnumerator();
        }

        public int Length => _peopleList.Count;


        public Person this[int index]
        {
            get => _peopleList[index];
            set => _peopleList[index] = value;
        }

    }



    public class Agent : Person
    {



    }



    public class Owner : Person
    {
        public string PreferredLender { get; set; }

    }



    public class Buyer : Person
    {



    }



}


