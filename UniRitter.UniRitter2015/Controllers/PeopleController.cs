using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly IRepository<PersonModel> _repo;

        public PeopleController(IRepository<PersonModel> repo)
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
        public async Task<IHttpActionResult> Post([FromBody] PersonModel person)
        {
            if (ModelState.IsValid)
            {
                var data = await _repo.Add(person);
                return Json(data);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Person/5
        public async Task<IHttpActionResult> Put(Guid id, [FromBody] PersonModel person)
        {
            var data = await _repo.Update(id, person);
            return Json(person);
        }

        // DELETE: api/Person/5
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}