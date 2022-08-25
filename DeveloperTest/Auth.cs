using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace DeveloperTest
{
    public class SecretKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "SecretKey";
        public const string HeaderName = "x-api-key";
    }

    public class SecretKeyAuthenticationHandler : AuthenticationHandler<SecretKeyAuthenticationOptions>
    {
        private const string API_KEY = "ApiKey";

        private readonly IConfiguration configuration;

        public SecretKeyAuthenticationHandler(IOptionsMonitor<SecretKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            this.configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(SecretKeyAuthenticationOptions.HeaderName, out var apiKey) || apiKey.Count != 1)
            {
                return AuthenticateResult.Fail("x-api-key header missing or not valid");
            }

            if(configuration.GetValue<string>(API_KEY) != apiKey.Single())
            {
                return AuthenticateResult.Fail("x-api-key header missing or not valid");
            }

            return AuthenticateResult.Success(
                new AuthenticationTicket(
                    new ClaimsPrincipal(new List<ClaimsIdentity>() { new ClaimsIdentity(Array.Empty<Claim>(), SecretKeyAuthenticationOptions.DefaultScheme) }), 
                    SecretKeyAuthenticationOptions.DefaultScheme));
        }
    }
}
