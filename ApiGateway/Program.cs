using ApiGateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Use the authentication Extension for Checking the token
builder.AddAppAuthentication();

//ocelot Configuration
if (builder.Environment.EnvironmentName.ToString().ToLower().Equals("production"))
{
    builder.Configuration.AddJsonFile("ocelot.Production.json", optional: false, reloadOnChange: true);
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
}
builder.Services.AddOcelot(builder.Configuration);

//cors
builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{

    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));


var app = builder.Build();
app.UseCors("policy1");
app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();
app.Run();
