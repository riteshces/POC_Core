
namespace Invoicemgmt.Core
{
    public class CustomerRegistrationProcessor
    {
        public CustomerRegistrationProcessor()
        {
        }

        public CustomerRegistrationResponse AddCustomer(CustomerRegistrationRequest request)
        {
            return new CustomerRegistrationResponse
            {
                FullName = request.FullName,
                ContactNo = request.ContactNo,
                AltContactNo = request.AltContactNo,
                Address=request.Address
            };
        }
    }
}