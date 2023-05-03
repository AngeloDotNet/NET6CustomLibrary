namespace NET6CustomLibrary.Scrutor;

public static class ScrutorService
{
    public static IServiceCollection AddRegisterTransientService<TAssembly>(this IServiceCollection services, string stringEndsWith) where TAssembly : class
    {
        services.Scan(scan =>
            scan.FromAssemblyOf<TAssembly>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith(stringEndsWith)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }

    public static IServiceCollection AddRegisterSingletonService<TAssembly>(this IServiceCollection services, string stringEndsWith) where TAssembly : class
    {
        services.Scan(scan =>
            scan.FromAssemblyOf<TAssembly>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith(stringEndsWith)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

        return services;
    }

    public static IServiceCollection AddRegisterScopedService<TAssembly>(this IServiceCollection services, string stringEndsWith) where TAssembly : class
    {
        services.Scan(scan =>
            scan.FromAssemblyOf<TAssembly>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith(stringEndsWith)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
}