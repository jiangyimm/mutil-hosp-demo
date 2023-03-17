using System.ServiceModel;
using multi_hosp_demo.Entities;

[ServiceContract]
public interface ISampleService
{
    [OperationContract]
    string HIPMessageServer(string action, string message);
}

public class SampleService : ISampleService
{
    QcContext _dbContext;

    public SampleService(QcContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string HIPMessageServer(string action, string message)
    {
        //var str = message.ReadAsStringAsync();
        // using var reader = new StreamReader(message);
        // var msg = reader.ReadToEnd();
        return message;
    }
}