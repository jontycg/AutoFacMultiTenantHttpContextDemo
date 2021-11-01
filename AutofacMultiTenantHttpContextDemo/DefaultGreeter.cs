namespace AutofacMultiTenantHttpContextDemo
{
    public class DefaultGreeter : IGreeter
    {
        public string SayGreeting()
        {
            return "Hello Default";
        }
    }
}