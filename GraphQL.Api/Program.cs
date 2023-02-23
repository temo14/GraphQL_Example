using GraphQL;
using GraphQL.Api.Data;
using GraphQL.Api.GraphQL;
using GraphQL.Api.Repository;
using GraphQL.Server.Ui.Playground;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContextConnection")));

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductReviewRepository>();
builder.Services.AddScoped<GraphSchema>();

builder.Services.AddGraphQL(o => o
    .AddGraphTypes()
    .AddUserContextBuilder(httpContext =>
    {
        var userContext = new Dictionary<string, object?>
            {
                { "user", httpContext.User }
            };

        return userContext;
    })
    .AddDataLoader()
    .AddSystemTextJson());

var app = builder.Build();

app.UseGraphQL<GraphSchema>();

app.UseGraphQLPlayground(
    "/ui/graphql",
    new PlaygroundOptions
    {
        GraphQLEndPoint = "/graphql",
        SubscriptionsEndPoint = "/graphql",
        RequestCredentials = RequestCredentials.Include,
    });

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
dbContext!.Database.EnsureCreated();
dbContext.Seed();

app.Run();
