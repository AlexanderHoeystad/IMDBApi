using IMDBApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(option =>
{
    option.AddPolicy(name: "AllowAll",
              builder =>
              {
                  builder.AllowAnyOrigin();
                  builder.AllowAnyMethod();
                  builder.AllowAnyHeader();
              });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TitleRepoDB>();
builder.Services.AddScoped<NameRepo>();
builder.Services.AddScoped<CrewRepoDB>();


builder.Services.AddDbContext<IMDBDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IMDBDbContext"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllers();

app.Run();
