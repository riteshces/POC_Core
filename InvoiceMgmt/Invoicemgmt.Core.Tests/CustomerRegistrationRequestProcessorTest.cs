using FluentAssertions;
using Invoicemgmt.Core.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicemgmt.Core
{
    public class CustomerRegistrationRequestProcessorTest
    {
        [Fact]
        public void Should_Return_Customer_Registration_Response_With_Request_Values()
        {
            //Arrange
            var request = CustomerRegistrationFixture.GetCustomerData();
            var processor = new CustomerRegistrationProcessor();

            //Act

            CustomerRegistrationResponse response = processor.AddCustomer(request);


            //Assert
            response.Should().NotBeNull();

            response.Address.Equals(request.Address);
            response.FullName.Equals(request.FullName);
            response.AltContactNo.Equals(request.AltContactNo);
            response.Address.City.Equals(request.Address.City);
            response.ContactNo.Equals(request.ContactNo);
            response.Address.Country.Equals(request.Address.Country);
            response.Address.Pincode.Equals(request.Address.Pincode);
        }
    }
}
