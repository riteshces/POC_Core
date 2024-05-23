using Invoicemgmt.Domain;
using Invoicemgmt.Domain.BaseModels;

namespace Invoicemgmt.Infra.Tests.Fixtures
{
    public static class CustomerRegistrationFixture
    {
        public static CustomerRegistration GetCustomerData()
        {
            return new CustomerRegistration
            {
                Id= "664f28655109e7d5f39ddc5d",
                FullName = "Test",
                ContactNo = "1234567890",
                AltContactNo = "1234567890",
                Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
            };
        }
    }
}
