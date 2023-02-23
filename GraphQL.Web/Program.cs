using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.Web.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ProductHttpClient>(o => 
            o.BaseAddress = new Uri(builder.Configuration["GraphApiUri"]));

builder.Services.AddSingleton<ProductGraphClient>();

builder.Services.AddSingleton(t => new GraphQLHttpClient(new GraphQLHttpClientOptions()
{
    EndPoint = new Uri(builder.Configuration["GraphApiUri"]),
    MediaType = "application/json",
}, new NewtonsoftJsonSerializer()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
