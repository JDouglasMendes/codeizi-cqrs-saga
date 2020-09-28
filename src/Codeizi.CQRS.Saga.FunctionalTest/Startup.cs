using Codeizi.CQRS.Saga.Services;
using Codeizi.DI.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codeizi.CQRS.Saga.FunctionalTest
{
    public class Startup
    {
        public static readonly ILoggerFactory MyLoggerFactory
     = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCQRSSaga(Configuration,
                                options =>
                                options.UseLoggerFactory(MyLoggerFactory).
                                    UseSqlServer(
                                        Configuration.GetConnectionString("DefaultConnection")));

            services.AddInjectables(GetType().Assembly);
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}