using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    abstract public class BaseController<TModel> : ApiController
        where TModel: class, IModel
    {
        private readonly IRepository<TModel> _repo;

        public BaseController(IRepository<TModel> repo)
        {
            _repo = repo;
        }

        // GET: api/Person
        public virtual IHttpActionResult Get()
        {
            return Json(_repo.GetAll());
        }

        // GET: api/Person/5
        public virtual IHttpActionResult Get(Guid id)
        {
            var data = _repo.GetById(id);
            if (data != null)
            {
                return Json(data);
            }

            return NotFound();
        }

        // POST: api/Person
        public virtual IHttpActionResult Post([FromBody] TModel person)
        {
            if (ModelState.IsValid)
            {
                var data = _repo.Add(person);
                return Json(data);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Person/5
        public virtual IHttpActionResult Put(Guid id, [FromBody] TModel person)
        {
            var data = _repo.Update(id, person);
            return Json(person);
        }

        // DELETE: api/Person/5
        public virtual IHttpActionResult Delete(Guid id)
        {
            _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
