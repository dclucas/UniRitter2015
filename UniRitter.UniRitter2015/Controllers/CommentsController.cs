using System;
using System.Net;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly IRepository<CommentModel> _repo;

        public CommentsController(IRepository<CommentModel> repo)
        {
            _repo = repo;
        }

        // GET: api/Comment
        public IHttpActionResult Get()
        {
            return Json(_repo.GetAll());
        }

        // GET: api/Comment/5
        public IHttpActionResult Get(Guid id)
        {
            var data = _repo.GetById(id);
            if (data != null)
            {
                return Json(data);
            }

            return NotFound();
        }

        // POST: api/Comment
        public IHttpActionResult Post([FromBody] CommentModel person)
        {
            if (ModelState.IsValid)
            {
                var data = _repo.Add(person);
                return Json(data);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Comment/5
        public IHttpActionResult Put(Guid id, [FromBody] CommentModel person)
        {
            var data = _repo.Update(id, person);
            return Json(person);
        }

        // DELETE: api/Comment/5
        public IHttpActionResult Delete(Guid id)
        {
            _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
