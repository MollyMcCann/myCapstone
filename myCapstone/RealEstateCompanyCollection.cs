﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTrackerDatamodelLibrary;

namespace myCapstone
{
   public class RealEstateCompanyCollection
            : IEnumerator<RealEstateCompany>, IEnumerable<RealEstateCompany>
    {
        private List<RealEstateCompany> _realEstateCompanies;
        int position = -1;
        public RealEstateCompanyCollection()
        {
            _realEstateCompanies = new List<RealEstateCompany>();
        }
        public RealEstateCompanyCollection(List<RealEstateCompany> realEstateCompanies)
        {
            _realEstateCompanies = realEstateCompanies;
            if (_realEstateCompanies.Count > -1)
            {
                position = -1;
            }
        }
        public RealEstateCompany Current => _realEstateCompanies[position];

        object IEnumerator.Current => _realEstateCompanies[position];
        public void Add(RealEstateCompany realEstateCompany)
        {
            var re = _realEstateCompanies.SingleOrDefault(r => r.CompanyID == realEstateCompany.CompanyID);
            if (re == null)
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

        public bool MoveNext()
        {
            position++;
            return (position < _realEstateCompanies.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        public RealEstateCompany Get(int id)
        {
            return _realEstateCompanies.SingleOrDefault(re => re.CompanyID == id);
        }

        public IEnumerator<RealEstateCompany> GetEnumerator()
        {
            return ((IEnumerable<RealEstateCompany>)_realEstateCompanies).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<RealEstateCompany>)_realEstateCompanies).GetEnumerator();
        }
    }
}
