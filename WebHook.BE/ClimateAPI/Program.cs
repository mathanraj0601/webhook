using ClimateAPI.Data;
using ClimateAPI.Data.Context;
using ClimateAPI.Mapper;
using ClimateAPI.MessageBus;
using ClimateAPI.MiddleWare;
using ClimateAPI.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ClimateContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));

//Middleware
builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();

// Mapper
builder.Services.AddAutoMapper(typeof(ClimateMapper));
builder.Services.AddAutoMapper(typeof(SubscribeMapper));

// Repositories
builder.Services.AddScoped<IRepo<Climate,int>,ClimateRepo>();
builder.Services.AddScoped<IRepo<Subscriber, int>, SubscriberRepo>();

// Message bus
builder.Services.AddScoped<IMessageBus, MessageBus>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCors");
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
