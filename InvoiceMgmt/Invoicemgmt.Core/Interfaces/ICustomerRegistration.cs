using Invoicemgmt.Domain;

namespace Invoicemgmt.Core.Interfaces
{
    public interface ICustomerRegistration
    {
        void AddCustomer(CustomerRegistration customerRegistration);
        IEnumerable<CustomerRegistration> GetCustomersByContactNo(string contactNo);

    }
}
