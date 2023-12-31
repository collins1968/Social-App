using AuthService.Data;
using AuthService.Extensions;
using AuthService.Services;
using AuthService.Services.IServices;
using AuthService.Utitlity;
using MyMessageBus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthService.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect to database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//set cors policy
builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{
    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));


//Register Services
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();  
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IMessageBus, MessageBus>();
builder.Services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
//Register IdentityFramework
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

//Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//configure JWtOptions 
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddHttpClient("Posts", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:PostsApi"]));

//custom builder services
builder.AddAppAuthentication();
builder.AddSwaggenGenExtension();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    if (!app.Environment.IsDevelopment())
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AUTH API");
        c.RoutePrefix = string.Empty;
    }
});


app.UseHttpsRedirection();

app.UseMigration();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("policy1");

app.Run();
