using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class InMemoryRepository<TModel> : IRepository<TModel> where TModel: class, IModel
    {
        private static readonly Dictionary<Guid, TModel> Data = new Dictionary<Guid, TModel>();

        public Task<TModel> Add(TModel model)
        {
            if (!model.id.HasValue)
            {
                model.id = Guid.NewGuid();
            }
            // TODO: this is __NOT__ thread safe!
            Data[model.id.Value] = model;
            return Task.FromResult(model);
        }

        public Task<bool> Delete(Guid modelId)
        {
            Data.Remove(modelId);
            return Task.FromResult(true);
        }

        public Task<TModel> Update(Guid id, TModel model)
        {
            // TODO: this is __NOT__ thread safe!
            // TODO: id should be checked against model.id
            Data[id] = model;
            return Task.FromResult(model);
        }

        public Task<IEnumerable<TModel>> GetAll()
        {
            IEnumerable<TModel> values = Data.Values;
            return Task.FromResult(values);
        }

        public Task<TModel> GetById(Guid id)
        {
            return Task.FromResult(Data.ContainsKey(id) ? Data[id] : null);
        }
    }
}