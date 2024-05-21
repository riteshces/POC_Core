namespace Invoicemgmt.Core
{
    public class CustomerRegistrationResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public int ContactNo { get; set; }
        public int AltContactNo { get; set; }
        public CustomerAddress Address { get; set; }
    }

    
}