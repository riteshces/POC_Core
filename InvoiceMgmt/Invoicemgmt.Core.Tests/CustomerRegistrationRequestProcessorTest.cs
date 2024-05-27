using AutoFixture.Xunit2;
using FluentAssertions;
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
        private readonly Mock<ICustomerService> _mock;
        private readonly List<CustomerRegistration> _customerRegistrations;

        public CustomerRegistrationRequestProcessorTest()
        {
            //Arrange

            //Get customer sample data from fixures
            _request = CustomerRegistrationFixture.GetCustomerData();

            //Initialize the customer list
            _customerRegistrations = new List<CustomerRegistration>() { new CustomerRegistration { ContactNo = "1234567890" } };

            //generating mock request from Customer Service Interface
            _mock = new Mock<ICustomerService>();


            //process
            _processor = new CustomerRegistrationRepository(_mock.Object);
        }

        #region Save Customer
        [Fact]
        public async void Should_Return_Customer_Registration_Response_With_Request_Values()
        {
            //Arrange
            _mock.Setup(customer => customer.GetCustomersByContactNo(_request.ContactNo)).Returns(Task.FromResult(new List<CustomerRegistration>()));

            //Act
            CustomerRegistrationResponse response = await _processor.AddCustomer(_request);

            //Assert
            _mock.Verify(q => q.AddCustomer(It.IsAny<CustomerRegistration>()), Times.Once);
            response.Should().NotBeNull();
        }

        [Fact]
        public void Should_Throw_Null_Exception_For_Null_Request()
        {
            //Act
            Func<Task> result = async () => await _processor.AddCustomer(null);

            //Assert
            result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void Should_Save_Customer_Registration_Request()
        {
            //Arrange
            CustomerRegistration savedCustomerRegistration = null;

            _mock.Setup(customer => customer.GetCustomersByContactNo(_request.ContactNo)).Returns(Task.FromResult(new List<CustomerRegistration>()));
            _mock.Setup(customer => customer.AddCustomer(It.IsAny<CustomerRegistration>()))
                .Callback<CustomerRegistration>(customer =>
                {
                    savedCustomerRegistration = customer;
                });

            //Act
            _processor.AddCustomer(_request);

            //Assert
            _mock.Verify(q => q.AddCustomer(It.IsAny<CustomerRegistration>()), Times.Once);
            savedCustomerRegistration.Should().NotBeNull();
            savedCustomerRegistration.FullName.Should().BeEquivalentTo(_request.FullName);
        }

        [Fact]
        public void Should_Not_Save_Customer_Registration_Request_If_Customer_Exist_With_Same_Contact_No()
        {
            //Arrange
            _mock.Setup(customer => customer.GetCustomersByContactNo(_request.ContactNo)).Returns(Task.FromResult(_customerRegistrations));

            //Act
            Func<Task> result = async () => await _processor.AddCustomer(_request);

            //Assert
            _mock.Verify(q => q.AddCustomer(It.IsAny<CustomerRegistration>()), Times.Never);
            result.Should().ThrowAsync<Exception>();
        }

        #endregion

        #region Update Customer

        [Fact]
        public void OnUpdate_Should_Return_Request_Same_As_Response()
        {
            //Arrange
            CustomerRegistrationUpdateRequest customerRegistrationUpdateRequest = CustomerRegistrationFixture.GetCustomerUpdateData();

            //Act
            //CustomerRegistrationResponse customerRegistrationResponse = _processor.UpdateCustomer(customerRegistrationUpdateRequest.Id, customerRegistrationUpdateRequest);

            CustomerRegistrationResponse customerRegistrationResponse = null;

            //Assert

            customerRegistrationResponse.Should().NotBeNull();
            customerRegistrationResponse.Id.Should().Be(customerRegistrationUpdateRequest.Id);
        }

        #endregion

        #region Get Customers List

        [Theory]
        [AutoData]
        public async void OnSuccess_Should_Return_If_Found_CustomerList(int pageNumber, int pageSize)
        {
            //Arrange
            List<CustomerRegistration> customerRegistrations = CustomerRegistrationFixture.GetCustomersList();
            _mock.Setup(q => q.GetCustomers(pageNumber, pageSize)).Returns(Task.FromResult(customerRegistrations));

            //Act
            List<CustomerRegistration> customers = await _processor.GetCustomers(pageNumber, pageSize);

            //Assert
            customers.Should().NotBeNull();
            customers.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async void OnFailure_Should_Return_Blank_CustomerList_When_Customers_Not_Exists()
        {
            //Arrange
            int pageNumber = 1, pageSize = 10;
            List<CustomerRegistration> customerRegistrations = new List<CustomerRegistration>();
            _mock.Setup(q => q.GetCustomers(pageNumber, pageSize)).Returns(Task.FromResult(customerRegistrations));

            //Act
            List<CustomerRegistration> customers = await _processor.GetCustomers(pageNumber, pageSize);

            //Assert
            customers.Should().NotBeNull();
            customers.Count().Should().Be(0);
        }

        #endregion

        #region Get Customer By Id

        [Fact]
        public async void OnSuccess_Should_Return_Customer()
        {
            //Arrange

            CustomerRegistration customerRegistrations = CustomerRegistrationFixture.GetCustomerRegistration();
            var customerid = customerRegistrations.Id;
            _mock.Setup(q => q.GetCustomerById(customerid)).Returns(Task.FromResult(customerRegistrations));

            //Act
            CustomerRegistration customer = await _processor.GetCustomerById(customerid);

            //Assert
            customer.Should().NotBeNull();
            customer.Id.Should().BeEquivalentTo(customerid);
        }

        [Fact]
        public async void OnFailure_Should_Return_Blank_Customer_When_Customers_Not_Found()
        {
            //Arrange
            var customerid = "3";
            CustomerRegistration customerRegistrations = CustomerRegistrationFixture.GetCustomerRegistration();
            _mock.Setup(q => q.GetCustomerById(customerid)).Returns(Task.FromResult(customerRegistrations));

            //Act
            CustomerRegistration customer = await _processor.GetCustomerById(customerid);

            //Assert
            customer.Should().NotBeNull();
        }

        #endregion
    }
}
