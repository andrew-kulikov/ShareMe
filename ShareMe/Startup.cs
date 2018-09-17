using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShareMe.Core;
using ShareMe.Core.Models;
using ShareMe.Services;
using ShareMe.Services.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe
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
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddDbContext<ShareMeDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<AspNetUsers, IdentityRole>()
				.AddEntityFrameworkStores<ShareMeDbContext>()
				.AddDefaultTokenProviders();

			services.AddAuthentication()
				.AddCookie(options =>
				{
					options.LoginPath = "/Account/Login/";

				})
				.AddJwtBearer(cfg =>
				{
					cfg.RequireHttpsMetadata = false;
					cfg.SaveToken = true;

					cfg.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidIssuer = Configuration["Tokens:Issuer"],
						ValidAudience = Configuration["Tokens:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
					};

				});

			services.AddLocalization(options => options.ResourcesPath = "Resources");
			services.AddMvc()
				.AddViewLocalization()
				.AddDataAnnotationsLocalization();

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();
			services.AddScoped<IPhotoService, PhotoService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IRatingService, RatingService>();
			services.AddScoped<IFollowingService, FollowingService>();

			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new[]
				{
					new CultureInfo("ru"),
					new CultureInfo("en")
				};

				
				options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");

				options.SupportedCultures = supportedCultures;
	
				options.SupportedUICultures = supportedCultures;
		
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			var supportedCultures = new[]
			{
				new CultureInfo("en"),
				new CultureInfo("ru")
			};

			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});
			

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			CreateUserRoles(services).Wait();
		}

		private async Task CreateUserRoles(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<AspNetUsers>>();

			var roleCheck = await roleManager.RoleExistsAsync("Admin");
			if (!roleCheck)
				await roleManager.CreateAsync(new IdentityRole("Admin"));

			var user = await userManager.FindByEmailAsync("123@gmail.com");

			await userManager.AddToRoleAsync(user, "Admin");
		}
	}
}
