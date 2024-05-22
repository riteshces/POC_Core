using Invoicemgmt.Core.Enums;
using Invoicemgmt.Core.Interfaces;
using Invoicemgmt.Core.Models.Customer;
using Invoicemgmt.Domain;
using Invoicemgmt.Domain.BaseModels;

namespace Invoicemgmt.Core.Repository
{
    public class CustomerRegistrationRepository
    {
        private readonly ICustomerRegistration _customerRegistration;

        public CustomerRegistrationRepository(ICustomerRegistration customerRegistration)
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
                result.ResultFlag=ResultFlag.Success;
            }
            else
            {
                result.ResultFlag = ResultFlag.Failure;
            }

            return result;
        }


        //Generic Method to handle request and response according to base class
        private TCustomerRegistration CreateCustomerRegistrationRequest<TCustomerRegistration>(CustomerRegistrationBase customerRegistrationBase) where TCustomerRegistration : CustomerRegistrationBase, new()
        {
            return new TCustomerRegistration
            {
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