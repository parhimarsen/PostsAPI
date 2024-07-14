using Microsoft.AspNetCore.Builder;
using PostsAPI.Filters;
using PostsAPI.Interfaces;
using PostsAPI.Services;
using static System.Net.Mime.MediaTypeNames;

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
    // Другие провайдеры логирования...
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

app.Run();
