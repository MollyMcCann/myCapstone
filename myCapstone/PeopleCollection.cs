﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;

namespace myCapstone
{

    public class PeopleCollection<People>
        : IEnumerator<People>, IEnumerable<People>
        
        {
            private List<People> _peopleList;
            int position = -1;
            public PeopleCollection()
            {
                _peopleList = new List<People>();
            }
            public PeopleCollection(List<People> people)
            {
                _peopleList =people;

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


            public People Current => _peopleList[position];

            object IEnumerator.Current => _peopleList[position];



            public void Add(People person)
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



            public People Get(int id)//check in on this
            {
                return _peopleList.SingleOrDefault(p => p.Id == id);
            }



            public IEnumerator<People> GetEnumerator()
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
            public int Length => _peopleList.Count;

            //object IEnumerator.Current => throw new NotImplementedException();

            public People this[int index]
            {
                get => _peopleList[index];
                set => _peopleList[index] = value;
            }
            IEnumerator<People> IEnumerable<People>.GetEnumerator()
            {
                return _peopleList.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _peopleList.GetEnumerator();
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

    }


