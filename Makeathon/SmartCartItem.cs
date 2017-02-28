using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makeathon
{
    public class SmartCartItem: IEquatable<SmartCartItem>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public string ExpiryDate { get; set; }

        public bool Equals(SmartCartItem other)
        {
            if (other == null)
                return false;
            else if (
                    string.Equals(this.Name, other.Name) &&
                    string.Equals(this.Location, other.Location) &&
                    string.Equals(this.ExpiryDate, other.ExpiryDate) &&
                    this.Price == other.Price)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.Location.GetHashCode() ^ this.ExpiryDate.GetHashCode() ^ this.Price.GetHashCode();
        }
    }
}
