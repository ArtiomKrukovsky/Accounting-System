using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Сonfectionery.API.Application.Modules;
using Сonfectionery.Infrastructure.Processing.Outbox;
using Сonfectionery.Infrastructure.Processing.Quartz.Jobs;
using Сonfectionery.Services.Configurations;

namespace Сonfectionery.API.Extensions
{
    public static class AutofacExtensions
    {
        private const string DefaultConnectionString = "DefaultConnection";

        public static IServiceProvider CreateAutofacServiceProvider(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var container = new ContainerBuilder();

            container.Populate(services);

            var kSqlDbConfig = new KSqlDbConfig();
            configuration.Bind(KSqlDbConfig.KSqlDbConfiguration, kSqlDbConfig);

            var kafkaConfig = new KafkaConfig();
            configuration.Bind(KafkaConfig.KafkaConfiguration, kafkaConfig);

            container.RegisterModule(new ProgramModule());
            container.RegisterModule(new KafkaModule(kafkaConfig));
            container.RegisterModule(new KSqlDbModule(kSqlDbConfig));
            container.RegisterModule(new MediatorModule());

            container.RegisterModule(new QuartzModule());
            container.RegisterModule(new ProcessingModule());

            var connectionString = configuration.GetConnectionString(DefaultConnectionString);
            container.RegisterModule(new InfrastructureModule(connectionString));

            return new AutofacServiceProvider(container.Build());
        }

        public static void StartQuartz(
            this IServiceCollection serviceCollection, 
            IConfiguration configuration)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            var container = new ContainerBuilder();

            var kSqlDbConfig = new KSqlDbConfig();
            configuration.Bind(KSqlDbConfig.KSqlDbConfiguration, kSqlDbConfig);

            var kafkaConfig = new KafkaConfig();
            configuration.Bind(KafkaConfig.KafkaConfiguration, kafkaConfig);

            container.RegisterModule(new ProgramModule());
            container.RegisterModule(new KafkaModule(kafkaConfig));
            container.RegisterModule(new KSqlDbModule(kSqlDbConfig));
            container.RegisterModule(new MediatorModule());

            container.RegisterModule(new QuartzModule());
            container.RegisterModule(new ProcessingModule());

            var connectionString = configuration.GetConnectionString(DefaultConnectionString);
            container.RegisterModule(new InfrastructureModule(connectionString));

            scheduler.JobFactory = new JobFactory(container.Build());

            scheduler.Start().GetAwaiter().GetResult();

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();

            scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult();
        }
    }
}