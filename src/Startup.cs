using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShinCacheTensei.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShinCacheTensei.Services;
using ShinCacheTensei.Data;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace ShinCacheTensei
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;  
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            if (_env.IsDevelopment())
                services.AddDbContext<ShinCacheTenseiContext>(options => options.UseSqlServer
                (@"Server=(localdb)\mssqllocaldb;Database=ShinCacheTensei;Trusted_Connection=True"));
            else
            {
                Environment.GetEnvironmentVariable("DATABASE_URL");
                string postgresUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                var postgresUri = new Uri(postgresUrl);
                string postgresDatabase = postgresUri.LocalPath.Replace("/", "");
                string[] postgresUserInfo = postgresUri.UserInfo.Split(":");

                services.AddDbContext<ShinCacheTenseiContext>(options => options.UseNpgsql
                (
                $"Host = {postgresUri.Host};" +
                $"Database = {postgresDatabase};" +
                $"Port = {postgresUri.Port};" +
                $"User Id = {postgresUserInfo[0]};" +
                $"Password = {postgresUserInfo[1]};" +

                "SSL Mode = Require;" +
                "Trust Server Certificate = true;"
                ));
            }

            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddScoped<IDemonRepository, DemonRepository>();
            services.AddScoped<IDemonService, CacheableDemonService>();
            services.AddScoped<ICacheKeyGeneratorService, CacheKeyGeneratorService>();

            services.AddScoped<IFilterOptionRepository, FilterOptionRepository>();
            services.AddScoped<IFilterOptionsService, FilterOptionsService>();

            services.AddScoped<ShinCacheTenseiContext>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShinCacheTensei", Version = "v1" });
                c.EnableAnnotations();
            });
            services.Configure<RouteOptions>(ro => { ro.LowercaseUrls = true; ro.LowercaseQueryStrings = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Vou manter o Swagger em produção
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShinCacheTensei v1"));
            }
            
            
            //app.UseCors();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
