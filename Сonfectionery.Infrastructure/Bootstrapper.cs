using Quartz;
using Quartz.Impl;
using Сonfectionery.Infrastructure.Processing.Quartz.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace Сonfectionery.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection StartQuartz(this IServiceCollection services)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            scheduler.Start().GetAwaiter().GetResult();

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();

            scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult();

            return services;
        }
    }
}