using System;
using System.Net;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    abstract public class BaseController<TModel> : ApiController
        where TModel: class, IModel
    {
        public IRepository<TModel> _repo;

        public BaseController()
        {

        }

        public BaseController(IRepository<TModel> repo)
        {
            _repo = repo;
        }

        // GET: api/Person
        public IHttpActionResult Get()
        {
            return Json(_repo.GetAll());
        }

        // GET: api/Person/5
        public IHttpActionResult Get(Guid id)
        {
            var data = _repo.GetById(id);
            if (data != null)
            {
                return Json(data);
            }
            return NotFound();
        }

        // POST: api/Person
        public IHttpActionResult Post([FromBody] TModel model)
        {
            if (ModelState.IsValid)
            {
                var data = _repo.Add(model);
                return Json(data);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Person/5
        public IHttpActionResult Put(Guid id, [FromBody] TModel model)
        {
            var data = _repo.Update(id, model);
            return Json(model);
        }

        // DELETE: api/Person/5
        public IHttpActionResult Delete(Guid id)
        {
            _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}