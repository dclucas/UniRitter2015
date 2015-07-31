using System;
using System.Collections.Generic;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class InMemoryRepository<TModel> : IRepository<TModel> where TModel: class, IModel
    {
        private static readonly Dictionary<Guid, TModel> Data = new Dictionary<Guid, TModel>();

        public TModel Add(TModel model)
        {
            if (!model.id.HasValue)
            {
                model.id = Guid.NewGuid();
            }
            // TODO: this is __NOT__ thread safe!
            Data[model.id.Value] = model;
            return model;
        }

        public bool Delete(Guid modelId)
        {
            Data.Remove(modelId);
            return true;
        }

        public TModel Update(Guid id, TModel model)
        {
            // TODO: this is __NOT__ thread safe!
            // TODO: id should be checked against model.id
            Data[id] = model;
            return model;
        }

        public IEnumerable<TModel> GetAll()
        {
            return Data.Values;
        }

        public TModel GetById(Guid id)
        {
            return Data.ContainsKey(id) ? Data[id] : null;
        }
    }
}