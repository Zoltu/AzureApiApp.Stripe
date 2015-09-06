using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Zoltu.AzureApiApp.Controllers
{
	public class StripeController : ApiController
	{
		[HttpGet]
		[Route("api/CreateCustomer")]
		public async Task<String> CreateCustomer(String cardTokenId, String stripeApiKey)
		{
			Contract.Requires(cardTokenId != null);
			Contract.Requires(stripeApiKey != null);
			Contract.Ensures(Contract.Result<String>() != null);
			Contract.Ensures(Contract.Result<Task<String>>() != null);

			var address = @"https://api.stripe.com/v1/customers";
			var authorizationHeader = GetBasicAuthenticationHeader(stripeApiKey, "");
			var content = new Dictionary<String, String> { { "source", cardTokenId } };
			var formContent = new FormUrlEncodedContent(content);

			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = authorizationHeader;
			var result = await httpClient.PostAsync(address, formContent);
			if (!result.IsSuccessStatusCode)
				throw new HttpResponseException(result);

			return await result.Content.ReadAsStringAsync();
		}

		private static AuthenticationHeaderValue GetBasicAuthenticationHeader(String username, String password)
		{
			Contract.Requires(username != null);
			Contract.Requires(password != null);
			Contract.Ensures(Contract.Result<AuthenticationHeaderValue>() != null);

			var content = $"{username}:{password}";
			var contentBytes = Encoding.ASCII.GetBytes(content);
			var contentBytesEncoded = Convert.ToBase64String(contentBytes);
			return new AuthenticationHeaderValue("Basic", contentBytesEncoded);
		}
	}
}
