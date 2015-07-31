using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Support;
using System.Threading.Tasks;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class MongoRepository<TModel> : IRepository<TModel> where TModel : class, IModel
    {
        private IMongoCollection<TModel> collection;
        private IMongoDatabase database;

        public MongoRepository(IApiConfig cfg)
        {
            var typeName = typeof(TModel).Name;
            var collectionName = typeName.Substring(0,
                typeName.Length - "Model".Length);
            var client = new MongoClient(cfg.MongoDbUrl);
            database = client.GetDatabase(cfg.MongoDbName);
            collection = database.GetCollection<TModel>(collectionName);
        }

        public virtual Task<TModel> Add(TModel model)
        {
            if (!model.id.HasValue)
            {
                model.id = Guid.NewGuid();
            }
            // todo: remove this wait
            collection.InsertOneAsync(model).Wait();
            return Task.FromResult(model);
        }

        public virtual Task<bool> Delete(Guid modelId)
        {
            var result = collection.DeleteOneAsync(
                p => p.id == modelId).Result;

            return Task.FromResult(result.DeletedCount > 0);
        }

        public virtual async Task<TModel> Update(Guid id, TModel model)
        {
            var res = await collection.ReplaceOneAsync(p => p.id == id, model);
            
            return model;
        }

        public virtual async Task<IEnumerable<TModel>> GetAll()
        {
            var data = collection.Find(
                p => true).ToListAsync();

            return await data;
        }

        public virtual Task<TModel> GetById(Guid id)
        {
            var data = collection.Find(
                p => p.id == id).FirstOrDefaultAsync();

            return data;
        }

        public virtual Task Upsert(IEnumerable<TModel> itemList)
        {
            var options = new UpdateOptions { IsUpsert = true };
            var results = from item in itemList
                          select collection.ReplaceOneAsync(model => model.id == item.id, item, options);
            return Task.WhenAll(results);
        }
    }
}