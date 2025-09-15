using System.IO;
using System.Linq;

namespace NameSorterClasses
{
    public static class NameSorterFile
    {
        public static void ReadFile(string filename, IBuildNames buildNames)
        {
            var lineNumber = 0;
            foreach (string line in File.ReadLines(filename))
            {
                lineNumber++;
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var nameParts = line.Split(' ');
                if (nameParts.Count() > FullName.MaximumNumberOfGivenNames + 1)
                {
                    throw new NameSorterFileException($"Too many Given Names in line number {lineNumber}, value {line}");
                } 
                
                if (nameParts.Count() < 2)
                {
                    throw new NameSorterFileException($"Not Enough Given Names in line number {lineNumber}, value {line}");
                }

                if (nameParts.Where(n => string.IsNullOrEmpty(n)).Count() > 0)
                {
                    throw new NameSorterFileException($"Multiple spaces together in Given Names in line number {lineNumber}, value {line}");
                }

                var lastName = nameParts.Last();
                var givenNames = new string[FullName.MaximumNumberOfGivenNames];
                for (int i = 0; i < nameParts.Length - 1; i++)
                {
                    givenNames[i] = nameParts[i];
                }

                buildNames.Add(givenNames, lastName);
            }
        }

        public static void WriteFile(string filename, ISortedNames sortedNames)
        {
            File.WriteAllLines(filename, sortedNames.SortedNameList.Select(n => n.ToString()));
        }
    }
}
