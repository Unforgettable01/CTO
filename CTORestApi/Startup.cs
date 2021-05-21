using CTOBusinessLogic.BusinessLogic;
using CTOBusinessLogic.Interfaces;
using CTODatabaseImplement.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTORestApi
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
            services.AddTransient<IClientStorage, ClientStorage>();
            services.AddTransient<IWorkerStorage, WorkerStorage>();
            services.AddTransient<IWorkStorage, WorkStorage>();
            services.AddTransient<IRequestStorage, RequestStorage>();
            services.AddTransient<IPaymentStorage, PaymentStorage>();
            services.AddTransient<ICostStorage, CostStorage>();
            services.AddTransient<ClientLogic>();
            services.AddTransient<WorkerLogic>();
            services.AddTransient<WorkLogic>();
            services.AddTransient<RequestLogic>();
            services.AddTransient<PaymentLogic>();
            services.AddTransient<CostLogic>();
            services.AddControllers();
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
