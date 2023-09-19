using System.Reflection;

namespace FactoryPattern.Factories
{
    public static class DifferentDriverFactoryExtension
    {
        public static void AddDriverFactory(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var typesFromAssemblies = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(IDriver)));

            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(IDriver), type, ServiceLifetime.Transient));
            }
            services.AddTransient<IDriver, Car>();
           services.AddTransient<IDriver, Truck>();
           services.AddSingleton<Func<string, IDriver>>(x => type =>
            {
                var drivers = x.GetService<IEnumerable<IDriver>>();
                var driver = drivers.First(d => d.GetType().Name == type);
                return driver;
            });
            services.AddSingleton(typeof(IDriverFactory<>), typeof(DriverFactory<>));

        }
    }
    public class DriverFactory<T> : IDriverFactory<T>
    {
        private readonly Func<string, IDriver> _factory;

        public DriverFactory(Func<string, IDriver> factory)
        {
            _factory = factory;
        }

        public string Start()
        {
            var name = typeof(T).Name;
            return _factory(name).Start();
        }
    }
    public interface IDriverFactory<T>
    {
        string Start();
    }
    public abstract class IDriver
    {
        public abstract string Start();
    }
    public class Car : IDriver
    {
        public string Name { get; set; } = nameof(Car);
        public override string Start() => $"The {nameof(Car)} has been started";
    }
    public class Truck : IDriver
    {
        public string Name { get; set; } = nameof(Truck);
        public override string Start() => $"The {nameof(Truck)} has been started";
    }
}
