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

	    [Route("api/v1/brewery/"), HttpGet]
        public Brewery[] Get()
        {
            return _breweryRepository.GetAllBreweries();
        }

	    [Route("api/v1/brewery/{id}"), HttpGet]
		public Brewery Get(string id)
        {
            return _breweryRepository.GetBrewery(id);
        }

	    [Route("api/v1/brewery/"), HttpPost]
        public bool Post(Brewery brewery)
        {
            return _breweryRepository.AddBrewery(brewery);
        }

	    [Route("api/v1/brewery/"), HttpPut]
        public Brewery Put(Brewery brewery)
        {
            return _breweryRepository.UpdateBrewery(brewery);
        }

	    [Route("api/v1/brewery/"), HttpDelete]
        public bool Delete(string id)
        {
            return _breweryRepository.DeleteBrewery(id);
        }
    }
}