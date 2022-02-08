namespace BlazorApp.Features.Shared;

public static class GcmCredentialsConfigurator
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
            var filecontent = Environment.GetEnvironmentVariable("gcm.json");

            File.WriteAllText($"{dir}/gcm.json", filecontent);

            filePath = $"{dir}/gcm.json";
        }

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
    }
}