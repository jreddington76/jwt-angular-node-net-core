using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WebApiJwtExample
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
          options.AddPolicy("AllowOrigins", builder =>
              builder
                  .WithOrigins("*")
                  .WithHeaders("*")
                  .WithMethods("*")
                  .AllowCredentials()));

      services.AddMvc(options =>
      {
        options.Filters.Add(new CorsAuthorizationFilterFactory("AllowOrigins"));
      });

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = "Jwt";
        options.DefaultChallengeScheme = "Jwt";
      }).AddJwtBearer("Jwt", options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateAudience = false,
          //ValidAudience = "the audience you want to validate",
          ValidateIssuer = false,
          //ValidIssuer = "the isser you want to validate",

          ValidateIssuerSigningKey = true,
          //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")),
          IssuerSigningKey = new SymmetricSecurityKey(File.ReadAllBytes("./key/private.key")),

          ValidateLifetime = true, //validate the expiration and not before values in the token

          ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
        };
      });

    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseAuthentication();

      app.UseCors("AllowOrigins");

      app.UseMvcWithDefaultRoute();

      app.Run(async (context) =>
      {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Page not found");
      });
    }
  }
}
