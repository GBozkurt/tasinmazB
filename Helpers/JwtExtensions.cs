using Microsoft.AspNetCore.Http;

namespace tasinmazz.Helpers
{
	public static class JwtExtensions
	{
		public static void AddAplicationError(this HttpResponse response,string message)
		{
			response.Headers.Add("Application-Error", message);
			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
		}
	}
}
