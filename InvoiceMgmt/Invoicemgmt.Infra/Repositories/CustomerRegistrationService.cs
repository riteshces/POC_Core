using Invoicemgmt.Core.Interfaces;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;
using Invoicemgmt.Infra.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MongoDB.Driver;

namespace Invoicemgmt.Infra.Repositories
{
    public class CustomerRegistrationService : ICustomerService
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRegistrationService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CustomerRegistration> AddCustomer(CustomerRegistration customerRegistration)
        {
            await _appDbContext.CustomerRegistrations.InsertOneAsync(customerRegistration);
            return customerRegistration;
        }

        public async Task<List<CustomerRegistration>> GetCustomers(int pageNumber, int pageSize)
        {
            return await _appDbContext.CustomerRegistrations.Find(FilterDefinition<CustomerRegistration>.Empty).Skip(pageNumber * pageSize).Limit(pageSize).ToListAsync();
        }

        public async Task<CustomerRegistration> GetCustomerById(string customerId)
        {
            var filter = Builders<CustomerRegistration>.Filter.Eq(customer => customer.Id, customerId);
            var customerRegistration = await _appDbContext.CustomerRegistrations.FindAsync(filter).Result.FirstOrDefaultAsync();
            return customerRegistration;
        }

        public async Task<List<CustomerRegistration>> GetCustomersByContactNo(string contactNo)
        {
            var filter = Builders<CustomerRegistration>.Filter.Eq(customer => customer.ContactNo, contactNo);
            var customerRegistrations = await _appDbContext.CustomerRegistrations.FindAsync(filter).Result.ToListAsync();
            return customerRegistrations;
        }

        public void UpdateCustomer(string id, CustomerRegistrationUpdateRequest customerRegistrationUpdateRequest)
        {
            throw new NotImplementedException();
        }


    }
}
