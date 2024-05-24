using Invoicemgmt.Core.Enums;
using Invoicemgmt.Core.Interfaces;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;
using Invoicemgmt.Domain.BaseModels;

namespace Invoicemgmt.Core.Services
{
    public class CustomerRegistrationRepository
    {
        private readonly ICustomerRegistrationService _customerRegistration;

        public CustomerRegistrationRepository(ICustomerRegistrationService customerRegistration)
        {
            _customerRegistration = customerRegistration;
        }

        public CustomerRegistrationResponse AddCustomer(CustomerRegistrationRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException();
            }

            var customers = _customerRegistration.GetCustomersByContactNo(request.ContactNo);
            var result = CreateCustomerRegistrationRequest<CustomerRegistrationResponse>(request);

            if (!customers.Any())
            {
                _customerRegistration.AddCustomer(CreateCustomerRegistrationRequest<CustomerRegistration>(request));
                result.ResultFlag = ResultFlag.Success;
            }
            else
            {
                result.ResultFlag = ResultFlag.Failure;
            }

            return result;
        }

        public CustomerRegistration GetCustomerById(string id)
        {
            return _customerRegistration.GetCustomerById(id);
        }

        public List<CustomerRegistration> GetCustomers()
        {
            var customers = _customerRegistration.GetCustomers();
            if (!customers.Any())
            {
                return new List<CustomerRegistration>();
            }
            return customers;
        }

        public CustomerRegistrationResponse UpdateCustomer(string Id, CustomerRegistrationUpdateRequest customerRegistrationUpdateRequest)
        {
            _customerRegistration.UpdateCustomer(Id, customerRegistrationUpdateRequest);
            var result = UpdateCustomerRegistrationRequest<CustomerRegistrationResponse>(customerRegistrationUpdateRequest);
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


        private TCustomerRegistration UpdateCustomerRegistrationRequest<TCustomerRegistration>(CustomerRegistrationUpdateRequest customerRegistration) where TCustomerRegistration : CustomerRegistrationResponse, new()
        {
            return new TCustomerRegistration
            {
                Id = customerRegistration.Id,
                FullName = customerRegistration.FullName,
                ContactNo = customerRegistration.ContactNo,
                AltContactNo = customerRegistration.AltContactNo,
                Address = new CustomerAddress
                {
                    Address = customerRegistration.Address.Address,
                    City = customerRegistration.Address.City,
                    Country = customerRegistration.Address.Country,
                    Pincode = customerRegistration.Address.Pincode,
                }
            };
        }

    }
}