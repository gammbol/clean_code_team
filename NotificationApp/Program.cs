using NotificationApp.Models;
using NotificationApp.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var factory = new NotificationStrategyFactory();

app.MapPost("/api/user_reg", async (NotificationRequest req) =>
{
    var strategy = factory.GetStrategy("email");
    await strategy.SendAsync(req);
    return Results.Ok("User registration notification sent");
});

app.MapPost("/api/password_reset", async (NotificationRequest req) =>
{
    var strategy = factory.GetStrategy("sms");
    await strategy.SendAsync(req);
    return Results.Ok("Password reset notification sent");
});

app.MapPost("/api/order_created", async (NotificationRequest req) =>
{
    var strategy = factory.GetStrategy("push");
    await strategy.SendAsync(req);
    return Results.Ok("Order notification sent");
});

app.Run();