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
            Assert.NotNull(response);
            Assert.Equal(request.FullName, response.FullName);
            Assert.Equal(request.Address, response.Address);
            Assert.Equal(request.AltContactNo, response.AltContactNo);
            Assert.Equal(request.City, response.City);
            Assert.Equal(request.ContactNo, response.ContactNo);
            Assert.Equal(request.Country, response.Country);
            Assert.Equal(request.Pincode, response.Pincode);
        }
    }
}
