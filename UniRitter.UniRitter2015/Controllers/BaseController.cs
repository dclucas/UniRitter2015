using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public BaseController(IRepository<TModel> repo)
        {
            this._repo = repo;
        }

        public virtual async Task<IHttpActionResult> Get()
        {
            return Json(await _repo.GetAll());
        }

        public virtual async Task<IHttpActionResult> Get(Guid id)
        {
            var data = await _repo.GetById(id);

            if (data != null)
            {
                return Json(data);
            }
            return NotFound();
        }

        public virtual async Task<IHttpActionResult> Post([FromBody]TModel post)
        {
            if (ModelState.IsValid)
            {
                var data = await _repo.Add(post);

                return Json(data);
            }
            return BadRequest(ModelState);
        }

        public virtual async Task<IHttpActionResult> Put(Guid id, [FromBody] TModel post)
        {
            var data = await _repo.Update(id, post);

            return Json(post);
        }

        public virtual async Task<IHttpActionResult> Delete(Guid id)
        {
            await _repo.Delete(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
