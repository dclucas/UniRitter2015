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
            var data = new string[] { "value1", "value2" };
            return Json(data);
        }

        // GET: api/Person/5
        public IHttpActionResult Get(int id)
        {
            return Json("value");
        }

        // POST: api/Person
        public IHttpActionResult Post([FromBody]PersonModel person)
        {
            if (ModelState.IsValid)
            {
                person.id = Guid.NewGuid();
                return Json(person);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Person/5
        public IHttpActionResult Put(int id, [FromBody]PersonModel person)
        {
            return Json(person);
        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
            // todo: implement resource (logical) removal later on
            throw new NotImplementedException();
        }
    }
}