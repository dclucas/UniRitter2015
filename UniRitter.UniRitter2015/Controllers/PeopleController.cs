using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Controllers
{
    public class PeopleController : ApiController
    {
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
        public IHttpActionResult Post([FromBody]PersonModel value)
        {
            if (ModelState.IsValid)
            {
                value.id = Guid.NewGuid();
                return Json(value);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Person/5
        public IHttpActionResult Put(int id, [FromBody]PersonModel value)
        {
            return Json(value);
        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
        }
    }
}