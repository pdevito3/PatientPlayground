namespace PatientManagement.FunctionalTests;

using PatientManagement.Databases;
using PatientManagement;
using PatientManagement.Domain.Roles;
using PatientManagement.Domain.Users;
using PatientManagement.SharedTestHelpers.Fakes.User;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
 
public class TestBase
{
    public static IServiceScopeFactory _scopeFactory;
    public static WebApplicationFactory<Program> _factory;
    public static HttpClient _client;

    [SetUp]
    public async Task TestSetUp()
    {
        _factory = new TestingWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions());
        
        // seed root user so tests won't always have user as super admin
        await AddNewSuperAdmin();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<PatientManagementDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<PatientManagementDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PatientManagementDbContext>();

        try
        {
            //await dbContext.BeginTransactionAsync();

            await action(scope.ServiceProvider);

            //await dbContext.CommitTransactionAsync();
        }
        catch (Exception)
        {
            //dbContext.RollbackTransaction();
            throw;
        }
    }

    public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PatientManagementDbContext>();

        try
        {
            //await dbContext.BeginTransactionAsync();

            var result = await action(scope.ServiceProvider);

            //await dbContext.CommitTransactionAsync();

            return result;
        }
        catch (Exception)
        {
            //dbContext.RollbackTransaction();
            throw;
        }
    }

    public static Task ExecuteDbContextAsync(Func<PatientManagementDbContext, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<PatientManagementDbContext>()));

    public static Task ExecuteDbContextAsync(Func<PatientManagementDbContext, ValueTask> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<PatientManagementDbContext>()).AsTask());

    public static Task ExecuteDbContextAsync(Func<PatientManagementDbContext, IMediator, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<PatientManagementDbContext>(), sp.GetService<IMediator>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<PatientManagementDbContext, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<PatientManagementDbContext>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<PatientManagementDbContext, ValueTask<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<PatientManagementDbContext>()).AsTask());

    public static Task<T> ExecuteDbContextAsync<T>(Func<PatientManagementDbContext, IMediator, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<PatientManagementDbContext>(), sp.GetService<IMediator>()));

    public static Task<int> InsertAsync<T>(params T[] entities) where T : class
    {
        return ExecuteDbContextAsync(db =>
        {
            foreach (var entity in entities)
            {
                db.Set<T>().Add(entity);
            }
            return db.SaveChangesAsync();
        });
    }

    public static async Task<User> AddNewSuperAdmin()
    {
        var user = FakeUser.Generate();
        user.AddRole(Role.SuperAdmin());
        await InsertAsync(user);
        return user;
    }

    public static async Task<User> AddNewUser(List<Role> roles)
    {
        var user = FakeUser.Generate();
        foreach (var role in roles)
            user.AddRole(role);
        
        await InsertAsync(user);
        return user;
    }
}