using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FastEndpoints;

/// <summary>
/// extension methods for job queues
/// </summary>
public static class JobQueueExtensions
{
    private static Type tStorageRecord;
    private static Type tStorageProvider;

    /// <summary>
    /// add job queue functionality
    /// </summary>
    /// <typeparam name="TStorageRecord">the implementation type of the job storage record</typeparam>
    /// <typeparam name="TStorageProvider">the implementation type of the job storage provider</typeparam>
    public static IServiceCollection AddJobQueues<TStorageRecord, TStorageProvider>(this IServiceCollection svc)
        where TStorageRecord : IJobStorageRecord, new()
        where TStorageProvider : class, IJobStorageProvider<TStorageRecord>
    {
        tStorageProvider = typeof(TStorageProvider);
        tStorageRecord = typeof(TStorageRecord);
        svc.AddSingleton<TStorageProvider>();
        svc.AddSingleton(typeof(JobQueue<,,>));
        svc.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly(), typeof(IJobHandler<>));

        return svc;
    }

    private static void AddClassesAsImplementedInterface(
       this IServiceCollection services,
       Assembly assembly,
       Type compareType,
       ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        assembly.GetTypesAssignableTo(compareType).ForEach((type) =>
        {
            foreach (var implementedInterface in type.ImplementedInterfaces)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Scoped:
                        services.AddScoped(implementedInterface, type);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(implementedInterface, type);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(implementedInterface, type);
                        break;
                }
            }
        });
    }

    private static List<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
    {
        var typeInfoList = assembly.DefinedTypes.Where(x => x.IsClass
                            && !x.IsAbstract
                            && x != compareType
                            && x.GetInterfaces()
                                    .Any(i => i.IsGenericType
                                            && i.GetGenericTypeDefinition() == compareType))?.ToList();

        return typeInfoList;
    }

    /// <summary>
    /// enable job queue functionality with given settings
    /// </summary>
    /// <param name="options">specify settings/execution limits for each job queue type</param>
    /// <exception cref="InvalidOperationException">thrown when no commands/handlers have been detected</exception>
    public static IApplicationBuilder UseJobQueues(this IApplicationBuilder app, Action<JobQueueOptions>? options = null)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        var jobTypes = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IJob)));
        //var registry = app.ApplicationServices.GetRequiredService<CommandHandlerRegistry>();

        if (!jobTypes.Any())
            throw new InvalidOperationException("No Commands/Handlers found in the system! Have you called AddFastEndpoints() yet?");

        var opts = new JobQueueOptions();
        options?.Invoke(opts);

        foreach (var tCommand in jobTypes)
        {
            var tJobQ = typeof(JobQueue<,,>).MakeGenericType(tCommand, tStorageRecord, tStorageProvider);
            var jobQ = app.ApplicationServices.GetRequiredService(tJobQ);
            opts.SetExecutionLimits(tCommand, (JobQueueBase)jobQ);
        }

        ServiceResolver.SetRootProvider(app.ApplicationServices);

        return app;
    }

    /// <summary>
    /// queues up a given command in the respective job queue for that command type.
    /// </summary>
    /// <param name="cmd">the command to be queued</param>
    /// <param name="executeAfter">if set, the job won't be executed before this date/time. if unspecified, execution is attempted as soon as possible.</param>
    /// <param name="expireOn">if set, job will be considered stale/expired after this date/time. if unspecified, jobs expire after 4 hours of creation.</param>
    /// <param name="ct">cancellation token</param>
    public static Task QueueJobAsync(this IJob cmd, DateTime? executeAfter = null, DateTime? expireOn = null, CancellationToken ct = default)
        => JobQueueBase.AddToQueueAsync(cmd, executeAfter, expireOn, ct);
}