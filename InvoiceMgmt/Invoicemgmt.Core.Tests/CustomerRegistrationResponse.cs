namespace Invoicemgmt.Core
{
    public class CustomerRegistrationResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string AltContactNo { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}