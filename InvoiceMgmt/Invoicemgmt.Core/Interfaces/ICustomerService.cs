using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;

namespace Invoicemgmt.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerRegistration> AddCustomer(CustomerRegistration customerRegistration);
        Task<CustomerRegistration> GetCustomerById(string customerId);
        Task<List<CustomerRegistration>> GetCustomers(int pageNumber, int pageSize);
        Task<List<CustomerRegistration>> GetCustomersByContactNo(string contactNo);

        void UpdateCustomer(string id, CustomerRegistrationUpdateRequest customerRegistrationUpdateRequest);
    }
}
