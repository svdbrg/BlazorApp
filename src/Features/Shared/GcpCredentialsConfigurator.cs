namespace BlazorApp.Features.Shared;

public static class GcpCredentialsConfigurator
{
    public static void AddGoogleCredentials(this WebApplication app, WebApplicationBuilder builder)
    {
        var filePath = "";

        Console.WriteLine("Setting GCP credentials");

        if (app.Environment.IsDevelopment())
        {
            filePath = builder.Configuration
                .GetSection("GoogleCloudPlatform")
                .GetValue<string>("TokenFilePath");

            Console.WriteLine($"In development, filepath is {filePath}");
        }
        else
        {
            Console.WriteLine($"In Heroku...");

            var dir = Directory.GetCurrentDirectory();
            var filecontent = Environment.GetEnvironmentVariable("gcp.json");
            Console.WriteLine($"Filecontent: {filecontent}");

            File.WriteAllText($"{dir}/gcp.json", filecontent);

            filePath = $"{dir}/gcp.json";

            Console.WriteLine($"Filepath is {filePath}");

        }

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
    }
}