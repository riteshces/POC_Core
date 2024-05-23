﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicemgmt.Domain.BaseModels
{
    public abstract class CustomerRegistrationBase
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string AltContactNo { get; set; }
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
