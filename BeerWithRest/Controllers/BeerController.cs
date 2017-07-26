using System.Web.Http;
using BeerWithRest.Models;
using BeerWithRest.Services;

namespace BeerWithRest.Controllers
{
    public class BeerController : ApiController
    {
        private BeerRepository beerRepository;

        public BeerController()
        {
            this.beerRepository = new BeerRepository();
        }

        public Beer[] Get()
        {
            return beerRepository.GetAllBeers();
        }
    }
}
