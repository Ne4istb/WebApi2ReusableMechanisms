using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http.ModelBinding;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace WebApi2ReusableMechanisms.Filters.Validation
{
	public static class ValidationErrorsExtensions
	{
		public static void WriteTo(this ValidationResult validationResult, ModelStateDictionary modelState, string argumentName)
		{
			foreach (var error in validationResult.Errors)
			{
				modelState.AddModelError(CreatePropertyModelName(argumentName, error.PropertyName), error.ErrorMessage);
			}
		}

		public static IDictionary<string, string> GetErrors(this ModelStateDictionary modelState)
		{
			var errors = new Dictionary<string, IEnumerable<string>>();
			foreach (var keyModelStatePair in modelState)
			{
				IEnumerable<string> errorMessages = SerializeModelState(keyModelStatePair.Value);
				if (errorMessages.Any())
					errors.Add(Format(keyModelStatePair.Key), errorMessages);
			}

			return errors.ToDictionary(item => item.Key, item => string.Join("; ", item.Value));
		}

		static string[] SerializeModelState(ModelState model)
		{
			var modelErrors = model.Errors;

			if (modelErrors == null || modelErrors.Count <= 0)
				return new string[0];

			return modelErrors.Select(ToErrorMessage).ToArray();
		}

		static string ToErrorMessage(ModelError error)
		{
			return IsSerializationException(error.Exception)
				? "Invalid type. Please see documentation for correct type"
				: error.ErrorMessage;
		}

		static bool IsSerializationException(Exception exception)
		{
			if (exception == null)
				return false;

			var exceptionType = exception.GetType();

			return exceptionType == typeof(JsonSerializationException)
				|| exceptionType == typeof(JsonReaderException)
				|| exceptionType == typeof(SerializationException);
		}

		static string Format(string modelKey)
		{
			var parts = modelKey.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			return string.Join(".", parts.Select(propertyName => char.ToLower(propertyName[0]) + propertyName.Substring(1)));
		}

		static string CreatePropertyModelName(string prefix, string propertyName)
		{
			if (string.IsNullOrEmpty(prefix))
				return propertyName ?? string.Empty;

			if (string.IsNullOrEmpty(propertyName))
				return prefix;

			return $"{prefix}.{propertyName}";
		}
	}
}