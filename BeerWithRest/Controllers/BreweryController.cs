using System.Web.Mvc;
using BeerWithRest.Models;
using BeerWithRest.Services;

namespace BeerWithRest.Controllers
{
    public class BreweryController : Controller
    {
        private BreweryRepository breweryRepository;

        public BreweryController()
        {
            this.breweryRepository = new BreweryRepository();
        }

        public Brewery[] Get()
        {
            return breweryRepository.GetAllBreweries();
        }

        public Brewery Get(string id)
        {
            return breweryRepository.GetBrewery(id);
        }

        [HttpPost]
        public bool Post(Brewery brewery)
        {
            return breweryRepository.AddBrewery(brewery);
        }

        [HttpPut]
        public Brewery Put(Brewery brewery)
        {
            return breweryRepository.UpdateBrewery(brewery);
        }

        [HttpDelete]
        public bool Delete(string id)
        {
            return breweryRepository.DeleteBrewery(id);
        }
    }
}