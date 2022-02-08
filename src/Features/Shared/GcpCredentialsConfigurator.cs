namespace BlazorApp.Features.Shared;

public static class GcpCredentialsConfigurator
{
    public static void AddGoogleCredentials(this WebApplication app, WebApplicationBuilder builder)
    {
        var filePath = "";
        
        if (app.Environment.IsDevelopment())
        {
            filePath = builder.Configuration
                .GetSection("GoogleCloudPlatform")
                .GetValue<string>("TokenFilePath");
        }
        else
        {
            var dir = Directory.GetCurrentDirectory();
            var filecontent = Environment.GetEnvironmentVariable("gcp.json");

            File.WriteAllText($"{dir}/gcp.json", filecontent);

            filePath = $"{dir}/gcp.json";
        }

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
    }
}