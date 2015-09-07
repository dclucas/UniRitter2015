using System.Collections.Generic;
using System.Web.Http;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services.Implementation;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    public class CommentsController : BaseController<CommentModel>
    {
        private readonly IRepository<CommentModel> _repo;

        public CommentsController(IRepository<CommentModel> repo)
            : base (repo)
        {
            _repo = repo;
        }
    }
}