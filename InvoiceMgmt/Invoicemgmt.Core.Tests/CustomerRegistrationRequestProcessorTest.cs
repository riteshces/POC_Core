using FluentAssertions;
using Invoicemgmt.Core.Enums;
using Invoicemgmt.Core.Fixtures;
using Invoicemgmt.Core.Interfaces;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Core.Repository;
using Invoicemgmt.Domain;
using Moq;

namespace Invoicemgmt.Core
{
    public class CustomerRegistrationRequestProcessorTest
    {
        private readonly CustomerRegistrationRepository _processor;
        private readonly CustomerRegistrationRequest _request;
        private readonly Mock<ICustomerRegistration> _mock;
        private readonly List<CustomerRegistration> _customerRegistrations;

        public CustomerRegistrationRequestProcessorTest()
        {
            //Arrange

            //Get customer sample data from fixures
            _request = CustomerRegistrationFixture.GetCustomerData();

            //Initialize the customer list
            _customerRegistrations = new List<CustomerRegistration>() { new CustomerRegistration { ContactNo= "1234567890" } };

            //generating mock request from Customer Service Interface
            _mock = new Mock<ICustomerRegistration>();


            //process
            _processor = new CustomerRegistrationRepository(_mock.Object);
        }

        [Fact]
        public void Should_Return_Customer_Registration_Response_With_Request_Values()
        {
            //Act
            CustomerRegistrationResponse response = _processor.AddCustomer(_request);

            //Assert
            response.Should().NotBeNull();

            response.Address.Equals(_request.Address);
            response.FullName.Equals(_request.FullName);
            response.AltContactNo.Equals(_request.AltContactNo);
            response.Address.City.Equals(_request.Address.City);
            response.ContactNo.Equals(_request.ContactNo);
            response.Address.Country.Equals(_request.Address.Country);
            response.Address.Pincode.Equals(_request.Address.Pincode);
        }

        [Fact]
        public void Should_Throw_Null_Exception_For_Null_Request()
        {
            //Act
            Action result = () => _processor.AddCustomer(null);

            //Assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_Save_Customer_Registration_Request()
        {
            //Arrange
            CustomerRegistration savedCustomerRegistration = null;
            _mock.Setup(m => m.AddCustomer(It.IsAny<CustomerRegistration>()))
                .Callback<CustomerRegistration>(customer =>
                {
                    savedCustomerRegistration = customer;
                });

            //Act
            _processor.AddCustomer(_request);
            _mock.Verify(q => q.AddCustomer(It.IsAny<CustomerRegistration>()), Times.Once);

            //Assert
            savedCustomerRegistration.Should().NotBeNull();

            savedCustomerRegistration?.Address.Equals(_request.Address);
            savedCustomerRegistration?.FullName.Equals(_request.FullName);
            savedCustomerRegistration?.AltContactNo.Equals(_request.AltContactNo);
            savedCustomerRegistration?.Address.City.Equals(_request.Address.City);
            savedCustomerRegistration?.ContactNo.Equals(_request.ContactNo);
            savedCustomerRegistration?.Address.Country.Equals(_request.Address.Country);
            savedCustomerRegistration?.Address.Pincode.Equals(_request.Address.Pincode);
        }

        [Fact]
        public void Should_Not_Save_Customer_Registration_Request_If_Customer_Exist_With_Same_Contact_No()
        {
            //Arrange
            _mock.Setup(q => q.GetCustomersByContactNo(_request.ContactNo)).Returns(_customerRegistrations);

            //Act
            _processor.AddCustomer(_request);

            //Assert
            _mock.Verify(q => q.AddCustomer(It.IsAny<CustomerRegistration>()), Times.Never);
        }

        [Theory]
        [InlineData(ResultFlag.Failure,false)]
        [InlineData(ResultFlag.Success, true)]
        public void Should_Return_SuccessOrFailure_Flag_In_Result(ResultFlag resultFlag, bool isCustomerAvailable)
        {
            //Arrange
            _mock.Setup(q => q.GetCustomersByContactNo(_request.ContactNo)).Returns(_customerRegistrations);
            if (isCustomerAvailable)
            {
                _customerRegistrations.Clear();
            }

            //Act
            var result = _processor.AddCustomer(_request);

            //Assert
            resultFlag.Should().Be(result.ResultFlag);
        }
    }
}
