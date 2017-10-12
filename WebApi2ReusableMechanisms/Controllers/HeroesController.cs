using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FluentValidation;
using FluentValidation.Results;
using WebApi2ReusableMechanisms.Domain;
using WebApi2ReusableMechanisms.Models;

namespace WebApi2ReusableMechanisms.Controllers
{
	[RoutePrefix("heroes/{id?}")]
	[Route("", Name = "HeroRoute")]
	public class HeroesController : ApiController
	{
		readonly IHeroRepository repository;

		public HeroesController(IHeroRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		public HeroModel Get(Guid id)
		{
			return repository.Get(id);
		}

		[HttpPost]
		public HeroModel Post(HeroModel model)
		{
			return repository.Create(model);
		}

		[HttpPut]
		public void Put(Guid id, HeroModel model)
		{
			repository.Update(id, model);
		}

		[HttpDelete]
		public void Delete(Guid id)
		{
			repository.Remove(id);
		}
	}
}
