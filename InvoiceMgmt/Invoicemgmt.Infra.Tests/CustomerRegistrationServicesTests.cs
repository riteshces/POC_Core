using AutoFixture.Xunit2;
using FluentAssertions;
using Invoicemgmt.Domain;
using Invoicemgmt.Infra.Data;
using Invoicemgmt.Infra.Repositories;
using Invoicemgmt.Infra.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Moq;

namespace Invoicemgmt.Infra.Tests
{
    public class CustomerRegistrationServicesTests
    {
        private readonly Mock<IMongoClient> _mockClient;
        public CustomerRegistrationServicesTests()
        {
            _mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public async void Should_Return_Available_Customers_From_Contact_No()
        {
            //Arrange

            var customerRequest = CustomerRegistrationFixture.GetCustomerData();
            using var dbContext = new AppDbContext(_mockClient.Object);
            //dbContext.CustomerRegistrations.DeleteMany(FilterDefinition<CustomerRegistration>.Empty);
            dbContext.CustomerRegistrations.InsertOne(customerRequest);
            CustomerRegistrationService customerRegistrationService = new CustomerRegistrationService(dbContext);

            //Act

            var customer = await customerRegistrationService.GetCustomersByContactNo(customerRequest.ContactNo);


            //Assert
            customer.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        public async void Should_Save_Customer_Registration()
        {
            //Arrange
            using var dbContext = new AppDbContext(_mockClient.Object);
            CustomerRegistrationService customerRegistrationService = new CustomerRegistrationService(dbContext);
            var customerData = CustomerRegistrationFixture.GetCustomerData();

            //Act
            var customer = await customerRegistrationService.AddCustomer(customerData);

            //Assert
            customer.Should().NotBeNull();
            customer.Id.Should().NotBeNullOrEmpty().And.HaveLength(24);
            customer.FullName.Should().BeEquivalentTo(customerData.FullName);
        }

        [Theory]
        [AutoData]
        public async void Should_Return_List_of_Customers(int pageNumber, int pageSize)
        {
            //Arrange

            using var dbContext = new AppDbContext(_mockClient.Object);
            dbContext.CustomerRegistrations.InsertOne(CustomerRegistrationFixture.GetCustomerData());
            CustomerRegistrationService customerRegistrationService = new CustomerRegistrationService(dbContext);

            //Act

            var customers = await customerRegistrationService.GetCustomers(pageNumber,pageSize);

            //Assert
            customers.Should().NotBeNull();
            customers.Should().BeOfType<List<CustomerRegistration>>();
        }

        [Fact]
        public async void Should_Return_Customer_By_Id()
        {
            //Arrange
            var customerData = CustomerRegistrationFixture.GetCustomerData();
            using var dbContext = new AppDbContext(_mockClient.Object);
            await dbContext.CustomerRegistrations.InsertOneAsync(customerData);
            CustomerRegistrationService customerRegistrationService = new CustomerRegistrationService(dbContext);

            //Act

            var customer = await customerRegistrationService.GetCustomerById(customerData.Id);

            //Assert
            customer.Should().NotBeNull();
            customer.Id.Should().BeEquivalentTo(customerData.Id);
        }
    }
}
