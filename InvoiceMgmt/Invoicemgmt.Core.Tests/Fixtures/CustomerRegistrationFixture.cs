using Castle.Core.Resource;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;
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


        public static CustomerRegistration GetCustomerRegistration()
        {
            return new CustomerRegistration
            {
                Id="1",
                FullName = "Test",
                ContactNo = "1234567890",
                AltContactNo = "1234567890",
                Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
            };
        }


        public static List<CustomerRegistration> GetCustomersList() => new()
        {
           new CustomerRegistration
            {
                   Id="1",
                FullName = "Test",
                ContactNo = "1234567890",
                AltContactNo = "1234567890",
                Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
            },
           new CustomerRegistration
            {
                   Id="2",
                FullName = "Test",
                ContactNo = "1234567890",
                AltContactNo = "1234567890",
                Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
            },
           new CustomerRegistration
            {
                   Id="3",
                FullName = "Test",
                ContactNo = "1234567890",
                AltContactNo = "1234567890",
                Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
            }
           };

        public static CustomerRegistrationUpdateRequest GetCustomerUpdateData()
        {
            return new CustomerRegistrationUpdateRequest
            {
                Id = "664f28655109e7d5f39ddc5d",
                FullName = "Test",
                ContactNo = "1234567890",
                AltContactNo = "1234567890",
                Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
            };
        }
    }
}
