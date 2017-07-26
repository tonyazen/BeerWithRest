using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BeerWithRest.Models;

namespace BeerWithRest.Controllers
{
    public class BeerController : ApiController
    {
        public Beer[] Get()
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
