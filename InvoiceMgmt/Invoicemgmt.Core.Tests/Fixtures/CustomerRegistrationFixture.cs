using AutoFixture;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;
using Invoicemgmt.Domain.BaseModels;

namespace Invoicemgmt.Core.Fixtures
{
    public static class CustomerRegistrationFixture
    {
        public static CustomerRegistrationRequest GetCustomerData()
        {
            Fixture fixture = new Fixture();
            return fixture.Create<CustomerRegistrationRequest>();
        }


        public static CustomerRegistration GetCustomerRegistration()
        {
            Fixture fixture = new Fixture();
            return fixture.Create<CustomerRegistration>();
        }


        //public static List<CustomerRegistration> GetCustomersList() => new()
        //{
        //   new CustomerRegistration
        //    {
        //           Id="1",
        //        FullName = "Test",
        //        ContactNo = "1234567890",
        //        AltContactNo = "1234567890",
        //        Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
        //    },
        //   new CustomerRegistration
        //    {
        //           Id="2",
        //        FullName = "Test",
        //        ContactNo = "1234567890",
        //        AltContactNo = "1234567890",
        //        Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
        //    },
        //   new CustomerRegistration
        //    {
        //           Id="3",
        //        FullName = "Test",
        //        ContactNo = "1234567890",
        //        AltContactNo = "1234567890",
        //        Address = new CustomerAddress { Address = "testing", City = "test", Country = "test", Pincode = 123456 }
        //    }
        //   };


        public static List<CustomerRegistration> GetCustomersList()
        {
            Fixture fixture = new Fixture();
            return fixture.CreateMany<CustomerRegistration>(10).ToList();
        }
        public static CustomerRegistrationUpdateRequest GetCustomerUpdateData()
        {
            Fixture fixture = new Fixture();
            return fixture.Build<CustomerRegistrationUpdateRequest>().With(c => c.Id, "664f28655109e7d5f39ddc5d").Create();
        }
    }
}
