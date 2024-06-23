
using Marten;

var builder = WebApplication.CreateBuilder(args);


// add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database"));
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    //opts.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "Basket_";
});

var app = builder.Build();

// configure the the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
