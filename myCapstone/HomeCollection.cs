using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;

namespace myCapstone
{
    class HomeCollection
        : IEnumerator<Home>, IEnumerable<Home>
    {
        private List<Home> _homes;
        int position = -1;
    
        public HomeCollection()
        {
            _homes = new List<Home>();
        }

        public HomeCollection(List<Home> homes)
        {
            _homes = homes;
            if (_homes.Count > 0 )
            {
                position = 0;
            }
        }
        public void Clear()
        {
            _homes.Clear();
        }

        public Home Current => _homes[position];

        object IEnumerator.Current => _homes[position];

        public void Add(Home home)
        {
            
            var re = _homes.SingleOrDefault(h => h.HomeID == home.HomeID);
            if (re == null)
            {
                _homes.Add(home);
            }
        }
      
        public void Dispose()
        {
         //   throw new NotImplementedException();
        }

        public IEnumerator<Home> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _homes.Count);
        }
        public void Reset()
        {
            position = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Home>)_homes).GetEnumerator();
        }
    }
}
