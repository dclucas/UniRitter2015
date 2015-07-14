using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class MongoCommentRepository : IRepository<CommentModel>
    {
        private IMongoDatabase database;
        private IMongoCollection<CommentModel> collection;

        public MongoCommentRepository()
        {
            var client = new MongoClient("mongodb://localhost");
            database = client.GetDatabase("uniritter");
            collection = database.GetCollection<CommentModel>("comment");
        }

        public CommentModel Add(CommentModel model)
        {
            model.id = Guid.NewGuid();
            collection.InsertOneAsync(model).Wait();
            return model;
        }

        public bool Delete(Guid modelId)
        {
            var result = collection.DeleteOneAsync(
                p => p.id == modelId).Result;

            return result.DeletedCount > 0;
        }

        public CommentModel Update(Guid id, CommentModel model)
        {
            collection.ReplaceOneAsync(p => p.id == id, model).Wait();

            return model;
        }

        public IEnumerable<CommentModel> GetAll()
        {
            var data = collection.Find(
                p => true).ToListAsync<CommentModel>();
            return data.Result;
        }

        public CommentModel GetById(Guid id)
        {
            var data = collection.Find(
                p => p.id == id).FirstOrDefaultAsync();
            return data.Result;
        }
    }
}