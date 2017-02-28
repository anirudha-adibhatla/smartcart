using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makeathon
{
    public class SmartCartItem: IEquatable<SmartCartItem>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public string Location { get; set; }
        public string ExpiryDate { get; set; }

        public bool Equals(SmartCartItem other)
        {
            if (other == null)
                return false;
            else if (string.Equals(this.ID, other.ID))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
