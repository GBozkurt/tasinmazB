using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasinmazz.Business.Abstract.Interfaces;
using tasinmazz.Business.Conrete.Services;
using tasinmazz.DataAccess.Conrete;


namespace tasinmazz
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
			var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
			services.AddControllers();
			string connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));
			services.AddMvc();
			services.AddScoped<TasinmazInterface, TasinmazService>();
			services.AddScoped<MahalleInterface, MahalleService>();
			services.AddScoped<IllerInterface, IlService>();
			services.AddScoped<IlceInterface, IlceService>();
			services.AddSingleton<IConfiguration>(Configuration);
			services.AddScoped<UserInterface, UserService>();
			services.AddScoped<LogInterface, LogService>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
				};
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
			});
			services.AddCors(options =>
			{
				options.AddPolicy("AllowOrigin",
					builder => builder.AllowAnyOrigin()
									  .AllowAnyMethod()
									  .AllowAnyHeader());
			});
			services.AddDbContext<Context>(options =>
			options.UseNpgsql(connectionString));


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseCors("AllowOrigin");
			app.UseAuthorization();
			app.UseSwagger();
			app.UseAuthentication();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("v1/swagger.json", "My API V1");

			});
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
