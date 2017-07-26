using BeerWithRest.Models;

namespace BeerWithRest.Services
{
    public class BeerRepository
    {
        public Beer[] GetAllBeers()
        {
            return new Beer[]
            {
                new Beer
                {
                    Id = 1,
                    Name = "Red's Rye"
                },
                new Beer
                {
                    Id = 2,
                    Name = "Dragon's Milk"
                }
            };

        }
    }
}