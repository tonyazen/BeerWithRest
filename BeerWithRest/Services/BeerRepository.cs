using BeerWithRest.Models;
using System;
using System.Linq;
using System.Web;
using log4net;

namespace BeerWithRest.Services
{
    public class BeerRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(BeerRepository));

        private const string CacheKey = "BeerStore";

        public BeerRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var beers = new []
                    {
                        new Beer
                        {
                            Id = "1",
                            Name = "Red's Rye",
                            Abv = "6.6%",
                            Brewery = "Founder's Brewing Co.",
                            Style = "Indian Pale Ale"
                        },
                        new Beer
                        {
                            Id = "2",
                            Name = "Dragon's Milk",
                            Abv = "11%",
                            Brewery = "New Holland Brewing Co.",
                            Style = "Stout"
                        }
                    };

                    ctx.Cache[CacheKey] = beers;
                }
            }
        }

        public Beer[] GetAllBeers()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Beer[])ctx.Cache[CacheKey];
            }

            return new []
            {
                    new Beer
                    {
                        Id = "0",
                        Name = "",
                        Abv = "",
                        Brewery = "",
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
                    var currentData = ((Beer[])ctx.Cache[CacheKey]).ToList();
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
                    var currentData = ((Beer[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(beer);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;

                }
                catch (Exception ex)
                {
                    _logger.Error($"AddBeer Exception. Beer: {beer.Name}, {beer.Style}, {beer.Abv}, {beer.Brewery}. Exception: {ex}");
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
                    var currentData = ((Beer[])ctx.Cache[CacheKey]).ToList();
                    foreach (var beer in currentData)
                    {
                        if (beer.Id == updateBeer.Id)
                        {
                            beer.Name = updateBeer.Name;
                        }
                    }

                    ctx.Cache[CacheKey] = currentData.ToArray();

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
                    var currentData = ((Beer[])ctx.Cache[CacheKey]).ToList();
                    var beerToRemove = currentData.FirstOrDefault(beer => beer.Id.ToString() == id);
                    currentData.Remove(beerToRemove);
                    ctx.Cache[CacheKey] = currentData.ToArray();

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