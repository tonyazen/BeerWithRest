using BeerWithRest.Models;
using System;
using System.Linq;
using System.Web;
using log4net;

namespace BeerWithRest.Services
{
    public class BeerRepository : IBeerRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(BeerRepository));

        private const string BeerCacheKey = "BeerStore";
	    private const string LastBeerIdCacheKey = "BeerIdStore";
        public BeerRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[BeerCacheKey] == null)
                {
                    var beers = new []
                    {
						new Beer
						{
							Id = "1",
							Name = "Red's Rye",
							Abv = "6.6%",
							BreweryId = "10005",
							Style = "Indian Pale Ale"
						},
						new Beer
						{
							Id = "2",
							Name = "DryPA",
							Abv = "6.2%",
							BreweryId = "10009",
							Style = "Pale Ale"
						},
						new Beer
						{
							Id = "3",
							Name = "Dragon's Milk",
							Abv = "11%",
							BreweryId = "10007",
							Style = "Stout"
						},
						new Beer
						{
							Id = "4",
							Name = "Chaga Khan",
							Abv = "10.4",
							BreweryId = "10008",
							Style = "Stout"
						},
						new Beer
						{
							Id = "5",
							Name = "Grapefruit IPA",
							Abv = "5%",
							BreweryId = "10006",
							Style = "Indian Pale Ale"
						}
					};
                    ctx.Cache[BeerCacheKey] = beers;
	                ctx.Cache[LastBeerIdCacheKey] = 5;
                }
			}
        }

        public Beer[] GetAllBeers()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Beer[])ctx.Cache[BeerCacheKey];
            }

            return new []
            {
                    new Beer
                    {
                        Id = "0",
                        Name = "",
                        Abv = "",
                        BreweryId = "",
                        Style = ""
                    }
            };
        }

        public Beer GetBeer(string id)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Beer[])ctx.Cache[BeerCacheKey]).ToList();
                    return currentData.FirstOrDefault(beer => beer.Id.ToString() == id);
                }
                catch (Exception ex)
                {
                    _logger.Error($"GetBeer Exception. Id: {id}. Exception: {ex}");
                    return null;
                }
            }

            return null;
        }

        public bool AddBeer(Beer beer)
        {

            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
	                var beerId = Convert.ToInt32(ctx.Cache[LastBeerIdCacheKey]) + 1;
					beer.Id = beerId.ToString();
                    var currentData = ((Beer[])ctx.Cache[BeerCacheKey]).ToList();
                    currentData.Add(beer);
                    ctx.Cache[BeerCacheKey] = currentData.ToArray();
	                ctx.Cache[LastBeerIdCacheKey] = beerId;

					return true;

                }
                catch (Exception ex)
                {
                    _logger.Error($"AddBeer Exception. Beer: {beer.Name}, {beer.Style}, {beer.Abv}, {beer.BreweryId}. Exception: {ex}");
                    return false;
                }
            }

            return false;
        }

        public Beer UpdateBeer(Beer updateBeer)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Beer[])ctx.Cache[BeerCacheKey]).ToList();
                    foreach (var beer in currentData)
                    {
                        if (beer.Id == updateBeer.Id)
                        {
                            beer.Name = updateBeer.Name;
                        }
                    }

                    ctx.Cache[BeerCacheKey] = currentData.ToArray();

                    return currentData.FirstOrDefault(beer => beer.Id == updateBeer.Id);
                }
                catch (Exception ex)
                {
                    _logger.Error($"UpdateBeer Exception. Id: {updateBeer.Id}. Exception: {ex}");
                    return updateBeer;
                }
            }

            return updateBeer;
        }

        public bool DeleteBeer(string id)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Beer[])ctx.Cache[BeerCacheKey]).ToList();
                    var beerToRemove = currentData.FirstOrDefault(beer => beer.Id.ToString() == id);
                    currentData.Remove(beerToRemove);
                    ctx.Cache[BeerCacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Error($"DeleteBeer Exception. Id: {id}. Exception: {ex}.");
                    return false;
                }
            }

            return false;
        }
    }
}