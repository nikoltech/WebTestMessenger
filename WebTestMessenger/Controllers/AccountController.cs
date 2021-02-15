using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebTestMessenger.BusinessLogic.Models;
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

        public IActionResult Index()
        {
            return this.Ok("Works!!!");
        }

        [HttpPost("/token")]
        public async Task<IActionResult> TokenAsync(string login, string password)
        {
            var identity = await this.GetIdentity(login, password).ConfigureAwait(false);
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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                User entity = await this.repo.RegisterAsync(model.ToEntity()).ConfigureAwait(false);

                UserModel resultModel = new UserModel();
                resultModel.ToModel(entity);

                return this.Ok(resultModel);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        #region private methods
        private async  Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            User user = await this.repo.GetUserAsync(login, password).ConfigureAwait(false);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim("UserId", user.Id.ToString()),
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
