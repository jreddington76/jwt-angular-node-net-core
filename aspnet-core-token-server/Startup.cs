using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using aspnet_core_token_server.Services;
using aspnet_core_token_server.Interfaces;

namespace aspnet_core_token_server
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
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
      }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddTransient<IUserService, UserService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseCors("AllowOrigins");

      // add some secutiry headers (recommendations from securityheaders.io)
      app.Use((context, next) =>
      {
        context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
        context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
        context.Response.Headers["X-Content-Type-Options"] = "nosniff";
        context.Response.Headers["Content-Security-Policy"] = "script-src 'self'";
        context.Response.Headers["Referrer-Policy"] = "no-referrer-when-downgrade";
        return next.Invoke();
      });

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
