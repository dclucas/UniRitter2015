using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;

namespace UniRitter.UniRitter2015.Controllers
{
    public class PeopleController : BaseController<PersonModel>
    {
        //private IRepository<PersonModel> _repo;

        public PeopleController(IRepository<PersonModel> repo)
        {
            _repo = repo;
        }
    }
}