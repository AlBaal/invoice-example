using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.EntityFramework;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Invoice.BL.Interfaces;
using Invoice.DataAccess;
using Invoice.DataAccess.Services;
using Invoice.GraphQL.Graphs;
using Invoice.GraphQL.Interfaces;
using Invoice.GraphQL.Mutations;
using Invoice.GraphQL.Queries;
using Invoice.GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Invoice.GraphQL
{
    public class Startup
    {
        private const string _sqlConnection = @"Server=(localdb)\mssqllocaldb;Database=InvoiceDB;Trusted_Connection=True;";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds Invoice DB context
            services.AddDbContext<InvoiceDbContext>(options => options.UseSqlServer(_sqlConnection));

            var sp = services.BuildServiceProvider();
            using (var myDataContext = sp.GetService<InvoiceDbContext>())
            {
                EfGraphQLConventions.RegisterInContainer(services, myDataContext);
            }
            EfGraphQLConventions.RegisterConnectionTypesInContainer(services);


            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<InvoiceQuery>();
            services.AddSingleton<InvoiceQueryEF>();
            services.AddSingleton<InvoiceMutation>();
            services.AddSingleton<InvoiceType>();
            services.AddSingleton<InvoiceInputType>();
            services.AddSingleton<InvoiceInterface>();
            services.AddSingleton<ISchema, InvoiceSchema>();

            foreach (var type in GetGraphQlTypes())
            {
                services.AddSingleton(type);
            }

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            })
            .AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User });

            // Data access services
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceItemService, InvoiceItemService>();
            services.AddTransient<IPartnerService, PartnerService>();

            // DB initializer
            services.AddTransient<InvoiceDbInitializer>();
        }

        static IEnumerable<Type> GetGraphQlTypes()
        {
            return typeof(Startup).Assembly
                .GetTypes()
                .Where(x => !x.IsAbstract && (typeof(IObjectGraphType).IsAssignableFrom(x) || typeof(IInputObjectGraphType).IsAssignableFrom(x)));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();

            // add http for Schema at default url /graphql
            app.UseGraphQL<ISchema>("/graphql");

            // use graphql-playground at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });
        }
    }
}
