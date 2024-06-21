

var builder = WebApplication.CreateBuilder(args);




var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{ 
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));

});

builder.Services.AddValidatorsFromAssemblies(new[] { assembly });
builder.Services.AddCarter();
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database"));
    //opts.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
