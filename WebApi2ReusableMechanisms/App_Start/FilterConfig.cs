using System.Web.Mvc;
using WebApi2ReusableMechanisms.Filters.Validation;

namespace WebApi2ReusableMechanisms
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
