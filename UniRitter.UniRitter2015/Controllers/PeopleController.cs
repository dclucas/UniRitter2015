using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    public class PeopleController : ApiController
    {
        private IPersonRepository _repo;

        public PeopleController(IPersonRepository repo)
        {
            this._repo = repo;
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
        public IHttpActionResult Post([FromBody]PersonModel person)
        {
            if (ModelState.IsValid)
            {
                var data = _repo.Add(person);
                return Json(data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Person/5
        public IHttpActionResult Put(Guid id, [FromBody]PersonModel person)
        {
            var data = _repo.Update(id, person);
            return Json(person);
        }

        // DELETE: api/Person/5
        public IHttpActionResult Delete(Guid id)
        {
            _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}