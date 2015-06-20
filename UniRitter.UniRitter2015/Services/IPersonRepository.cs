using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services
{
    public interface IPersonRepository
    {
        PersonModel Add(PersonModel model);

        void Delete(Guid modelId);

        PersonModel Update(Guid id, PersonModel model);

        IEnumerable<PersonModel> GetAll();

        PersonModel GetById(Guid id);
    }
}
