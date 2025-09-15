using System.Text;

namespace NameSorterClasses
{
    public class FullName
    {
        public const int MaximumNumberOfGivenNames = 3;

        public FullName()
        {
            GivenNames = new string[MaximumNumberOfGivenNames];
        }

        public string[] GivenNames { get; set; } 
        public string LastName { get; set; }

        public override string ToString()
        {
            var name = new StringBuilder(20);
            for (int i = 0; i < GivenNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(GivenNames[i]))
                {
                    name.Append(GivenNames[i]);
                    name.Append(" ");
                }
            }
            name.Append(LastName);
            return name.ToString();
        }

        public string SortKey()
        {
            var key = new StringBuilder(20);
            key.Append(LastName);
            for (int i = 0; i < GivenNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(GivenNames[i]))
                {
                    key.Append(" ");
                    key.Append(GivenNames[i]);
                }
            }
            return key.ToString();
        }
    }
}
