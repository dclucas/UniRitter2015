using System;
using System.Collections.Generic;
using MongoDB.Driver;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class MongoPersonRepository : IRepository<PersonModel>
    {
        private readonly IMongoCollection<PersonModel> collection;
        private readonly IMongoDatabase database;

        public MongoPersonRepository()
        {
            var client = new MongoClient("mongodb://localhost");
            database = client.GetDatabase("uniritter");
            collection = database.GetCollection<PersonModel>("people");
        }

        public PersonModel Add(PersonModel model)
        {
            if (!model.id.HasValue)
            {
                model.id = Guid.NewGuid();
            }
            collection.InsertOneAsync(model).Wait();
            return model;
        }

        public bool Delete(Guid modelId)
        {
            var result = collection.DeleteOneAsync(
                p => p.id == modelId).Result;

            return result.DeletedCount > 0;
        }

        public PersonModel Update(Guid id, PersonModel model)
        {
            collection.ReplaceOneAsync(p => p.id == id, model).Wait();

            return model;
        }

        public IEnumerable<PersonModel> GetAll()
        {
            var data = collection.Find(
                p => true).ToListAsync();
            return data.Result;
        }

        public PersonModel GetById(Guid id)
        {
            var data = collection.Find(
                p => p.id == id).FirstOrDefaultAsync();
            return data.Result;
        }

        public void Upsert(IEnumerable<PersonModel> peopleList)
        {
            //collection.UpdateManyAsync()
            /*
             _collection.Update(
    Query.EQ("UUID", thing.UUID),
    Update.Replace(thing),
    UpsertFlags.Upsert
);
             */
            var options = new UpdateOptions {IsUpsert = true};
            foreach (var person in peopleList)
            {
                collection.ReplaceOneAsync(model => model.id == person.id, person, options);
            }
        }
    }
}