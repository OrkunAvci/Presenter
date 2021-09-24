using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presenter.Data;

namespace Presenter
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; set; }

		public Startup(IWebHostEnvironment env)
		{
			//	Custom builder.
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//	Define user exclusive folders/pages.
			services.AddRazorPages(options =>
			{
				options.Conventions.AuthorizeFolder("/Dashboard", "Cookie");
				options.Conventions.AuthorizePage("/Screens/Create", "Cookie");
				options.Conventions.AuthorizePage("/Screens/Edit", "Cookie");
				options.Conventions.AuthorizePage("/Screens/Delete", "Cookie");
				options.Conventions.AuthorizePage("/Screens/Details", "Cookie");
			}).AddRazorRuntimeCompilation();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
			services.AddOptions();

			//	Needed to access Controller properties in cshtml files.
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//	Cookie authentication.
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/Login";
					options.LogoutPath = "/Logout";
					options.ExpireTimeSpan = DateTime.UtcNow.AddDays(30) - DateTime.Now;
					options.ReturnUrlParameter = "ReturnUrl";
				});

			//	Custom policy to enforce "auth" requirement.
			services.AddAuthorization(options =>
			{
				options.AddPolicy("Cookie", authBuilder =>
				{
					authBuilder.RequireClaim("auth");
				});
			});

			//	TODO: Configure to your environemnt. Check appsettings.json.
			//	Database connection.
			string conn_str = Configuration.GetConnectionString("MySQL");
			services.AddDbContext<UserContext>(options => options.UseMySql(conn_str, ServerVersion.AutoDetect(conn_str)));
			services.AddDbContext<ScreenContext>(options => options.UseMySql(conn_str, ServerVersion.AutoDetect(conn_str)));
			services.AddDbContext<ImagesContext>(options => options.UseMySql(conn_str, ServerVersion.AutoDetect(conn_str)));

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				//	DO NOT use this in release. Might leak/expose sensitive data.
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapRazorPages();
			});
		}
	}
}
