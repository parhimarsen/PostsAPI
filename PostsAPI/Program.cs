using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PostsAPI.Filters;
using PostsAPI.Interfaces;
using PostsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

builder.Services.AddHttpClient("test", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
    });
}

app.Run();
