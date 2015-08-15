using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    public class PostsController : BaseController<PostModel>
    {
        private readonly IRepository<PostModel> _repo;

        public PostsController(IRepository<PostModel> repo) : base(repo)
        {
            _repo = repo;
        }

        // GET: api/Post
        /*public IHttpActionResult Get()
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
        public IHttpActionResult Post([FromBody] PostModel person)
        {
            if (ModelState.IsValid)
            {
                var data = _repo.Add(person);
                return Json(data);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Post/5
        public IHttpActionResult Put(Guid id, [FromBody] PostModel person)
        {
            var data = _repo.Update(id, person);
            return Json(person);
        }

        // DELETE: api/Post/5
        public IHttpActionResult Delete(Guid id)
        {
            _repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }*/
    }
}