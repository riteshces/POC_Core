using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicemgmt.Domain.BaseModels
{
    public class CustomerAddress
    {
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
