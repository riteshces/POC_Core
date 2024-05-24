using FluentAssertions;
using Invoicemgmt.Core.Enums;
using Invoicemgmt.Core.Fixtures;
using Invoicemgmt.Core.Interfaces;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Core.Services;
using Invoicemgmt.Domain;
using Moq;

namespace Invoicemgmt.Core
{
    public class CustomerRegistrationRequestProcessorTest
    {
        private readonly CustomerRegistrationRepository _processor;
        private readonly CustomerRegistrationRequest _request;
        private readonly Mock<ICustomerRegistrationService> _mock;
        private readonly List<CustomerRegistration> _customerRegistrations;

        public CustomerRegistrationRequestProcessorTest()
        {
            //Arrange

            //Get customer sample data from fixures
            _request = CustomerRegistrationFixture.GetCustomerData();

            //Initialize the customer list
            _customerRegistrations = new List<CustomerRegistration>() { new CustomerRegistration { ContactNo = "1234567890" } };

            //generating mock request from Customer Service Interface
            _mock = new Mock<ICustomerRegistrationService>();


            //process
            _processor = new CustomerRegistrationRepository(_mock.Object);
        }

        #region Save Customer
        [Fact]
        public void Should_Return_Customer_Registration_Response_With_Request_Values()
        {
            //Act
            CustomerRegistrationResponse response = _processor.AddCustomer(_request);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(_request);
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
            savedCustomerRegistration.Should().BeEquivalentTo(_request);
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
        [InlineData(ResultFlag.Failure, false)]
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
        #endregion

        #region Update Customer

        [Fact]
        public void OnUpdate_Should_Return_Request_Same_As_Response()
        {
            //Arrange
            CustomerRegistrationUpdateRequest customerRegistrationUpdateRequest = CustomerRegistrationFixture.GetCustomerUpdateData();

            //Act
            CustomerRegistrationResponse customerRegistrationResponse = _processor.UpdateCustomer(customerRegistrationUpdateRequest.Id, customerRegistrationUpdateRequest);

            //Assert

            customerRegistrationResponse.Should().NotBeNull();
            customerRegistrationResponse.Id.Should().Be(customerRegistrationUpdateRequest.Id);
        }

        #endregion

        #region Get Customers List

        [Fact]
        public void OnSuccess_Should_Return_If_Found_CustomerList()
        {
            //Arrange
            List<CustomerRegistration> customerRegistrations = CustomerRegistrationFixture.GetCustomersList();
            _mock.Setup(q => q.GetCustomers()).Returns(customerRegistrations);

            //Act
            List<CustomerRegistration> customers = _processor.GetCustomers();

            //Assert
            customers.Should().NotBeNull();
            customers.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void OnFailure_Should_Return_Blank_CustomerList_When_Customers_Not_Exists()
        {
            //Arrange
            List<CustomerRegistration> customerRegistrations = new List<CustomerRegistration>();
            _mock.Setup(q => q.GetCustomers()).Returns(customerRegistrations);

            //Act
            List<CustomerRegistration> customers = _processor.GetCustomers();

            //Assert
            customers.Should().NotBeNull();
            customers.Count().Should().Be(0);
        }

        #endregion

        #region Get Customer By Id

        [Fact]
        public void OnSuccess_Should_Return_Customer()
        {
            //Arrange
           
            CustomerRegistration customerRegistrations = CustomerRegistrationFixture.GetCustomerRegistration();
            var customerid=customerRegistrations.Id;
            _mock.Setup(q => q.GetCustomerById(customerid)).Returns(customerRegistrations);

            //Act
            CustomerRegistration customer = _processor.GetCustomerById(customerid);

            //Assert
            customer.Should().NotBeNull();
            customer.Id.Should().BeEquivalentTo(customerid);
        }

        [Fact]
        public void OnFailure_Should_Return_Blank_Customer_When_Customers_Not_Found()
        {
            //Arrange
            var customerid = "3";
            CustomerRegistration customerRegistrations = CustomerRegistrationFixture.GetCustomerRegistration();
            _mock.Setup(q => q.GetCustomerById(customerid)).Returns(customerRegistrations);

            //Act
            CustomerRegistration customer = _processor.GetCustomerById(customerid);

            //Assert
            customer.Should().NotBeNull();
        }

        #endregion
    }
}
