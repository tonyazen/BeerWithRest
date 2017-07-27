using BeerWithRest.Models;
using BeerWithRest.Services;
using System.Web.Http;

namespace BeerWithRest.Controllers
{
    public class BreweryController : ApiController
    {
        private readonly BreweryRepository _breweryRepository;

        public BreweryController()
        {
            _breweryRepository = new BreweryRepository();
        }

        public Brewery[] Get()
        {
            return _breweryRepository.GetAllBreweries();
        }

        public Brewery Get(string id)
        {
            return _breweryRepository.GetBrewery(id);
        }

        [HttpPost]
        public bool Post(Brewery brewery)
        {
            return _breweryRepository.AddBrewery(brewery);
        }

        [HttpPut]
        public Brewery Put(Brewery brewery)
        {
            return _breweryRepository.UpdateBrewery(brewery);
        }

        [HttpDelete]
        public bool Delete(string id)
        {
            return _breweryRepository.DeleteBrewery(id);
        }
    }
}