
using Microsoft.AspNetCore;
using CgnClean;

// Environment.SetEnvironmentVariable("DOTNET_hostBuilder:reloadConfigOnChange", "false");
var configuration = GetConfiguration();
var host = BuildWebHost(configuration, args);
host.Run();
return 0;


IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>

    WebHost.CreateDefaultBuilder(args)
        .CaptureStartupErrors(false)
        // .ConfigureKestrel(options =>
        // {
        //     var ports = GetDefinedPorts(configuration);
        //     options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
        //     {
        //         listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
        //     });

        //     options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
        //     {
        //         listenOptions.Protocols = HttpProtocols.Http2;
        //     });

        // })
        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .Build();




IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    // var config = builder.Build();

    // if (config.GetValue<bool>("UseVault", false))
    // {
    //     TokenCredential credential = new ClientSecretCredential(
    //         config["Vault:TenantId"],
    //         config["Vault:ClientId"],
    //         config["Vault:ClientSecret"]);
    //     builder.AddAzureKeyVault(new Uri($"https://{config["Vault:Name"]}.vault.azure.net/"), credential);
    // }

    return builder.Build();
}

public partial class Program
{

    public static string Namespace = typeof(Startup).Namespace;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}