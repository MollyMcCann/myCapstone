using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;

namespace HomeTrackerTest
{
    class HomeSalesCollection
        : IEnumerator, IEnumerable
    {
        private List<HomeSale> _homeSales;
        int position = -1;
        public HomeSalesCollection()
        {
            _homeSales = new List<HomeSale>();
           // using (HomeTrackerModel1 db = new HomeTrackerModel1())
           // {
           //     //retrieve data:
           //     HomeSale homeSales = (from hs in db.HomeSales
           //                  select hs).FirstOrDefault();
           //     Add(homeSales);
           // }
        }     

        public HomeSale Current => _homeSales[position];

        object IEnumerator.Current => _homeSales[position];

        public void Add(HomeSale homeSales)
        {

            var re = _homeSales.SingleOrDefault(hs => hs.HomeID == homeSales.HomeID);
            if (re == null)
            {
                _homeSales.Add(homeSales);
            }
        }
        public int Count
        {
            get { return _homeSales.Count; }
        }

        public void Clear()
        {
            _homeSales.Clear();
        }

        public void Sort()
        {
            _homeSales.Sort();
        }

        public void Dispose()
        {
            //   throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _homeSales.Count);
        }
        public void Reset()
        {
            position = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<HomeSale>)this;
        }

       
    }
}
