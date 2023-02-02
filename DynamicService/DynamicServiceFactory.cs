using multi_hosp_demo.MultiHosp;

public class DynamicServiceFactory
{
    IMultiHospProvider _multiHospProvider;

    public DynamicServiceFactory(IMultiHospProvider multiHospProvider)
    {
        _multiHospProvider = multiHospProvider;
    }

    public T GetService<T>(IServiceProvider serviceProvider) where T : IService
    {
        var services = serviceProvider.GetServices<T>();
        var service = services.FirstOrDefault(p => p.Key == _multiHospProvider.GetHospCode());
        return service;
    }

    // public IService Accesor(IServiceProvider serviceProvider)
    // {
    //     var hospCode = _multiHospProvider.GetHospCode();
    //     if (hospCode.Equals("0101"))
    //     {
    //         return serviceProvider.GetService<ServiceA>();
    //     }
    //     else if (key.Equals("MultiImpDemo.B"))
    //     {
    //         return implementationFactory.GetService<B.SayHello>();
    //     }
    //     else
    //     {
    //         throw new ArgumentException($"Not Support key : {key}");
    //     }
    // }
}