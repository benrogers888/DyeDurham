using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameSorterClasses;

namespace NameSorterUnitTests
{
    [TestClass]
    public class NamesTest
    {
        [TestMethod]
        public void EmptyNamesSort()
        {
            var expectedSortNames = new string[] { };

            var names = new Names();
            names.Sort();

            AssertSortedNames(expectedSortNames, names);
        }

        [TestMethod]
        public void SortByLastName()
        {
            var expectedSortedNames = new string[] { "Sam Brown", "Fred Scott", "John Smith" };

            var names = new Names();
            names.Add(new string[] { "John" }, "Smith");
            names.Add(new string[] { "Sam" }, "Brown");
            names.Add(new string[] { "Fred" }, "Scott");

            names.Sort();

            AssertSortedNames(expectedSortedNames, names);
        }

        [TestMethod]
        public void SortByGivenNames()
        {
            var expectedSortedNames = new string[] { "Fred Smith", "Fred Alexander Smith", "Fred Charles Smith", "John Smith", "Sam Smith" };

            var names = new Names();
            names.Add(new string[] { "John" }, "Smith");
            names.Add(new string[] { "Fred", "Charles" }, "Smith");
            names.Add(new string[] { "Sam" }, "Smith");
            names.Add(new string[] { "Fred" }, "Smith");
            names.Add(new string[] { "Fred", "Alexander" }, "Smith");

            names.Sort();

            AssertSortedNames(expectedSortedNames, names);
        }

        [TestMethod]
        public void SortByMultipleNames()
        {
            var expectedSortedNames = new string[] { "Sam Brown", "Sam Adam Brown", "Sam Adam James Brown", "Adam Browning", "Fred Scott" };

            var names = new Names();

            names.Add(new string[] { "Sam Adam" }, "Brown");
            names.Add(new string[] { "Sam" }, "Brown");
            names.Add(new string[] { "Adam" }, "Browning");
            names.Add(new string[] { "Fred" }, "Scott");
            names.Add(new string[] { "Sam Adam James" }, "Brown");

            names.Sort();

            AssertSortedNames(expectedSortedNames, names);
        }

        [TestMethod]
        public void SortByMultipleNamesWithDifferentSpacing()
        {
            var expectedSortedNames = new string[] { "Edmond Clark", "Fred Clark", "dmond Clarke", "mond Clarked", "mond James Clarked" };

            var names = new Names();

            names.Add(new string[] { "Fred" }, "Clark");
            names.Add(new string[] { "mond" }, "Clarked");
            names.Add(new string[] { "Edmond" }, "Clark");
            names.Add(new string[] { "dmond" }, "Clarke");
            names.Add(new string[] { "mond James" }, "Clarked");

            names.Sort();

            AssertSortedNames(expectedSortedNames, names);
        }

        void AssertSortedNames(string[] expectedSortedNames, ISortedNames sortedNames)
        {
            var actualSortedNames = sortedNames.SortedNameList.Select(n => n.ToString()).ToArray();

            Assert.AreEqual<int>(expectedSortedNames.Length, actualSortedNames.Length);
            for (int i = 0; i < expectedSortedNames.Length; i++)
            {
                Assert.AreEqual<string>(expectedSortedNames[i], actualSortedNames[i]);
            }
        }
    }
}
