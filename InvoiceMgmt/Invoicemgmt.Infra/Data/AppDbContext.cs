using Invoicemgmt.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicemgmt.Infra.Data
{
    public class AppDbContext:DbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public AppDbContext(IMongoClient mongoClient)
        {
            string strConnectionString = "mongodb://localhost:27017/TDD_POC";
            MongoUrl mongoUrl = new MongoUrl(strConnectionString);
            mongoClient = new MongoClient(mongoUrl);
            _mongoDatabase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<CustomerRegistration> CustomerRegistrations => _mongoDatabase.GetCollection<CustomerRegistration>("customermaster");


    }
}
