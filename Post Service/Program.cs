using Microsoft.EntityFrameworkCore;
using Post_Service.Data;
using Post_Service.Extensions;
using Post_Service.Services;
using Post_Service.Services.IServices;
using Post_Service.Utility;

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

//register services
builder.Services.AddScoped<IPostInterface, PostService>();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();

//Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//register http client
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("Comments", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:CommentsApi"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

//custom builder services
builder.AddAppAuthentication();
builder.AddSwaggenGenExtension();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();
