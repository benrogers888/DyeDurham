using System;
using System.Collections.Generic;
using System.Linq;

namespace NameSorterClasses
{
    public class Names : IBuildNames, ISortedNames
    {  
        public Names()
        {
            NameList = new List<FullName>();
        }

        public void Add(string[] givenNames, string lastName)
        {
            if (givenNames.Length > FullName.MaximumNumberOfGivenNames)
            {
                throw new ArgumentException("Given Names argument array size is too large");
            }

            if (givenNames.Length == 0)
            {
                throw new ArgumentException("Given Names argument array is empty");
            }

            var fullName = new FullName();
            for (int i = 0; i < FullName.MaximumNumberOfGivenNames; i++)
            {
                if (i < givenNames.Length)
                {
                    fullName.GivenNames[i] = givenNames[i];
                }
            }
            fullName.LastName = lastName;
            NameList.Add(fullName);
        }

        public void Sort()
        {
            SortedNameList = NameList.OrderBy(n => n.SortKey()).ToList();
        }

        public List<FullName> NameList { get; private set; }
        public List<FullName> SortedNameList { get; private set; }
    }
}
