namespace FactoryPattern.Services
{
    public interface ISample1
    {
        DateTime GetDateTime();
    }

    public class Sample1 : ISample1
    {
        public DateTime GetDateTime() => DateTime.Now;
    }

}
