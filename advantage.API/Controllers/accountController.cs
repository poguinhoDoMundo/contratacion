using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using advantage.API.Models;

namespace WebApiPaises.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {

        [HttpPost]
        [Route("Login")]
        public async  Task<IActionResult> Login([FromBody] Account account )
        {


            (bool result, string msg) = await Account.is_user( account )  ;
                if (result)
                {
                    return Ok(BuildToken( account ));
                }
                else
                {
                    if (  msg == "OK" )
                        return Ok( Json("Usuario y/o contraseña incorrectos !!!") );

                    return Ok( Json(msg)  );
                }
        }

        private IActionResult BuildToken( Account account )
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, account.user ),
                new Claim("id_user", "1"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( Environment.GetEnvironmentVariable("secretKey")  ));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });
        }
    }
}