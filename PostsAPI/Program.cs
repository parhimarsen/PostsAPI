using PostsAPI.Interfaces;
using PostsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();

builder.Services.AddControllers();

builder.Services.AddHttpClient("test", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
