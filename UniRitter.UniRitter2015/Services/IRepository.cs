using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniRitter.UniRitter2015.Services
{
    public interface IRepository<TModel>
    {
        Task<TModel> Add(TModel model);
        Task<bool> Delete(Guid modelId);
        Task<TModel> Update(Guid id, TModel model);
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetById(Guid id);
    }
}