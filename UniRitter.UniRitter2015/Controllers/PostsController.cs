using System.Collections.Generic;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    public class PostsController :BaseController<PostModel>
    {
        private readonly IRepository<PostModel> _repo;

        public PostsController(IRepository<PostModel> repo)
        {
            _repo = repo;
        }
    }
}