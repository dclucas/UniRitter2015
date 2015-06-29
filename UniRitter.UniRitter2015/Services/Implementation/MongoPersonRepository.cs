using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class MongoPersonRepository : IRepository<PersonModel>
    {
        private IMongoDatabase database;

        public MongoPersonRepository()
        {
            var client = new MongoClient("mongodb://localhost");
            database = client.GetDatabase("foo");
        }

        public PersonModel Add(PersonModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid modelId)
        {
            throw new NotImplementedException();
        }

        public PersonModel Update(Guid id, PersonModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public PersonModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}