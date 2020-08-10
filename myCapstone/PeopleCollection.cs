using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;

namespace HomeTrackerTest
{
    class PeopleCollection
    {
       
        public class PeopleCollection1<T>
        : IEnumerator<T>, IEnumerable<T>
        where T : Person, IID
        {
            private List<T> _peopleList;
            int position = -1;
            public PeopleCollection1()
            {
                _peopleList = new List<T>();
            }
            public PeopleCollection1(List<T> people)
            {
                _peopleList = people;
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


            public T Current => _peopleList[position];

            object IEnumerator.Current => _peopleList[position];



            public void Add(T person)
            {
                var p1 = _peopleList.SingleOrDefault(p => p.Id == person.Id);
                if (p1 == null)
                {
                    _peopleList.Add(person);
                }
            }



            public void Dispose()
            {
                //throw new NotImplementedException();
            }



            public T Get(int id)//check in on this
            {
                return _peopleList.SingleOrDefault(p => p.Id == id);
            }



            public IEnumerator<T> GetEnumerator()
            {
                return this;
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



            public int Length => _peopleList.Count;

            //object IEnumerator.Current => throw new NotImplementedException();

            public T this[int index]
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



        }



        public class Buyer : Person
        {



        }



        public interface IID
        {
            int Id { get; set; }
        }



        public class Person : IID
        {
            public int Id { get; set; }
        }

        //class Program
        //{
        //private static void Main(string[] args)//cant have main again as it is the 
        //    //entry point in  Program class so get that sorted
        //    {
        //        var peopleCollection = new PeopleCollection<Person>();
        //        peopleCollection.Add(new Agent() { Id = 1 });
        //        peopleCollection.Add(new Owner() { Id = 2 });
        //        peopleCollection.Add(new Buyer() { Id = 3 });
        //        peopleCollection.Add(new Buyer() { Id = 3 });



        //        var buyer = peopleCollection.Get(3);



        //        foreach (var p in peopleCollection)
        //        {
        //            var id = p.Id;
        //        }



        //        var b = peopleCollection[2];
        //        peopleCollection[2] = new Buyer() { Id = 4 };



        //    }

    }
}

