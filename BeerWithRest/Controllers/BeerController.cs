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

        public Beer Get(string id)
        {
            return beerRepository.GetBeer(id);
        }

        [HttpPost]
        public bool Post(Beer beer)
        {
            return beerRepository.AddBeer(beer);
        }

        [HttpPut]
        public Beer Put(Beer beer)
        {
            return beerRepository.UpdateBeer(beer);
        }

        [HttpDelete]
        public bool Delete(string id)
        {
            return beerRepository.DeleteBeer(id);
        }
    }
}
