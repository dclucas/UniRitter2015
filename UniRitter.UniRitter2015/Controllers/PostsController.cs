using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    public class PostsController : BaseController<PostModel>
    {
        public PostsController(IRepository<PostModel> repo)
        {
            _repo = repo;
        }
    }
}
