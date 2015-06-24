using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class PersonEFRepository : System.Data.Entity.DbContext, IRepository<PersonModel>
    {
        public DbSet<PersonModel> People { get; set; }

        public PersonModel Add(PersonModel model)
        {
            model.id = Guid.NewGuid();
            People.Add(model);
            this.SaveChanges();
            return model;
        }

        public bool Delete(Guid modelId)
        {
            var entity = GetById(modelId);
            if (entity == null) return false;

            People.Remove(entity);
            return true;
        }

        public PersonModel Update(Guid id, PersonModel model)
        {
            this.Entry(model).State = EntityState.Modified;
            this.SaveChanges();
            return model;
        }

        public IEnumerable<PersonModel> GetAll()
        {
            return People;
        }

        public PersonModel GetById(Guid id)
        {
            return People.Find(id);
        }
    }
}