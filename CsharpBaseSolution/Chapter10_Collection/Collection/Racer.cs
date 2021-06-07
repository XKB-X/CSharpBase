using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10_Collection.Collection
{
    public class Racer : IComparable<Racer>, IFormattable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Wins { get; set; }
        public Racer(int id, string firstName, string lastName, string country,int wins) {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.Wins = wins;
        }
        public Racer(int id, string firstName, string lastName, string country) : this(id, firstName, lastName, country,wins:0) { }
        public int CompareTo(Racer other)
        {
            if(other==null)return -1;
            int compare = string.Compare(this.LastName, other.LastName);
            if(compare==0)
                return string.Compare(this.FirstName, other.FirstName);
            return compare;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "N";
            switch (format.ToUpper())
            {
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "W":
                    return string.Format("{0} Wins:{1}",ToString(),Wins);
                case "C":
                    return string.Format("{0} Country:{1}",ToString(),Country);
                case "ALL":
                    return string.Format("{0} Wins:{1} Country:{2}",ToString(),Wins,Country);
                default:
                    throw new FormatException(string.Format(formatProvider, "Format {0} is not support", format));
            }
        }
        public override string ToString()
        {
            return string.Format("{0} {1}",FirstName,LastName);
        }
        public string ToString(string format)
        {
            return ToString(format, null);
        }
    }
}
