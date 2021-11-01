public static class GcmCredentialsConfigurator
{
    public static void ConfigureGoogleCredentials()
    {
        var filecontent = Environment.GetEnvironmentVariable("gcm.json");

        File.WriteAllText("/gcm.json", filecontent);

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "/gcm.json");

        Console.WriteLine(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
    }
}