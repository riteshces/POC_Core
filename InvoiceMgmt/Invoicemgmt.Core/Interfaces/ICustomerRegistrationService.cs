using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;

namespace Invoicemgmt.Core.Interfaces
{
    public interface ICustomerRegistrationService
    {
        void AddCustomer(CustomerRegistration customerRegistration);
        CustomerRegistration GetCustomerById(string customerId);
        List<CustomerRegistration> GetCustomers();
        IEnumerable<CustomerRegistration> GetCustomersByContactNo(string contactNo);
        void UpdateCustomer(string id, CustomerRegistrationUpdateRequest customerRegistrationUpdateRequest);
    }
}
