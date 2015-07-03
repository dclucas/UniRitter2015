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
        private IMongoCollection<PersonModel> collection;

        public MongoPersonRepository()
        {
            var client = new MongoClient("mongodb://localhost");
            database = client.GetDatabase("uniritter");
            collection = database.GetCollection<PersonModel>("people");
        }

        public PersonModel Add(PersonModel model)
        {
            model.id = Guid.NewGuid();
            collection.InsertOneAsync(model);
            return model;
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
            var data = collection.Find(p => true).ToListAsync<PersonModel>();
            return data.Result;
        }

        public PersonModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}