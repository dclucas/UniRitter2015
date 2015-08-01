using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{

    public class BaseController : ApiController
    {
        private readonly IRepository<IModel> _repo;

        public BaseController(IRepository<IModel> repo)
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
            var data = await _repo.GetById(id);
            if (data != null)
            {
                return Json(data);
            }
            return NotFound();
        }

        // POST: api/Person
        public async Task<IHttpActionResult> Post([FromBody] IModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await _repo.Add(model);
                return Json(data);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Person/5
        public async Task<IHttpActionResult> Put(Guid id, [FromBody] IModel model)
        {
            var data = await _repo.Update(id, model);
            return Json(model);
        }

        // DELETE: api/Person/5
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
  
}