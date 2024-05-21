
namespace Invoicemgmt.Core
{
    public class CustomerRegistrationProcessor
    {
        public CustomerRegistrationProcessor()
        {
        }

        public CustomerRegistrationResponse AddCustomer(CustomerRegistration request)
        {
          return new CustomerRegistrationResponse
            {
                Pincode = request.Pincode,
                FullName = request.FullName,
                ContactNo = request.ContactNo,
                AltContactNo = request.AltContactNo,
                Address = request.Address,
                City = request.City,
                Country = request.Country
            };
        }
    }
}