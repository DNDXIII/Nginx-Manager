using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Threading.Tasks;
using WebApplication1.Common;
using WebApplication1.DataAccess;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;

namespace WebApplication1.Controllers
{
    [EnableCors("Cors")]
    [Route("api/jwt")]
    public class JwtController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IRepository<User> _users;

        public JwtController(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory, UserDataAccess users)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
            _users = users;
            _logger = loggerFactory.CreateLogger<JwtController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromBody] User applicationUser)
        {
            var identity = await GetClaimsIdentity(applicationUser);
            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({applicationUser.Username}) or password ({applicationUser.Password})");
                return BadRequest("Invalid credentials");
            }

            

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private Task<GenericIdentity> GetClaimsIdentity(User user)
        {
            User us = ((List<User>)_users.GetAll()).FirstOrDefault(u => u.Username == user.Username);

            if (us != null)
            {
                var hashedPass = Hash.ComputeHash(user.Password, us.Salt);
                if (us.Password == hashedPass)
                {
                    return Task.FromResult(new GenericIdentity(user.Username, "Token"));
                }
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<GenericIdentity>(null);
        }
    }
}
