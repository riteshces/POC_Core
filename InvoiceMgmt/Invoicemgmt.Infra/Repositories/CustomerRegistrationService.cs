using Invoicemgmt.Core.Interfaces;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;
using Invoicemgmt.Infra.Data;
using MongoDB.Driver;

namespace Invoicemgmt.Infra.Repositories
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRegistrationService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void AddCustomer(CustomerRegistration customerRegistration)
        {
            _appDbContext.CustomerRegistrations.InsertOne(customerRegistration);
        }

        public List<CustomerRegistration> GetCustomers()
        {
            return _appDbContext.CustomerRegistrations.Find(FilterDefinition<CustomerRegistration>.Empty).ToList();
        }

        public CustomerRegistration GetCustomerById(string customerId)
        {
            var filter = Builders<CustomerRegistration>.Filter.Eq(q => q.Id, customerId);
            var customerRegistration = _appDbContext.CustomerRegistrations.Find(filter).FirstOrDefault();
            return customerRegistration;
        }

        public IEnumerable<CustomerRegistration> GetCustomersByContactNo(string contactNo)
        {
            var filter = Builders<CustomerRegistration>.Filter.Eq(q=>q.ContactNo,contactNo);
            var customerRegistrations = _appDbContext.CustomerRegistrations.Find(filter).ToList();
            return customerRegistrations;

        }

        public void UpdateCustomer(string id, CustomerRegistrationUpdateRequest customerRegistrationUpdateRequest)
        {
            throw new NotImplementedException();
        }

        
    }
}
