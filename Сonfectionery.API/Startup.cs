using System.Reflection;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Сonfectionery.API.Application.Behaviors;
using Сonfectionery.API.Extensions;
using Сonfectionery.Domain.Seedwork;
using Сonfectionery.Services;
using Сonfectionery.Services.Configurations;

namespace Сonfectionery.API
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
            services.AddControllers();

            // Configure DB
            services.AddCustomDbContext(Configuration);

            // Get KSqlDB configuration
            var kSqlDbConfig = new KSqlDbConfig();
            Configuration.Bind(KSqlDbConfig.KSqlDbConfiguration, kSqlDbConfig);

            // Configure KSqlDB
            services.AddKSqlDb(p =>
            {
                p.BaseUrl = kSqlDbConfig.BaseUrl;
                p.Subscription = kSqlDbConfig.Subscription;
                p.ShouldPluralizeFromItemName = kSqlDbConfig.ShouldPluralizeFromItemName;
            });

            // Get Kafka configuration
            var kafkaConfig = new KafkaConfig();
            Configuration.Bind(KafkaConfig.KafkaConfiguration, kafkaConfig);

            // Configure Kafka
            services.AddKafkaProducer(p =>
            {
                p.Topic = kafkaConfig.Topic;
                p.BootstrapServers = kafkaConfig.BootstrapServers;
            });

            // Configure Mapster
            var config = new TypeAdapterConfig { RequireExplicitMapping = true };
            config.Scan(Assembly.GetExecutingAssembly());
            config.Compile(); // validate mappings
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            // Configure MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // Configure Domain Events
            //TODO: Register events

            // Configure Repositories
            services.AddRepositories();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

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
