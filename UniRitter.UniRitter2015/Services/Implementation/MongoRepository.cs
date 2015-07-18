using System;
using System.Collections.Generic;
using MongoDB.Driver;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class MongoRepository<TModel> : IRepository<TModel> where TModel: class, IModel
    {
        private IMongoCollection<TModel> collection;
        private IMongoDatabase database;

        public MongoRepository()
        {
            var typeName = typeof(TModel).Name;
            var collectionName = typeName.Substring(0, typeName.LastIndexOf("Model"));
            SetupCollection(collectionName);

        }
        
        private void SetupCollection(string collectionName)
        {
            var client = new MongoClient("mongodb://localhost");
            database = client.GetDatabase("uniritter");
            collection = database.GetCollection<TModel>(collectionName);
        }

        public virtual TModel Add(TModel model)
        {
            if (!model.id.HasValue)
            {
                model.id = Guid.NewGuid();
            }
            collection.InsertOneAsync(model).Wait();
            return model;
        }

        public virtual bool Delete(Guid modelId)
        {
            var result = collection.DeleteOneAsync(
                p => p.id == modelId).Result;

            return result.DeletedCount > 0;
        }

        public virtual TModel Update(Guid id, TModel model)
        {
            collection.ReplaceOneAsync(p => p.id == id, model).Wait();

            return model;
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            var data = collection.Find(
                p => true).ToListAsync();
            return data.Result;
        }

        public virtual TModel GetById(Guid id)
        {
            var data = collection.Find(
                p => p.id == id).FirstOrDefaultAsync();
            return data.Result;
        }

        public virtual void Upsert(IEnumerable<TModel> itemList)
        {
            var options = new UpdateOptions { IsUpsert = true };
            foreach (var item in itemList)
            {
                collection.ReplaceOneAsync(model => model.id == item.id, item, options);
            }
        }
    }
}