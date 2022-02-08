using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using challenge.Data;
using Microsoft.EntityFrameworkCore;
using challenge.Repositories;
using challenge.Services;
using challenge.Controllers;

namespace code_challenge.Tests.Integration
{
    public class TestServerStartup
    {
        public TestServerStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseInMemoryDatabase("EmployeeDB");
            });
            services.AddDbContext<CompensationContext>(options =>
            {
                options.UseInMemoryDatabase("CompensationDB");
            });
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<EmployeeDataSeeder>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IReportingStructureService, ReportingStructureService>();

            services.AddScoped<ICompensationRepository, CompensationRepository>();
            services.AddTransient<CompensationDataSeeder>();
            services.AddScoped<ICompensationService, CompensationService>();

            services.AddScoped<CompensationContext, CompensationContext>();
            services.AddSingleton<CompensationContext, CompensationContext>();
            services.AddTransient<CompensationContext, CompensationContext>();
            services.AddScoped<CompensationRepository, CompensationRepository>();
            services.AddSingleton<CompensationRepository, CompensationRepository>();
            services.AddTransient<CompensationRepository, CompensationRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, EmployeeDataSeeder seeder, CompensationDataSeeder seeder1)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seeder.Seed().Wait();
                seeder1.Seed().Wait();  
            }
            
            app.UseMvc();

        }
    }
}
