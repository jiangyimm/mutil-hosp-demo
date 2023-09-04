namespace FastEndpoints;

public class ServiceResolver
{
    private static IServiceProvider rootProvider;

    public static void SetRootProvider(IServiceProvider serviceProvider)
    {
        rootProvider = serviceProvider;
    }
    public static T GetService<T>()
    {
        return rootProvider.CreateScope().ServiceProvider.GetService<T>();
    }
}