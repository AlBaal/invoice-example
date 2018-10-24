using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Invoice.BL.Interfaces;
using Invoice.DataAccess;
using Invoice.DataAccess.Services;
using Invoice.WebApp.Mappers;
using Invoice.WebApp.Validators;
using Invoice.WebApp.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice.WebApp
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
            var connection = @"Server=(localdb)\mssqllocaldb;Database=InvoiceDB;Trusted_Connection=True;";
            services.AddDbContext<InvoiceDbContext>(options => options.UseSqlServer(connection));
            services.AddMvc().AddFluentValidation();

            // Data access services
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceItemService, InvoiceItemService>();
            services.AddTransient<IPartnerService, PartnerService>();

            // DB initializer
            services.AddTransient<InvoiceDbInitializer>();

            // Mappers
            services.AddSingleton<InvoicesViewModelMapper>();
            services.AddSingleton<InvoiceItemsViewModelMapper>();

            // Validators
            services.AddSingleton<IValidator<InvoicesViewModel>, InvoiceEditValidator>();
            services.AddSingleton<IValidator<InvoicesViewModel>, InvoiceCreateValidator>();
            services.AddSingleton<IValidator<InvoiceItemsViewModel>, InvoiceItemCreateValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, InvoiceDbInitializer invoiceDbInitializer, InvoiceDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Invoices}/{action=Index}/{id?}");
            });

            if (dbContext.Database.EnsureCreated())
            {
                invoiceDbInitializer.Seed().Wait();
            }
        }
    }
}
