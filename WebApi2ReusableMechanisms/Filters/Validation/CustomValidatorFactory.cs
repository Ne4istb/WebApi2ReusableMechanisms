using System;
using System.Web.Http.Dependencies;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Internal;

namespace WebApi2ReusableMechanisms.Filters.Validation
{
	internal class CustomValidatorFactory : IValidatorFactory
	{
		readonly InstanceCache cache = new InstanceCache();
		readonly IDependencyScope dependencyScope;

		public CustomValidatorFactory(IDependencyScope dependencyScope)
		{
			this.dependencyScope = dependencyScope;
		}

		public IValidator<T> GetValidator<T>()
		{
			return (IValidator<T>)GetValidator(typeof(T));
		}

		public virtual IValidator GetValidator(Type type)
		{
			if (type == null)
				return null;

			var validatorAttribute = (ValidatorAttribute)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute));

			if (validatorAttribute == null || validatorAttribute.ValidatorType == null)
				return null;

			return cache.GetOrCreateInstance(validatorAttribute.ValidatorType, ValidatorFactory) as IValidator;
		}

		IValidator ValidatorFactory(Type validatorType)
		{

			var validator = (dependencyScope.GetService(validatorType)
				?? Activator.CreateInstance(validatorType)) as IValidator;

			return validator;
		}
	}
}