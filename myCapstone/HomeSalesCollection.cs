using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;

namespace myCapstone
{
   public class HomeSalesCollection
        : IEnumerator<HomeSale>, IEnumerable<HomeSale>
    {
        private List<HomeSale> _homeSales;
        int position = -1;
        public HomeSalesCollection()
        {
            _homeSales = new List<HomeSale>();
          
        }
        public HomeSalesCollection(List<HomeSale> homeSales)
        {
            _homeSales = homeSales;
            if (_homeSales.Count > 0)
            {
                position = 0;
            }
        }
        public HomeSale Current => _homeSales[position];

        object IEnumerator.Current => _homeSales[position];

        public void Add(HomeSale homeSale)
        {
            homeSale.SaleID = getLastId() + 1;

            var homeS = _homeSales.SingleOrDefault(hs => hs.SaleID == homeSale.SaleID);
            if (homeS == null)
            {
                _homeSales.Add(homeSale);
                using (HomeTrackerModel1 db = new HomeTrackerModel1())
                {
                    try
                    {
                        db.HomeSales.Add(homeSale);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        //Todo: notify user
                        int i = 0;
                    }
                }
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

        public IEnumerator<HomeSale> GetEnumerator()
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
            return ((IEnumerable<HomeSale>)_homeSales).GetEnumerator();
        }

        private int getLastId()
        {
            int ret = -1;
            using (HomeTrackerModel1 db = new HomeTrackerModel1())
            {
                ret = db.HomeSales.Max(p => p.SaleID);
            }

            return ret;
        }


    }
}
