using Alura.ListaLeitura.Seguranca;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Alura.WebAPI.WebApp.Formatters;
using System;
using Alura.ListaLeitura.HttpClients;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Alura.ListaLeitura.WebApp
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration config)
		{
			Configuration = config;
		}

		public void ConfigureServices(IServiceCollection services)
		{

			services.AddHttpContextAccessor();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(Options =>
				{
                    Options.LoginPath ="/Usuario/Login";
				});

			services.AddHttpClient<LivroApiClient>(client =>
			{
				client.BaseAddress = new Uri("http://localhost:6001/Api/V1.0/");
			});

			services.AddHttpClient<AuthApiClient>(client =>
			{
				client.BaseAddress = new Uri("http://localhost:5000/Api/");
			});

			

			services.AddMvc(options =>
			{
				options.OutputFormatters.Add(new LivroCsvFormatter());
			}).AddXmlSerializerFormatters();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
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
     
	



	  

