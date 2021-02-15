using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebTestMessenger.DataAccess.Entities;
using WebTestMessenger.DataAccess.Repositories;
using WebTestMessenger.Infrastructure;

namespace WebTestMessenger.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository repo;

        public AccountController(IRepository repository)
        {
            this.repo = repository;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> TokenAsync(string username, string password)
        {
            var identity = await GetIdentity(username, password).ConfigureAwait(false);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        #region private methods
        private async  Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            User user = await this.repo.GetUserAsync(login, password).ConfigureAwait(false);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
        #endregion
    }
}
