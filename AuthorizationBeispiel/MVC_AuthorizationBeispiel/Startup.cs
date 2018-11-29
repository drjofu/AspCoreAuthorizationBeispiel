using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mondial;
using MVC_AuthorizationBeispiel.Models;
using System.IO;

namespace MVC_AuthorizationBeispiel
{
  public class Startup
  {
    private readonly IHostingEnvironment env;

    public Startup(IConfiguration configuration, IHostingEnvironment env)
    {
      Configuration = configuration;
      this.env = env;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddSingleton(new World(Path.Combine(env.ContentRootPath, "data/mondial.xml")));
      services.AddSingleton<UserRepo>();

      services.AddMvc(config =>
      {
        var policy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
        config.Filters.Add(new AuthorizeFilter(policy));
      })
      .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);



      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
          options.LoginPath = new PathString("/Account/Login/");
          options.AccessDeniedPath = new PathString("/Account/Forbidden/");
        });

      services.AddAuthorization(options =>
      {
        options.AddPolicy("AdministratorOnly", policy => policy.RequireRole("Admin"));
        options.AddPolicy("HasChildrenSet", policy => policy.RequireClaim("HasChildren", "True"));
        options.AddPolicy("BornBefore1970", policy => policy.Requirements.Add(new BornBefore1970Requirement()));
      });

      services.AddSingleton<IAuthorizationHandler, BornBefore1970Handler>();
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
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
