using System;
using System.IO;
using NameSorterClasses;

namespace NameSorterDisplay
{
    class Program
    {
        const string defaultUnsortedFileName = "unsorted-names-list.txt";
        const string defaultSortedFileName = "sorted-names-list.txt";

        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "?")
            {
                Console.WriteLine("name-sorter {unsorted file name} {sorted file name}");
                return;
            }

            if (args.Length > 2)
            {
                Console.WriteLine("Too many argments. There is a maximum of two.");
                return;
            }

            var unsortedFileName = defaultUnsortedFileName;
            var sortedFileName = defaultSortedFileName;

            if (args.Length == 2)
            {
                sortedFileName = args[1];
            }

            if (args.Length > 0)
            {
                unsortedFileName = args[0];
            }

            var names = new Names();

            try
            {
                NameSorterFile.ReadFile(unsortedFileName, names);
            }
            catch (NameSorterFileException ex)
            {
                Console.WriteLine($"Unsorted file has an error. {ex.Message}");
                return;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unsorted file wasnt found");
                return;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not found in the unsorted file name");
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Unexpected Error reading the unsorted file");
                return;
            }

            try
            {
                names.Sort();
            }
            catch (Exception)
            {
                Console.WriteLine("Error sorting the unsorted file contents");
                return;
            }

            Console.WriteLine("Sorted Names");
            foreach (var fullName in names.SortedNameList)
            {
                Console.WriteLine(fullName.ToString());
            }
            Console.WriteLine();

            try
            {
                NameSorterFile.WriteFile(sortedFileName, names);
                Console.WriteLine("Sorted file written");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not found in the sorted file name");
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Unexpected Error writing the sorted file");
                return;
            }
        }
    }
}
