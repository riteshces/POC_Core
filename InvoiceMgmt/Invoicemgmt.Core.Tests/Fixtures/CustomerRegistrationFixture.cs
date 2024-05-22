
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain.BaseModels;

namespace Invoicemgmt.Core.Fixtures
{
    public static class CustomerRegistrationFixture
    {
        public static CustomerRegistrationRequest GetCustomerData()
        {
            return new CustomerRegistrationRequest
            {
                FullName = "Test",
                ContactNo = "1234567890",
                AltContactNo = "1234567890",
                Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
            };
        }
    }
}
