using AutoFixture;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;
using Invoicemgmt.Domain.BaseModels;
using MongoDB.Driver.Core.Misc;

namespace Invoicemgmt.Infra.Tests.Fixtures
{
    public static class CustomerRegistrationFixture
    {
        public static CustomerRegistration GetCustomerData()
        {
            Fixture fixture = new Fixture();
            return new CustomerRegistration
            {
                //Id = "664f28655109e7d5f39ddc5d",
                FullName = fixture.Create<string>(),
                ContactNo = fixture.Create<string>(),
                AltContactNo = fixture.Create<string>(),
                Address = new CustomerAddress { Address = fixture.Create<string>(), City = fixture.Create<string>(), Country = fixture.Create<string>(), Pincode = fixture.Create<int>() }
            };


           
            return fixture.Create<CustomerRegistration>();
        }
    }
}
