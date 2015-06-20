using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class PersonInMemoryRepository : IPersonRepository
    {
        private static Dictionary<Guid, PersonModel> data = new Dictionary<Guid, PersonModel>();

        public PersonModel Add(PersonModel model)
        {
            var id = Guid.NewGuid();
            model.id = id;
            // TODO: this is __NOT__ thread safe!
            data[id] = model;
            return model;
        }

        public void Delete(Guid modelId)
        {
            data.Remove(modelId);
        }

        public PersonModel Update(Guid id, PersonModel model)
        {
            // TODO: this is __NOT__ thread safe!
            // TODO: id should be checked against model.id
            data[id] = model;
            return model;
        }

        public IEnumerable<PersonModel> GetAll()
        {
            return data.Values;
        }

        public PersonModel GetById(Guid id)
        {
            return data.ContainsKey(id) ? data[id] : null;
        }
    }
}