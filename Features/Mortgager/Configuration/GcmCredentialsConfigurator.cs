namespace BlazorApp.Features.Mortgager.Configuration;

public static class GcmCredentialsConfigurator
{
    public static void AddGoogleCredentials(this WebApplication app)
    {
        var filePath = "";
        
        if (app.Environment.IsDevelopment())
        {
            filePath = "C:\\Users\\svdbrg\\gcm\\mortgager.json";
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