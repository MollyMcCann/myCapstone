using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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
           
        }
        public HomeSale Current
        {
            get
            {
                return _homeSales[position];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return _homeSales[position];
            }
        }

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

        public void Update(HomeSale homeSale)
        {
            if (homeSale == null)
            {
                return;
            }

            using (HomeTrackerModel1 db = new HomeTrackerModel1())
            {
                try
                {
                    db.Entry(homeSale).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //Todo: notify user
                    int i = 0;
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
            return _homeSales.GetEnumerator();
        }

        public bool MoveNext()
        {
            position++;
            return (position < _homeSales.Count);
        }
        public void Reset()
        {
            position = 0;
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<HomeSale>)_homeSales).GetEnumerator();
        }
    }
}
