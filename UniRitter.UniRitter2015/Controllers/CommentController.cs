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
    public class CommentController : ApiController
    {
        private readonly IRepository<CommentModel> _repo;

        public CommentController(IRepository<CommentModel> repo)
        {
            this._repo = repo;
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
        public IHttpActionResult Post([FromBody]CommentModel comment)
        {
            if (ModelState.IsValid)
            {
                var data = _repo.Add(comment);
                return Json(data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Comment/5
        public IHttpActionResult Put(Guid id, [FromBody]CommentModel comment)
        {
            var data = _repo.Update(id, comment);
            return Json(comment);
        }

        // DELETE: api/Comment/5
        public IHttpActionResult Delete(Guid id)
        {
            _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}