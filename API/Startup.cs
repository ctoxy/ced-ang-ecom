using System.IO;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // for dev purpose
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            //appel du service StoreContext action vers la base apinet db
            services.AddDbContext<StoreContext>(x =>
                 x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            
            //appel du service Identity pour la gestion de la base users login
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseSqlite(_config.GetConnectionString("IdentityConnection"));
            });

            ConfigureServices(services);
        }

        // for prod purpose
        public void ConfigureProductionServices(IServiceCollection services)
        {
            //appel du service StoreContext action vers la base apinet db
            services.AddDbContext<StoreContext>(x =>
                 x.UseMySql(_config.GetConnectionString("DefaultConnection")));
            
            //appel du service Identity pour la gestion de la base users login
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseMySql(_config.GetConnectionString("IdentityConnection"));
            });

            ConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {   
            
            //appel de automapper pour transformer la response client issue de la base
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            
            
                 
            // appel de Redis for the basket
            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions
                    .Parse(_config.GetConnectionString("Redis"),
                true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            // appel de ApplicationServicesExtensions
            services.AddApplicationServices();
            // appel de IdentityServiceExtensions
            services.AddIdentityServices(_config);
            // appel de SwaggerServiceExtension
            services.AddSwaggerDocumentation();
            // appel de cors pour accés api via navigateur
            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            // servir les fichiers statique comme les images
            app.UseStaticFiles();
            // appel du dossier content des images du site front
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")
                ), RequestPath = "/content"
            });
            //appel du middleware CORS
            app.UseCors("CorsPolicy");
            // appel de l authentification pour le token avant useAuthorization position importante
            app.UseAuthentication();
            
            app.UseAuthorization();
            
            // appel de swagger documentation
            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
