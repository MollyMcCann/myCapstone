using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;
using System.Data.Entity;
using System.Threading;

namespace myCapstone
{
   public class HomeCollection
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
            home.HomeID = getLastId() + 1;
            var h1 = _homes.SingleOrDefault(h => h.HomeID == home.HomeID);
            if (h1 == null)
            {
                _homes.Add(home);
                using (HomeTrackerModel1 db = new HomeTrackerModel1())
                {

                    try
                    {
                       
                        
                        home.OwnerID = home.Owner.OwnerID;
                       
                        
                        db.Homes.Add(home);
                        db.SaveChanges(); //tried to add home with out owner and had this exception thrown. Owner can't be an option?
                    }
                    catch (Exception ex)
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
                ret = db.Homes.Max(p => p.HomeID);
            }

            return ret;
        }

        public void Dispose()
        {
         //   throw new NotImplementedException();
        }

        public bool Remove(Home removeMe)
        {
            bool removed = false;
            if (_homes.Remove(removeMe))
            {
                removed = true;
                using (HomeTrackerModel1 db = new HomeTrackerModel1())
                {
                    var homeSalesToRemove = from hs in db.HomeSales
                                            where hs.HomeID == removeMe.HomeID
                                            select hs;
                    try
                    {
                        foreach (var hs in homeSalesToRemove)
                        {
                            db.Entry(hs).State = EntityState.Deleted;
                        }

                        db.Entry(removeMe).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        //TODO: notify user
                    }
                }
            }
           return removed;
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

        public void Sort()
        {
            _homes.Sort();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Home>)_homes).GetEnumerator();
        }

        public IEnumerator<Home> GetEnumerator()
        {
            return ((IEnumerable<Home>)_homes).GetEnumerator();
        }
    }
}
