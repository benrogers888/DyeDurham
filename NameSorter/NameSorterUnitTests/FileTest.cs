using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameSorterClasses;

namespace NameSorterUnitTests
{
    [TestClass]
    public class FileTest
    {
        const string unsortedNameListFileName = "testUnsortedNameList.txt";
        const string sortedNameListFileName = "testSortedNameList.txt";

        [TestMethod]
        public void EmptyFile()
        {
            var allLines = new string[] { };
            File.WriteAllLines(unsortedNameListFileName, allLines);
            try
            {
                var names = new Names();
                NameSorterFile.ReadFile(unsortedNameListFileName, names);

                Assert.AreEqual(0, names.NameList.Count);
            }
            finally
            {
                File.Delete(unsortedNameListFileName);
            }
        }



        [TestMethod]
        [ExpectedException(typeof(NameSorterFileException))]
        public void NotEnoughGivenNames()
        {
            var allLines = new string[] { "Smith" };
            File.WriteAllLines(unsortedNameListFileName, allLines);

            try
            {
                var names = new Names();
                NameSorterFile.ReadFile(unsortedNameListFileName, names);
            }
            finally
            {
                File.Delete(unsortedNameListFileName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NameSorterFileException))]
        public void TooManyGivenNames()
        {
            var allLines = new string[] { "John Fred Samuel Alexander Smith" };
            File.WriteAllLines(unsortedNameListFileName, allLines);

            try
            {
                var names = new Names();
                NameSorterFile.ReadFile(unsortedNameListFileName, names);
            }
            finally
            {
                File.Delete(unsortedNameListFileName);
            }            
        }

        [TestMethod]
        [ExpectedException(typeof(NameSorterFileException))]
        public void MultipleSpacesTogetherInGivenName()
        {
            var allLines = new string[] { "John  Smith" };

            File.WriteAllLines(unsortedNameListFileName, allLines);

            try
            {
                var names = new Names();
                NameSorterFile.ReadFile(unsortedNameListFileName, names);
            }
            finally
            {
                File.Delete(unsortedNameListFileName);
            }
        }

        [TestMethod]
        public void StripOutEmptyLines()
        {
            var allLines = new string[] { string.Empty, "John Smith", string.Empty, string.Empty };
            File.WriteAllLines(unsortedNameListFileName, allLines);
            try
            {
                var names = new Names();
                NameSorterFile.ReadFile(unsortedNameListFileName, names);

                Assert.AreEqual(1, names.NameList.Count);
                Assert.AreEqual("John Smith", names.NameList.First().ToString());
            }
            finally
            {
                File.Delete(unsortedNameListFileName);
            }
        }

        [TestMethod]
        public void MultipleNames()
        {
            var allLines = new string[] { "Fred Smith", "John Smith", "Sam Brown", "Harry Rogers" };
            File.WriteAllLines(unsortedNameListFileName, allLines);
            try
            {
                var names = new Names();
                NameSorterFile.ReadFile(unsortedNameListFileName, names);

                Assert.AreEqual(4, names.NameList.Count);
                Assert.AreEqual("Fred Smith", names.NameList[0].ToString());
                Assert.AreEqual("John Smith", names.NameList[1].ToString());
                Assert.AreEqual("Sam Brown", names.NameList[2].ToString());
                Assert.AreEqual("Harry Rogers", names.NameList[3].ToString());
            }
            finally
            {
                File.Delete(unsortedNameListFileName);
            }
        }

        [TestMethod]
        public void WriteEmptyFile()
        {
            var names = new NamesForWriteTest();
            NameSorterFile.WriteFile(sortedNameListFileName, names);
            try
            {
                var content = File.ReadAllLines(sortedNameListFileName);
                Assert.AreEqual(0, content.Length);
            }
            finally
            {
                File.Delete(sortedNameListFileName);
            }
        }

        [TestMethod]
        public void WriteFileWithContents()
        {
            var names = new NamesForWriteTest();
            names.SortedNameList.Add(new FullName() { GivenNames = new string[] { "Sam" }, LastName = "Brown" });
            names.SortedNameList.Add(new FullName() { GivenNames = new string[] { "John" }, LastName = "Smith" });

            NameSorterFile.WriteFile(sortedNameListFileName, names);
            try
            {
                var content = File.ReadAllLines(sortedNameListFileName);
                Assert.AreEqual(2, content.Length);

                Assert.AreEqual("Sam Brown", content[0]);
                Assert.AreEqual("John Smith", content[1]);
            }
            finally
            {
                File.Delete(sortedNameListFileName);
            }
        }
    }

    public class NamesForWriteTest : ISortedNames
    {
        public NamesForWriteTest()
        {
            SortedNameList = new List<FullName>();
        }

        public List<FullName> SortedNameList { get; set; }
    }

}
