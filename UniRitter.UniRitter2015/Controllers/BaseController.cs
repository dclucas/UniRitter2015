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
        public async Task<IHttpActionResult> Get()
        {
            return Json(await _repo.GetAll());
        }

        // GET: api/Person/5
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var data = _repo.GetById(id);
            if (data != null)
            {
                return Json(await data);
            }

            return NotFound();
        }

        // POST: api/Person
        public async Task<IHttpActionResult> Post([FromBody] TModel modelo)
        {
            if (ModelState.IsValid)
            {
                var data = _repo.Add(modelo);
                return Json(await data);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Person/5
        public async Task<IHttpActionResult> Put(Guid id, [FromBody] TModel modelo)
        {
            var data = _repo.Update(id, modelo);
            return Json(await data);
        }

        // DELETE: api/Person/5
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
