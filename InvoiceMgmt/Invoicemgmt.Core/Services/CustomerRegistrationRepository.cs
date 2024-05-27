using Invoicemgmt.Core.Interfaces;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;
using Invoicemgmt.Domain.BaseModels;

namespace Invoicemgmt.Core.Services
{
    public class CustomerRegistrationRepository
    {
        private readonly ICustomerService _customerRegistration;

        public CustomerRegistrationRepository(ICustomerService customerRegistration)
        {
            _customerRegistration = customerRegistration;
        }

        public async Task<CustomerRegistrationResponse> AddCustomer(CustomerRegistrationRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException();
            }

            var customers = await _customerRegistration.GetCustomersByContactNo(request.ContactNo);
            var result = CreateCustomerRegistrationRequest<CustomerRegistrationResponse>(request);

            if (!customers.Any())
            {
                await _customerRegistration.AddCustomer(CreateCustomerRegistrationRequest<CustomerRegistration>(request));
            }
            else
            {
                throw new Exception("Customer already exists with same contact no.");
            }
            return result;
        }

        public async Task<CustomerRegistration> GetCustomerById(string id)
        {
            return await _customerRegistration.GetCustomerById(id);
        }

        public async Task<List<CustomerRegistration>> GetCustomers(int pageNumber, int pageSize)
        {
            var customers = await _customerRegistration.GetCustomers(pageNumber, pageSize);
            if (!customers.Any())
            {
                return new List<CustomerRegistration>();
            }
            return customers;
        }

        public CustomerRegistrationResponse UpdateCustomer(string Id, CustomerRegistrationUpdateRequest customerRegistrationUpdateRequest)
        {
            _customerRegistration.UpdateCustomer(Id, customerRegistrationUpdateRequest);
            var result = CreateCustomerRegistrationRequest<CustomerRegistrationResponse>(customerRegistrationUpdateRequest);
            return result;
        }

        //Generic Method to handle request and response according to base class
        private TCustomerRegistration CreateCustomerRegistrationRequest<TCustomerRegistration>(CustomerRegistrationBase customerRegistrationBase) where TCustomerRegistration : CustomerRegistrationBase, new()
        {
            return new TCustomerRegistration
            {
                //Id = customerRegistrationBase.Id,
                FullName = customerRegistrationBase.FullName,
                ContactNo = customerRegistrationBase.ContactNo,
                AltContactNo = customerRegistrationBase.AltContactNo,
                Address = new CustomerAddress
                {
                    Address = customerRegistrationBase.Address.Address,
                    City = customerRegistrationBase.Address.City,
                    Country = customerRegistrationBase.Address.Country,
                    Pincode = customerRegistrationBase.Address.Pincode,
                }
            };
        }

    }
}