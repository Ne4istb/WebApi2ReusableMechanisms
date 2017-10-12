using System.Web.Http;
using WebApi2ReusableMechanisms.Filters.Validation;

namespace WebApi2ReusableMechanisms
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			RegisterFilters(config);

			config.MapHttpAttributeRoutes();
		}

		static void RegisterFilters(HttpConfiguration config)
		{
			config.Filters.Add(new ModelValidationFilter());
		}
	}
}
