using AgentAPI.Data;
using AgentAPI.Mapper;
using AgentAPI.MiddleWare;
using AgentAPI.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Context
builder.Services.AddDbContext<AgentContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConn"));
});

// Repos
builder.Services.AddScoped<IRepo<WebHook, int>, WebhookRepo>();


// Mappers
builder.Services.AddAutoMapper(typeof(WebHookMapper));


// Middlewares
builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
