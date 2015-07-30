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
        public IRepository<TModel> _repo;

        public BaseController()
        {
          
        }

        // GET: api/Post
        public IHttpActionResult Get()
        {
            return Json(_repo.GetAll());
        }

        // GET: api/Post/5
        public IHttpActionResult Get(Guid id)
        {
            var data = _repo.GetById(id);
            if (data != null)
            {
                return Json(data);
            }

            return NotFound();
        }

        // POST: api/Post
        public IHttpActionResult Post([FromBody]TModel post)
        {
            if (ModelState.IsValid)
            {
                var data = _repo.Add(post);
                return Json(data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Post/5
        public IHttpActionResult Put(Guid id, [FromBody]TModel post)
        {
            var data = _repo.Update(id, post);
            return Json(post);
        }

        // DELETE: api/Post/5
        public IHttpActionResult Delete(Guid id)
        {
            _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
