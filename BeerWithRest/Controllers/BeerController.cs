using System;
using System.Net;
using System.Web.Http;
using BeerWithRest.Models;
using BeerWithRest.Services;
using log4net;

namespace BeerWithRest.Controllers
{
	[RoutePrefix("api/v1")] //TODO: maybe do this instead of route info below
	public class BeerController : ApiController
	{
		private readonly IBeerRepository _beerRepository;
		private readonly ILog _logger;

		public BeerController() //TODO: Changed this to be injected into constructor rather than creating new one which should be better for testing ???
		{
			_beerRepository = new BeerRepository();
			_logger = LogManager.GetLogger("logger");
			//_beerRepository = beerRepository;
			//_logger = logger;
		}

		[Route("beer/"), HttpGet]
		public Beer[] Get()
		{
			return _beerRepository.GetAllBeers();
		}

		[Route("beer/{id}"), HttpGet]
		public Beer Get(string id)
		{
			return _beerRepository.GetBeer(id);
		}

		[Route("beer/"), HttpPost]
		public IHttpActionResult Post(Beer beer) //TODO: Use DTOs (per-request objects). Example: Post([FromBody] BeerDto beerDto)
		{
			//TODO: Validate all data
			//TODO: Separate validation logic from DTO
			//TODO: Look at fluent validation
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
				//return BadRequest("Error message"); //can return a message instead
			}

			try
			{
				var result = _beerRepository.AddBeer(beer); //TODO: The controller should know where, but not how to do it.
				if (result)
				{
					//TODO: Something?
				}
				else
				{
					return Content(HttpStatusCode.NotFound, "Beer not found");
					//return NotFound(); //No Message
					//TODO: something else?
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex); //TODO: Change to logging
				return BadRequest(ex.Message);
			}

			return Ok(beer);
		}

		[Route("beer/"), HttpPut]
		public Beer Put(Beer beer)
		{
			return _beerRepository.UpdateBeer(beer);
		}

		[Route("beer/"), HttpDelete]
		public bool Delete(string id)
		{
			return _beerRepository.DeleteBeer(id);
		}
	}
}
