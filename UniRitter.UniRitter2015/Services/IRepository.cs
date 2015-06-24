using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services
{
    public interface IRepository<TModel>
    {
        TModel Add(TModel model);

        bool Delete(Guid modelId);

        TModel Update(Guid id, TModel model);

        IEnumerable<TModel> GetAll();

        TModel GetById(Guid id);
    }
}
