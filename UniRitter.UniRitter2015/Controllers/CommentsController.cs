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
    public class CommentsController : BaseController<CommentModel>
    {
       
        public CommentsController(IRepository<CommentModel> repo)
        {
            this._repo = repo;
        }
    }
}
