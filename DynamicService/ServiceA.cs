public class ServiceA : IService
{
    public string Key => "0101";
    public string GetKey()
    {
        return this.Key;
    }
}