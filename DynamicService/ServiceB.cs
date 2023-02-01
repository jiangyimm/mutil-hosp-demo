public class ServiceB : IService
{
    public string Key => "0102";
    public string GetKey()
    {
        return this.Key;
    }
}