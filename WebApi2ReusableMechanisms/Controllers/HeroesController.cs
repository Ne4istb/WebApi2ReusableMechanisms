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
		readonly IValidator<HeroModel> validator;

		public HeroesController(IHeroRepository repository, IValidator<HeroModel> validator)
		{
			this.repository = repository;
			this.validator = validator;
		}

		[HttpGet]
		public HeroModel Get(Guid id)
		{
			return repository.Get(id);
		}

		[HttpPost]
		public HeroModel Post(HeroModel model)
		{
			ProcessValidationError(validator.Validate(model));
			return repository.Create(model);
		}

		[HttpPut]
		public void Put(Guid id, HeroModel model)
		{
			ProcessValidationError(validator.Validate(model));
			repository.Update(id, model);
		}

		void ProcessValidationError(ValidationResult result)
		{
			if (result.IsValid) return;

			var message = string.Join("; ", result.Errors.Select(error => error.ErrorMessage));
			ThrowUnprocessableEntityException(message);
		}

		[HttpDelete]
		public void Delete(Guid id)
		{
			repository.Remove(id);
		}

		void ThrowUnprocessableEntityException(string message)
		{
			var response = Request.CreateErrorResponse((HttpStatusCode)422, message);
			throw new HttpResponseException(response);
		}
	}
}
