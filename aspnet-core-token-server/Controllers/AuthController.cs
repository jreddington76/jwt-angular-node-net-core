using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using aspnet_core_token_server.Models;

namespace aspnet_core_token_server.Controllers
{
  [Route("api/auth")]
  public class AuthController : Controller
  {
    // GET api/values
    [HttpPost, Route("login")]
    public IActionResult Login([FromBody]LoginModel model)
    {
      if (model == null)
      {
        return BadRequest("Invalid client request");
      }

      if (model.Email == "j" && model.Password == "j")
      {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Email),
            new Claim(ClaimTypes.Role, "Manager")
        };

        var tokenOptions = new JwtSecurityToken(
            issuer: "http://localhost:5000",
            audience: "http://localhost:5000",
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return Ok(new { Token = tokenString });
      }
      else
      {
        return Unauthorized();
      }
    }
  }
}