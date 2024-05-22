using Newtonsoft.Json.Bson;

namespace Invoicemgmt.Core
{
    public class CustomerRegistrationRequest
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int ContactNo { get; set; }
        public int AltContactNo { get; set; }
        public CustomerAddress Address { get; set; }
    }

    public class CustomerAddress
    {
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}