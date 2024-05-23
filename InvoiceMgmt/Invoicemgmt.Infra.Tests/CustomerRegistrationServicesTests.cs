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
        public void Should_Return_Available_Customers_From_Contact_No()
        {
            //Arrange

            var contactNo = "1234567890";
            using var dbContext = new AppDbContext(_mockClient.Object);
            dbContext.CustomerRegistrations.DeleteMany(FilterDefinition<CustomerRegistration>.Empty);
            dbContext.CustomerRegistrations.InsertOne(CustomerRegistrationFixture.GetCustomerData());
            CustomerRegistrationService customerRegistrationService = new CustomerRegistrationService(dbContext);

            //Act

            var customer = customerRegistrationService.GetCustomersByContactNo(contactNo);


            //Assert
            customer.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        public void Should_Save_Customer_Registration()
        {
            //Arrange

            using var dbContext = new AppDbContext(_mockClient.Object);
            dbContext.CustomerRegistrations.DeleteMany(FilterDefinition<CustomerRegistration>.Empty);
            CustomerRegistrationService customerRegistrationService = new CustomerRegistrationService(dbContext);

            //Act

            customerRegistrationService.AddCustomer(CustomerRegistrationFixture.GetCustomerData());
            var customers = dbContext.CustomerRegistrations.Find(FilterDefinition<CustomerRegistration>.Empty).ToList();
            var customer = customers.Single();

            //Assert

            customers.Should().ContainSingle();
            customer.FullName.Should().BeEquivalentTo(CustomerRegistrationFixture.GetCustomerData().FullName);
            customer.FullName.Should().BeEquivalentTo(CustomerRegistrationFixture.GetCustomerData().FullName);
        }

        [Fact]
        public void Should_Return_List_of_Customers()
        {
            //Arrange

            using var dbContext = new AppDbContext(_mockClient.Object);
            dbContext.CustomerRegistrations.DeleteMany(FilterDefinition<CustomerRegistration>.Empty);
            dbContext.CustomerRegistrations.InsertOne(CustomerRegistrationFixture.GetCustomerData());
            CustomerRegistrationService customerRegistrationService = new CustomerRegistrationService(dbContext);

            //Act

            var customers = customerRegistrationService.GetCustomers();

            //Assert
            customers.Should().NotBeNull();
            customers.Should().BeOfType<List<CustomerRegistration>>();
        }

        [Fact]
        public void Should_Return_Customer_By_Id()
        {
            //Arrange

            using var dbContext = new AppDbContext(_mockClient.Object);
            dbContext.CustomerRegistrations.DeleteMany(FilterDefinition<CustomerRegistration>.Empty);
            dbContext.CustomerRegistrations.InsertOne(CustomerRegistrationFixture.GetCustomerData());
            CustomerRegistrationService customerRegistrationService = new CustomerRegistrationService(dbContext);

            //Act

            var customer = customerRegistrationService.GetCustomerById("664f28655109e7d5f39ddc5d");

            //Assert
            customer.Should().NotBeNull();
            customer.Id.Should().BeEquivalentTo(CustomerRegistrationFixture.GetCustomerData().Id);
        }
    }
}
