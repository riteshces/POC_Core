using Invoicemgmt.Core.Enums;
using Invoicemgmt.Domain.BaseModels;

namespace Invoicemgmt.Core.Models.Customer
{
    public class CustomerRegistrationResponse:CustomerRegistrationBase
    {
        public string Id { get; set; }
        public ResultFlag ResultFlag { get; set; }
    }


}