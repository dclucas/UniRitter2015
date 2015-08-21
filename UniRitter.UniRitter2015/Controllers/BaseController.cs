using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    abstract public class BaseController<TModel> : ApiController
        where TModel: class, IModel
    {
        public IRepository<TModel> _repo;

        public BaseController(IRepository<TModel> repo)
        {
            _repo = repo;
        }

        public BaseController() { }

        // GET: api/Model
        public virtual async Task<IHttpActionResult> Get()
        {
            return Json(await _repo.GetAll());
        }

        // GET: api/Model/5
        public virtual async Task<IHttpActionResult> Get(Guid id)
        {
            var data = await _repo.GetById(id);
            if (data != null)
            {
                return Json(data);
            } else {
                return NotFound();
            }
        }

        // POST: api/Model
        public virtual async Task<IHttpActionResult> Post([FromBody]TModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await _repo.Add(model);
                return Json(data);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Model/5
        public virtual async Task<IHttpActionResult> Put(Guid id, [FromBody]TModel model)
        {
            var data = await _repo.Update(id, model);
            return Json(data);
        }

        // DELETE: api/Model/5
        public virtual async Task<IHttpActionResult> Delete(Guid id)
        {
            await _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
