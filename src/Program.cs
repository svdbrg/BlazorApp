using BlazorApp.Features.LiveDisplayer.Configuration;
using BlazorApp.Features.Mortgager.Configuration;
using BlazorApp.Features.TravelPlanner.Configuration;
using BlazorApp.Features.Shared;
using BlazorApp.Features.Shared.Models;
using MudBlazor.Services;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.local.json",
                optional: true,
                reloadOnChange: true)
                .AddEnvironmentVariables();

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

builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<LoadingState>();
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<SettingsState>();

builder.Services.AddMudServices();

builder.Services.AddHttpsRedirection(options => { options.HttpsPort = 443; });
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
    options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
});

builder.Services.Configure<Keys>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // For when docker container is running in Heroku
    app.Urls.Add("http://*:" + Environment.GetEnvironmentVariable("PORT"));

    app
        .UseForwardedHeaders()
        .UseHttpsRedirection();
}

app.AddGoogleCredentials(builder);

app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization("sv-SE");

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapGet("/api/get-departures-for-stop/{stopname}", (ITravelPlannerDataClient travelPlannerDataClient, string stopname) => travelPlannerDataClient.GetDeparturesForStop(stopname));

app.Run();