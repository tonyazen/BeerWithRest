using System;
using System.Linq;
using System.Web;
using BeerWithRest.Models;

namespace BeerWithRest.Services
{
    public class BreweryRepository
    {
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
                            Id = "1",
                            Name = "Founder's Brewing Co.",
                            Address = new BreweryAddress
                            {
                                Street1 = "235 Grandville Ave SW",
                                Street2 = "",
                                City = "Grand Rapids",
                                StateCode = "MI",
                                ZipCode = "49503"
                            }
                        },
                        new Brewery
                        {
                            Id = "2",
                            Name = "Perrin Brewing Co.",
                            Address = new BreweryAddress
                            {
                                Street1 = "5910 Comstock Park Dr NW",
                                Street2 = "",
                                City = "Comstock Park",
                                StateCode = "MI",
                                ZipCode = "49321"
                            }
                        },
                        new Brewery
                        {
                            Id = "3",
                            Name = "Bell's Brewery",
                            Address = new BreweryAddress
                            {
                                Street1 = "355 E Kalamazoo Ave",
                                Street2 = "",
                                City = "Kalamazoo",
                                StateCode = "MI",
                                ZipCode = "49007"
                            }
                        },
                        new Brewery
                        {
                            Id = "4",
                            Name = "Greyline Brewing Co.",
                            Address = new BreweryAddress
                            {
                                Street1 = "1727 Alpine Ave NW",
                                Street2 = "",
                                City = "Grand Rapids",
                                StateCode = "MI",
                                ZipCode = "49504"
                            }
                        },
                        new Brewery
                        {
                            Id = "5",
                            Name = "City Built Brewing",
                            Address = new BreweryAddress
                            {
                                Street1 = "820 Monroe Ave NW #155",
                                Street2 = "",
                                City = "Grand Rapids",
                                StateCode = "MI",
                                ZipCode = "49503"
                            }
                        },
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
                        Name = "Placeholder"
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
                    //log it
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
                    //log it
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
                    //log it
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
                    //log it
                    return false;
                }
            }

            return false;
        }

    }
}