using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Invoice.API.Handlers
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public ApiKeyAuthenticationOptions()
        {
        }
    }

    internal class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string _Scheme = "ApiKeyAuthScheme";

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string apiKey = Request.Headers["ApiKey"];

            // only for test needs, shold be stored in DB
            if (apiKey != "xy1234")
                return AuthenticateResult.Fail(new Exception("Invalid API key."));
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Anonymous, apiKey)
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name));
            var ticket = new AuthenticationTicket(claimsPrincipal,
                new AuthenticationProperties { IsPersistent = false },
                Scheme.Name
            );

            return AuthenticateResult.Success(ticket);
        }
    }
}
