using System.Security.Claims;

namespace Breakpoint.API.Middlewares
{
	public class SwaggerAuthMiddleware
	{
		private readonly RequestDelegate _next;

		public SwaggerAuthMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Headers.ContainsKey("X-Swagger"))
			{
				var principal = new ClaimsPrincipal();

				principal.AddIdentity(new ClaimsIdentity(
					new[] {
						new Claim("Permission", "CanViewPage"),
						new Claim("Manager", "yes"),
						new Claim(ClaimTypes.Role, "Administrator"),
						new Claim(ClaimTypes.NameIdentifier, "FakeAuthentication")
					}, "FakeScheme"));

				context.User = principal;
			}

			await _next.Invoke(context);
		}
	}
}
