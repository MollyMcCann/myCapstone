using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;

namespace HomeTrackerTest
{
    class RealEstateCompanyCollection
            : IEnumerator, IEnumerable
    {
        private List<RealEstateCompany> _realEstateCompanies;
        int position = -1;
        public RealEstateCompanyCollection()
        {
            _realEstateCompanies = new List<RealEstateCompany>();
        }

        public RealEstateCompany Current => _realEstateCompanies[position];

        object IEnumerator.Current => _realEstateCompanies[position];


       

        public void Add(RealEstateCompany realEstateCompany)
        {
            var re = _realEstateCompanies.SingleOrDefault(r => r.CompanyID == realEstateCompany.CompanyID);
            if (re == null)//the r here might give us trouble later
            {
                _realEstateCompanies.Add(realEstateCompany);
            }
        }
        public int Count
        {
            get { return _realEstateCompanies.Count; }
        }

        public void Clear()
        {
            _realEstateCompanies.Clear();
        }

        public void Sort()
        {
            _realEstateCompanies.Sort();
        }
        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _realEstateCompanies.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<RealEstateCompany>)this;
        }


        public RealEstateCompany Get(int id)
        {
            return _realEstateCompanies.SingleOrDefault(re => re.CompanyID == id);
        }

    }
}
