using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicemgmt.Core.Fixtures
{
    public static class CustomerRegistrationFixture
    {
        public static CustomerRegistration GetCustomerData()
        {
            return new CustomerRegistration
            {
                FullName = "Test",
                ContactNo = "1234567890",
                AltContactNo = "1234567890",
                Address = "Test",
                Pincode = "123456",
                City = "Test",
                Country = "Test"
            };
        }
    }
}
