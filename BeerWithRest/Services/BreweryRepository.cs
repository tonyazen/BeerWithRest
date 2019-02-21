using System;
using System.Linq;
using System.Web;
using BeerWithRest.Models;
using log4net;

namespace BeerWithRest.Services
{
    public class BreweryRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(BreweryRepository));

        private const string CacheKey = "BreweryStore";

        public BreweryRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var breweries = new Brewery[]
                    {
                        new Brewery
                        {
                            Id = "10001",
	                        Name = "Granite City Brewery",
	                        Address = new BreweryAddress
	                        {
		                        Street1 = "100 Renaissance Center",
		                        Street2 = "Ste. 1101",
		                        City = "Detroit",
		                        StateCode = "MI",
		                        ZipCode = "48243"
	                        }                           
                        },
                        new Brewery
                        {
                            Id = "10002",
	                        Name = "Atwater Brewery & Tap House",
	                        Address = new BreweryAddress
	                        {
		                        Street1 = "237 Jos Campau",
		                        Street2 = "",
		                        City = "Detroit",
		                        StateCode = "MI",
		                        ZipCode = "48207"
	                        }
                        },
                        new Brewery
                        {
                            Id = "10003",
	                        Name = "Detroit Beer Co.",
	                        Address = new BreweryAddress
	                        {
		                        Street1 = "1529 E. Broadway",
		                        Street2 = "",
		                        City = "Detroit",
		                        StateCode = "MI",
		                        ZipCode = "48226"
	                        }                            
                        },
                        new Brewery
                        {
                            Id = "10004",
	                        Name = "Jolly Pumpkin Pizzeria & Brewery",
	                        Address = new BreweryAddress
	                        {
		                        Street1 = "441 W Canfield St",
		                        Street2 = "#9",
		                        City = "Detroit",
		                        StateCode = "MI",
		                        ZipCode = "48201"
	                        }
                        },
                        new Brewery
                        {
                            Id = "10005",
	                        Name = "Founders Brewing Co. Detroit",
	                        Address = new BreweryAddress
	                        {
		                        Street1 = "456 Charlotte St",
		                        Street2 = "",
		                        City = "Detroit",
		                        StateCode = "MI",
		                        ZipCode = "48201"
							}
						}
                    };

                    ctx.Cache[CacheKey] = breweries;
                }
            }
        }

        public Brewery[] GetAllBreweries()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Brewery[])ctx.Cache[CacheKey];
            }

            return new Brewery[]
            {
                    new Brewery
                    {
                        Id = "0",
                        Name = ""
                    }
            };
        }

        public Brewery GetBrewery(string id)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Brewery[])ctx.Cache[CacheKey]).ToList();
                    return currentData.FirstOrDefault(brewery => brewery.Id.ToString() == id);
                }
                catch (Exception ex)
                {
                    _logger.Error($"GetBrewery Exception. Id: {id}. Exception: {ex}");
                    return null;
                }
            }

            return null;
        }

        public bool AddBrewery(Brewery brewery)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Brewery[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(brewery);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;

                }
                catch (Exception ex)
                {
                    _logger.Error($"AddBrewery Exception. Beer: {brewery.Name}, {brewery.Address.Street1}, {brewery.Address.Street2}, {brewery.Address.City}, {brewery.Address.StateCode}, {brewery.Address.ZipCode}. Exception: {ex}");
                    return false;
                }
            }

            return false;
        }

        public Brewery UpdateBrewery(Brewery updateBrewery)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Brewery[])ctx.Cache[CacheKey]).ToList();
                    foreach (var brewery in currentData)
                    {
                        if (brewery.Id == updateBrewery.Id)
                        {
                            brewery.Name = updateBrewery.Name;
                        }
                    };

                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return currentData.FirstOrDefault(brewery => brewery.Id == updateBrewery.Id);
                }
                catch (Exception ex)
                {
                    _logger.Error($"UpdateBrewery Exception. Id: {updateBrewery.Id}. Exception: {ex}");
                    return updateBrewery;
                }
            }

            return updateBrewery;
        }

        public bool DeleteBrewery(string id)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Brewery[])ctx.Cache[CacheKey]).ToList();
                    var breweryToRemove = currentData.FirstOrDefault(brewery => brewery.Id.ToString() == id);
                    currentData.Remove(breweryToRemove);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Error($"DeleteBrewery Exception. Id: {id}. Exception: {ex}.");
                    return false;
                }
            }

            return false;
        }

    }
}