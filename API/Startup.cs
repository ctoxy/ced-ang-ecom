using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            
            //appel de automapper pour transformer la response client issue de la base
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            
            //appel du service StoreContext action vers la base apinet db
            services.AddDbContext<StoreContext>(x =>
                 x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            
            // appel de ApplicationServicesExtensions
            services.AddApplicationServices();
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
            //appel du middleware CORS
            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            
            // appel de swagger documentation
            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
