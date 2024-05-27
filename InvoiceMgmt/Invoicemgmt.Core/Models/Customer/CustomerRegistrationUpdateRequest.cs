using Invoicemgmt.Domain.BaseModels;

namespace Invoicemgmt.Core.Models.Customer
{
    public class CustomerRegistrationUpdateRequest:CustomerRegistrationBase
    {
        public string Id { get; set; }
    }
}