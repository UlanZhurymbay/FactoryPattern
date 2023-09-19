namespace FactoryPattern.Factories
{
    public static class GenerateWithDataFactoryExtension
    {
        public static void AddGenericClassWithDataFactory(this IServiceCollection services)
        {
            services.AddTransient<IUserData, UserData>();
            services.AddSingleton<Func<IUserData>>(x => () => x.GetRequiredService<IUserData>());
            services.AddSingleton<IUserDataFactory, UserDataFactory>();
        }
    }

    public interface IUserDataFactory
    {
        IUserData GetUserData(string name);
    }
    public class UserDataFactory : IUserDataFactory
    {
        private readonly Func<IUserData> _factory;

        public UserDataFactory(Func<IUserData> factory)
        {
            _factory = factory;
        }

        public IUserData GetUserData(string name)
        {
            var userData = _factory();
            userData.Name = name;
            return userData;
        }
    }
}
