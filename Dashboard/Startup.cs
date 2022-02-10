using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using GraphQL;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using GraphQL.MicrosoftDI;
using Dashboard.Services;
using Dashboard.GraphQL;

namespace Dashboard
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
            //Register all GraphQl types
            services
                .AddScoped<IDocumentExecuter, DocumentExecuter>()
                .AddSingleton<IDocumentWriter, DocumentWriter>()
                .AddScoped<CardType>()
                .AddScoped<CustomerType>()
                .AddScoped<AddressType>()
                .AddScoped<AccountType>()
                .AddScoped<DashboardQuery>()
                .AddScoped<AccountMutation>()
                .AddScoped<ISchema, GraphQLSchema>()
                .AddGraphQL(options =>
                {
                    options.EnableMetrics = true;
                    options.MaxParallelExecutionCount = 10;
                    options.UnhandledExceptionDelegate = e => Console.WriteLine(e.ErrorMessage);
                })
                .AddSystemTextJson(options =>
                {
                    options.PropertyNameCaseInsensitive = true;
                    options.IgnoreNullValues = true;
                    options.WriteIndented = true;
                })
                .AddGraphTypes(typeof(GraphQLSchema), ServiceLifetime.Scoped);

            services.AddControllers();

            //Fetch external service urls
            var cardApiUrl = Configuration.GetValue<string>("CardApiUrl");
            var customerApiUrl = Configuration.GetValue<string>("CustomerApiUrl");

            services.AddScoped<IDashboardServiceProvider>(s => 
                    new DashboardServiceProvider(cardApiUrl, customerApiUrl)
                );

            services.AddSingleton<IAccountServiceProvider, AccountServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //The GraphQl middleware will handle all GraphQl operations on the default paths mentioned below
            app.UseGraphQL<ISchema>();      // Default Path = /graphql
            app.UseGraphQLPlayground();     // Default Path = /ui/playground

            //Uncomment the below statements (after commenting the above 2 stmts) to customize the GraphQl endpoints handled by the middleware
            //app.UseGraphQL<ISchema>("/api/dashboard/graphql");
            //app.UseGraphQLPlayground("/api/dashboard/graphql/ui/playground");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
