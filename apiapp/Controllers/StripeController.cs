using System;
using System.Diagnostics.Contracts;
using System.Web.Http;

namespace Zoltu.AzureApiApp.Controllers
{
	public class StripeController : ApiController
	{
		[HttpGet]
		[Route("api/Stripe")]
		public String Get()
		{
			Contract.Ensures(Contract.Result<String>() != null);

			return "Hello API App!";
		}
	}
}
