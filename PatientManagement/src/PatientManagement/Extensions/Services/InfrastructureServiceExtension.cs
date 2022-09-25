namespace PatientManagement.Extensions.Services;

using PatientManagement.Databases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HeimGuard;
using PatientManagement.Services;
using PatientManagement.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IWebHostEnvironment env)
    {
        // DbContext -- Do Not Delete
        if (env.IsEnvironment(Consts.Testing.FunctionalTestingEnvName))
        {
            services.AddDbContext<PatientManagementDbContext>(options =>
                options.UseInMemoryDatabase($"PatientManagement"));
        }
        else
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if(string.IsNullOrEmpty(connectionString))
            {
                // this makes local migrations easier to manage. feel free to refactor if desired.
                connectionString = env.IsDevelopment() 
                    ? "Host=localhost;Port=49269;Database=dev_patientmanagement;Username=postgres;Password=postgres"
                    : throw new Exception("DB_CONNECTION_STRING environment variable is not set.");
            }

            services.AddDbContext<PatientManagementDbContext>(options =>
                options.UseNpgsql(connectionString,
                    builder => builder.MigrationsAssembly(typeof(PatientManagementDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());
        }

        // Auth -- Do Not Delete
        if (!env.IsEnvironment(Consts.Testing.FunctionalTestingEnvName))
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Environment.GetEnvironmentVariable("AUTH_AUTHORITY");
                    options.Audience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE");
                    options.RequireHttpsMetadata = !env.IsDevelopment();
                });
        }

        services.AddAuthorization(options =>
        {
        });

        services.AddHeimGuard<UserPolicyHandler>()
            .MapAuthorizationPolicies()
            .AutomaticallyCheckPermissions();
    }
}
