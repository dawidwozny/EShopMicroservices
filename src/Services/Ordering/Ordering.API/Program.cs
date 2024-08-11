using Ordering.Application;
using Ordering.Infrastracture;
using Ordering.API;
using Ordering.Infrastructure.Data.Extensions; 

var builder = WebApplication.CreateBuilder(args);



// add services to the container
builder.Services
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration)
                .AddApiServices(builder.Configuration);


var app = builder.Build();

// configure http pipeline
app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
