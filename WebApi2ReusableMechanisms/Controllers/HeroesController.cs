using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
			Validate(model);
			return repository.Create(model);
		}

		[HttpPut]
		public void Put(Guid id, HeroModel model)
		{
			Validate(model);
			repository.Update(id, model);
		}

		[HttpDelete]
		public void Delete(Guid id)
		{
			repository.Remove(id);
		}

		void Validate(HeroModel model)
		{
			if (string.IsNullOrEmpty(model.Name))
				ThrowUnprocessableEntityException("Name should not be empty");

			if (model.Name.Length > 80)
				ThrowUnprocessableEntityException("Name should not be longer than 80 chars");

			if (repository.Exists(model.Name))
				ThrowUnprocessableEntityException($"Hero wiht name {model.Name} already exists");

			if (model.AlterEgo != null && model.AlterEgo.Length > 80)
				ThrowUnprocessableEntityException("AlterEgo should not be longer than 80 chars");

			if (model.Age < 0)
				ThrowUnprocessableEntityException("Age should be positive number");

			if (model.SuperPowers == null || model.SuperPowers.Length == 0)
				ThrowUnprocessableEntityException("Hero should have at least one super power");

			if (model.SuperPowers.Any(power => string.IsNullOrEmpty(power.Description)))
				ThrowUnprocessableEntityException("Each super power should have description");

			if (model.Lair == null)
				ThrowUnprocessableEntityException("Lair should not be null");

			if (model.Lair.LairType == LairType.None && !string.IsNullOrEmpty(model.Lair.Location))
				ThrowUnprocessableEntityException("Lair location should be null if LairType is None");
		}

		void ThrowUnprocessableEntityException(string message)
		{
			var response = Request.CreateErrorResponse((HttpStatusCode)422, message);
			throw new HttpResponseException(response);
		}
	}
}
