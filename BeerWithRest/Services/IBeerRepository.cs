using BeerWithRest.Models;

namespace BeerWithRest.Services
{
    public interface IBeerRepository
    {
        Beer[] GetAllBeers();
        Beer GetBeer(string id);
        bool AddBeer(Beer beer);
        Beer UpdateBeer(Beer updateBeer);
        bool DeleteBeer(string id);
    }
}