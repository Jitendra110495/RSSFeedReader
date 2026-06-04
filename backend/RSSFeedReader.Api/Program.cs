using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.Api.Services;
using RSSFeedReader.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5213")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddSingleton<SubscriptionService>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.Use(async (context, next) =>
{
    if (context.Request.Path.Equals("/api/subscriptions", StringComparison.OrdinalIgnoreCase) &&
        context.Request.Method == HttpMethods.Post)
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
        var bodyText = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        if (string.IsNullOrWhiteSpace(bodyText))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = "Request body is required." });
            return;
        }

        try
        {
            using var document = JsonDocument.Parse(bodyText);
            if (!document.RootElement.TryGetProperty("url", out var urlElement) ||
                urlElement.ValueKind != JsonValueKind.String ||
                string.IsNullOrWhiteSpace(urlElement.GetString()))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "Subscription URL is required." });
                return;
            }
        }
        catch (JsonException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = "Invalid JSON body." });
            return;
        }
    }

    await next();
});

app.MapControllers();

app.Run();

public partial class Program { }
