using System.Web.Http;
using BeerWithRest.Models;
using BeerWithRest.Services;

namespace BeerWithRest.Controllers
{
    public class BeerController : ApiController
    {
        private readonly BeerRepository _beerRepository;

        public BeerController()
        {
            _beerRepository = new BeerRepository();
        }

        public Beer[] Get()
        {
            return _beerRepository.GetAllBeers();
        }

        public Beer Get(string id)
        {
            return _beerRepository.GetBeer(id);
        }

        [HttpPost]
        public bool Post(Beer beer)
        {
            return _beerRepository.AddBeer(beer);
        }

        [HttpPut]
        public Beer Put(Beer beer)
        {
            return _beerRepository.UpdateBeer(beer);
        }

        [HttpDelete]
        public bool Delete(string id)
        {
            return _beerRepository.DeleteBeer(id);
        }
    }
}
