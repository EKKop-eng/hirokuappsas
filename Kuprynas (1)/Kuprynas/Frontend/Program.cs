using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Kuprynas.Data;
using Kuprynas.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
var isHeroku = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DYNO"));
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    if (isHeroku)
    {
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    }
});
builder.Services.AddHttpsRedirection(options =>
{
    if (isHeroku)
    {
        options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
        options.HttpsPort = 443;
    };
});
builder.Services.AddDbContext<KruprynasDbContext>(options =>
{
    var match = Regex.Match(Environment.GetEnvironmentVariable("DATABASE_URL") ?? "", @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
    options.UseNpgsql($"Server={match.Groups[3]};Port={match.Groups[4]};User Id={match.Groups[1]};Password={match.Groups[2]};Database={match.Groups[5]};sslmode=Prefer;Trust Server Certificate=true");
});

var app = builder.Build();

// Apply migrations and seed the databasee
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<KruprynasDbContext>();
    dbContext.Database.Migrate();
}


app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();

