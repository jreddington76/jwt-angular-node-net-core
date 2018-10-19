using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using aspnet_core_token_server.Models;
using aspnet_core_token_server.Interfaces;

namespace aspnet_core_token_server.Controllers
{
  [Route("api/auth")]
  public class AuthController : Controller
  {
    private readonly IUserService _userService;
    public AuthController(IUserService userService)
    {
      _userService = userService ?? throw new ArgumentException(nameof(userService));
    }

    // GET api/values
    [HttpPost, Route("login")]
    public IActionResult Login([FromBody]LoginModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest("Invalid client request");
      }

      var user = _userService.GetUser(model.Email, model.Password);
      if (user == null)
      {
        return Unauthorized();
      }

      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

      var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

      var claims = new List<Claim>
      {
          new Claim(ClaimTypes.Name, model.Email)
      };

      user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

      var tokenOptions = new JwtSecurityToken(
          issuer: "http://localhost:5000",
          audience: "http://localhost:5000",
          claims: claims,
          expires: DateTime.Now.AddMinutes(5),
          signingCredentials: signingCredentials
      );

      var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
      return Ok(new { Token = tokenString });
    }
  }
}