namespace CgnClean;
public static class Extension
{

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json",
                    optional: true,
                    reloadOnChange: false);
            });
    // .ConfigureWebHostDefaults(webBuilder =>
    // {
    //     webBuilder.UseStartup<Startup>();
    // });
}