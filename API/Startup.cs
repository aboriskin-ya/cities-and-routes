using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Repository.Storage;
using Service;
using Service.Services;
using Service.Services.Interfaces;
using System;

namespace API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            string connection = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CityRouteContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.AddScoped(typeof(IMapRepository), typeof(MapRepository));
            services.AddScoped(typeof(IImageRepository), typeof(ImageRepository));
            services.AddScoped(typeof(ISettingsRepository), typeof(SettingsRepository));
            services.AddScoped(typeof(ICityRepository), typeof(CityRepository));

            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IMapService, MapService>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IAlgorithmService, AlgorithmService>();
            services.AddTransient<ITravelSalesmanAnnealingResolver, TravelSalesmanAnnealingResolver>();
            services.AddTransient<IShortestPathResolverService, ShortestPathResolverService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IPathToGraphService, PathToGraphService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
