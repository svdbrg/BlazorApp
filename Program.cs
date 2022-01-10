using BlazorApp.Features.LiveDisplayer.Configuration;
using BlazorApp.Features.Mortgager.Configuration;
using BlazorApp.Features.Shared;
using BlazorApp.Features.Shared.Models;
using BlazorApp.Features.TravelPlanner.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLocalization();

builder.Services.Configure<List<FeatureInformation>>(opt =>
{
    opt = new List<FeatureInformation>();
});

builder.Services.ConfigureMortgager(builder);
builder.Services.ConfigureLiveDisplayer(builder);
builder.Services.ConfigureSharedServices();
builder.Services.ConfigureTravelPlanner(builder);

builder.Services.ConfigureAutoMapper();

builder.Services.AddSingleton<AppState>();
builder.Services.AddSingleton<LoadingState>();

var app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // For when docker container is running in Heroku
    app.Urls.Add("https://*:" + Environment.GetEnvironmentVariable("PORT"));
}

app.AddGoogleCredentials();

app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization("sv-SE");

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();