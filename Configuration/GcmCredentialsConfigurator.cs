public static class GcmCredentialsConfigurator
{
    public static void ConfigureGoogleCredentials()
    {
        var dir = Directory.GetCurrentDirectory();
        var filecontent = Environment.GetEnvironmentVariable("gcm.json");

        File.WriteAllText($"{dir}/gcm.json", filecontent);

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", $"{dir}/gcm.json");

        Console.WriteLine("Credentials: " + Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
    }
}