using System.ServiceModel;

[ServiceContract]
public interface ISampleService
{
    [OperationContract]
    string Test(string s);
}

public class SampleService : ISampleService
{
    public string Test(string s)
    {
        Console.WriteLine("Test Method Executed!");
        return s;
    }
}