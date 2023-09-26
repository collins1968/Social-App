using CommentService.Data;
using CommentService.Extensions;
using CommentService.Services;
using CommentService.Services.IServices;
using Microsoft.EntityFrameworkCore;

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

//register services
builder.Services.AddScoped<ICommentInterface, CommentServices>();

//Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "COMMENT API");
        c.RoutePrefix = string.Empty;
    }
});


app.UseMigration();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseCors("policy1");

app.Run();
