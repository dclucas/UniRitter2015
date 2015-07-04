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
    public class PostController : ApiController
    {
        private readonly IRepository<PostModel> _repo;

        public PostController(IRepository<PostModel> repo)
        {
            this._repo = repo;
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
        public IHttpActionResult Post([FromBody]PostModel post)
        {
            if (ModelState.IsValid)
            {
                post.id = Guid.NewGuid();
                var data = _repo.Add(post);
                return Created("posts/" + data.id, data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Post/5
        public IHttpActionResult Put(Guid id, [FromBody]PostModel post)
        {
            if (ModelState.IsValid)
            {
                return Json(_repo.Update(id, post));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Post/5
        public IHttpActionResult Delete(Guid id)
        {
            _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}