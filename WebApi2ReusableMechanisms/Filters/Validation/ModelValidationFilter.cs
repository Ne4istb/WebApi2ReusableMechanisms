using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FluentValidation;

namespace WebApi2ReusableMechanisms.Filters.Validation
{
	public class ModelValidationFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext context)
		{
			var arguments = GetActionArguments(context);
			if (arguments == null)
				return;

			var hasBindingErrors = !context.ModelState.IsValid;
			IValidatorFactory validatorFactory = new CustomValidatorFactory(context.Request.GetDependencyScope());

			foreach (var argument in arguments)
			{
				if (argument.Value == null)
				{
					if (IsOptionalArgument(context, argument, validatorFactory))
						continue;

					context.ModelState.AddModelError(argument.Key, new SerializationException("Missing argument"));
					hasBindingErrors = true;
				}

				ValidateArgument(context, argument, validatorFactory);
			}

			if (!context.ModelState.IsValid)
			{
				context.Response = context.Request.CreateResponse(
					hasBindingErrors ?
						HttpStatusCode.BadRequest : (HttpStatusCode)422,
					context.ModelState.GetErrors());
			}
		}

		static IEnumerable<KeyValuePair<string, object>> GetActionArguments(HttpActionContext context)
		{
			return context.ActionArguments
				.Select(argument => argument);
		}

		static bool IsOptionalArgument(HttpActionContext context, KeyValuePair<string, object> argument, IValidatorFactory validatorFacoty)
		{
			var actionArgumentDescriptor = GetActionArgumentDescriptor(context, argument.Key);

			if (actionArgumentDescriptor.IsOptional)
				return true;

			return validatorFacoty.GetValidator(actionArgumentDescriptor.ParameterType) == null;
		}

		static HttpParameterDescriptor GetActionArgumentDescriptor(HttpActionContext context, string argumentName)
		{
			return context.ActionDescriptor
				.GetParameters()
				.SingleOrDefault(prm => prm.ParameterName == argumentName);
		}

		static void ValidateArgument(HttpActionContext context, KeyValuePair<string, object> model, IValidatorFactory validatorFactory)
		{
			var validator = validatorFactory.GetValidator(model.Value.GetType());

			if (validator == null)
				return;

			var validationResult = validator.Validate(model.Value);

			if (validationResult.IsValid)
				return;

			validationResult.WriteTo(context.ModelState, model.Key);
		}
	}
}