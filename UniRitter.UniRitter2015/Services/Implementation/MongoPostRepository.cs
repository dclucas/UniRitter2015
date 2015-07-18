using System;
using System.Collections.Generic;
using MongoDB.Driver;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class MongoPostRepository : IRepository<PostModel>
    {
        private readonly IMongoCollection<PostModel> collection;
        private readonly IMongoDatabase database;

        public MongoPostRepository()
        {
            var client = new MongoClient("mongodb://localhost");
            database = client.GetDatabase("uniritter");
            collection = database.GetCollection<PostModel>("post");
        }

        public PostModel Add(PostModel model)
        {
            model.id = Guid.NewGuid();
            collection.InsertOneAsync(model).Wait();
            return model;
        }

        public bool Delete(Guid modelId)
        {
            var result = collection.DeleteOneAsync(
                p => p.id == modelId).Result;
            // "=>" identifica código lambda

            return result.DeletedCount > 0;
        }

        public PostModel Update(Guid id, PostModel model)
        {
            collection.ReplaceOneAsync(p => p.id == id, model).Wait();

            return model;
        }

        public IEnumerable<PostModel> GetAll()
        {
            var data = collection.Find(
                p => true).ToListAsync<PostModel>();
            return data.Result;
        }

        public PostModel GetById(Guid id)
        {
            var data = collection.Find(
                p => p.id == id).FirstOrDefaultAsync();
            return data.Result;
        }
    }
}