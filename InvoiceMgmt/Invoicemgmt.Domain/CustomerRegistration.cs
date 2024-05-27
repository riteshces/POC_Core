using Invoicemgmt.Domain.BaseModels;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Invoicemgmt.Domain
{
    public class CustomerRegistration:CustomerRegistrationBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }

}