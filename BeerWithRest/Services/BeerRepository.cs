using BeerWithRest.Models;
using System;
using System.Linq;
using System.Web;

namespace BeerWithRest.Services
{
    public class BeerRepository
    {
        private const string CacheKey = "BeerStore";

        public BeerRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var beers = new Beer[]
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

            return new Beer[]
            {
                    new Beer
                    {
                        Id = 0,
                        Name = "Placeholder"
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
                    //log it
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
                    //log it
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
                    };

                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return currentData.FirstOrDefault(beer => beer.Id == updateBeer.Id);
                }
                catch (Exception ex)
                {
                    //log it
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
                    //log it
                    return false;
                }
            }

            return false;
        }
    }
}