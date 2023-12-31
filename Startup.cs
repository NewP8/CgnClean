using System.Reflection;
using CgnClean.CgnFintech.Domain.Aggregates.TenantAggregate;
using CgnClean.CgnFintech.Infrastructure;
using CgnClean.CgnFintech.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CgnClean;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public virtual IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services
            // .AddGrpc(options =>
            // {
            //     options.EnableDetailedErrors = true;
            // })
            //.Services
            //.AddApplicationInsights(Configuration)
            .AddCustomMvc()
            //.AddHealthChecks(Configuration)
            .AddCustomDbContext(Configuration)
            //.AddCustomSwagger(Configuration)
            .AddCustomIntegrations(Configuration)
        //.AddCustomConfiguration(Configuration)
        //.AddEventBus(Configuration)
        //.AddCustomAuthentication(Configuration);
        //configure autofac
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());

        // builder.RegisterType<OrderRepository>()
        //     .As<IOrderRepository>()
        //     .InstancePerLifetimeScope();

        services.AddScoped<ITenantRepository, TenantRepository>();

        //var container = new ContainerBuilder();
        //container.Populate(services);

        //container.RegisterModule(new MediatorModule());
        //container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

        //return new AutofacServiceProvider(container.Build());
        //return container.Build();
        return services.BuildServiceProvider();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        // using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        // {
        //         var context = serviceScope.ServiceProvider.GetRequiredService<CgnFintechContext>();
        //         context.Database.Migrate();
        // }

        // // Configure the HTTP request pipeline.
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();




        //         //loggerFactory.AddAzureWebAppDiagnostics();
        //         //loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

        //         var pathBase = Configuration["PATH_BASE"];
        //         if (!string.IsNullOrEmpty(pathBase))
        //         {
        //             loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
        //             app.UsePathBase(pathBase);
        //         }

        //         app.UseSwagger()
        //             .UseSwaggerUI(c =>
        //             {
        //                 c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Ordering.API V1");
        //                 c.OAuthClientId("orderingswaggerui");
        //                 c.OAuthAppName("Ordering Swagger UI");
        //             });

        app.UseRouting();
        app.UseCors("CorsPolicy");


        //         ConfigureAuth(app);
        //app.UseMvcWithDefaultRoute();
        //app.UseMvc();
        // app.MapControllerRoute(
        //     name: "default",
        //     pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseEndpoints(endpoints =>
        {
            // endpoints.MapGrpcService<OrderingService>();
            endpoints.MapDefaultControllerRoute();

            // endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
            // {
            //     Predicate = _ => true,
            //     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            // // });
            // endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
            // {
            //     Predicate = r => r.Name.Contains("self")
            // });
        });

        //         ConfigureEventBus(app);
    }


    //     private void ConfigureEventBus(IApplicationBuilder app)
    //     {
    //         var eventBus = app.ApplicationServices.GetRequiredService<BuildingBlocks.EventBus.Abstractions.IEventBus>();

    //         eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>>();
    //         eventBus.Subscribe<GracePeriodConfirmedIntegrationEvent, IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>>();
    //         eventBus.Subscribe<OrderStockConfirmedIntegrationEvent, IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>>();
    //         eventBus.Subscribe<OrderStockRejectedIntegrationEvent, IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>>();
    //         eventBus.Subscribe<OrderPaymentFailedIntegrationEvent, IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>>();
    //         eventBus.Subscribe<OrderPaymentSucceededIntegrationEvent, IIntegrationEventHandler<OrderPaymentSucceededIntegrationEvent>>();
    //     }

    //     protected virtual void ConfigureAuth(IApplicationBuilder app)
    //     {
    //         app.UseAuthentication();
    //         app.UseAuthorization();
    //     }
    // }

    // static class CustomExtensionsMethods
    // {
    //     public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
    //     {
    //         services.AddApplicationInsightsTelemetry(configuration);
    //         services.AddApplicationInsightsKubernetesEnricher();

    //         return services;
    //     }
}

static class CustomExtensionsMethods
{
    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        // Add framework services.
        // services.AddControllers(options =>
        //     {
        //         options.Filters.Add(typeof(HttpGlobalExceptionFilter));
        //     })
        services.AddControllersWithViews();
        // Added for functional tests
        // .AddApplicationPart(typeof(OrdersController).Assembly)
        // .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        return services;
    }

    //     public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    //     {
    //         var hcBuilder = services.AddHealthChecks();

    //         hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

    //         hcBuilder
    //             .AddSqlServer(
    //                 configuration["ConnectionString"],
    //                 name: "OrderingDB-check",
    //                 tags: new string[] { "orderingdb" });

    //         if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
    //         {
    //             hcBuilder
    //                 .AddAzureServiceBusTopic(
    //                     configuration["EventBusConnection"],
    //                     topicName: "eshop_event_bus",
    //                     name: "ordering-servicebus-check",
    //                     tags: new string[] { "servicebus" });
    //         }
    //         else
    //         {
    //             hcBuilder
    //                 .AddRabbitMQ(
    //                     $"amqp://{configuration["EventBusConnection"]}",
    //                     name: "ordering-rabbitmqbus-check",
    //                     tags: new string[] { "rabbitmqbus" });
    //         }

    //         return services;
    //     }

    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CgnFintechContext>(options =>
                {
                    options.UseSqlite(configuration["ConnectionString"],
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                            //sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                });
        //     ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
        // );

        // services.AddDbContext<IntegrationEventLogContext>(options =>
        // {
        //     options.UseSqlServer(configuration["ConnectionString"],
        //                             sqlServerOptionsAction: sqlOptions =>
        //                             {
        //                                 sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
        //                                 //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
        //                                 sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        //                             });
        // });

        return services;
    }

    //     public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
    //     {
    //         services.AddSwaggerGen(options =>
    //         {            
    //             options.SwaggerDoc("v1", new OpenApiInfo
    //             {
    //                 Title = "eShopOnContainers - Ordering HTTP API",
    //                 Version = "v1",
    //                 Description = "The Ordering Service HTTP API"
    //             });
    //             options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    //             {
    //                 Type = SecuritySchemeType.OAuth2,
    //                 Flows = new OpenApiOAuthFlows()
    //                 {
    //                     Implicit = new OpenApiOAuthFlow()
    //                     {
    //                         AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize"),
    //                         TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token"),
    //                         Scopes = new Dictionary<string, string>()
    //                         {
    //                             { "orders", "Ordering API" }
    //                         }
    //                     }
    //                 }
    //             });

    //             options.OperationFilter<AuthorizeCheckOperationFilter>();
    //         });

    //         return services;
    //     }

    public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
        //         services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddTransient<IIdentityService, IdentityService>();
        //         services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
        //             sp => (DbConnection c) => new IntegrationEventLogService(c));

        //         services.AddTransient<IOrderingIntegrationEventService, OrderingIntegrationEventService>();

        //         if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        //         {
        //             services.AddSingleton<IServiceBusPersisterConnection>(sp =>
        //             {
        //                 var serviceBusConnectionString = configuration["EventBusConnection"];

        //                 var subscriptionClientName = configuration["SubscriptionClientName"];

        //                 return new DefaultServiceBusPersisterConnection(serviceBusConnectionString);
        //             });
        //         }
        //         else
        //         {
        //             services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        //             {
        //                 var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


        //                 var factory = new ConnectionFactory()
        //                 {
        //                     HostName = configuration["EventBusConnection"],
        //                     DispatchConsumersAsync = true
        //                 };

        //                 if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
        //                 {
        //                     factory.UserName = configuration["EventBusUserName"];
        //                 }

        //                 if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
        //                 {
        //                     factory.Password = configuration["EventBusPassword"];
        //                 }

        //                 var retryCount = 5;
        //                 if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
        //                 {
        //                     retryCount = int.Parse(configuration["EventBusRetryCount"]);
        //                 }

        //                 return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        //             });
        //         }

        return services;
    }

    //     public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
    //     {
    //         services.AddOptions();
    //         services.Configure<OrderingSettings>(configuration);
    //         services.Configure<ApiBehaviorOptions>(options =>
    //         {
    //             options.InvalidModelStateResponseFactory = context =>
    //             {
    //                 var problemDetails = new ValidationProblemDetails(context.ModelState)
    //                 {
    //                     Instance = context.HttpContext.Request.Path,
    //                     Status = StatusCodes.Status400BadRequest,
    //                     Detail = "Please refer to the errors property for additional details."
    //                 };

    //                 return new BadRequestObjectResult(problemDetails)
    //                 {
    //                     ContentTypes = { "application/problem+json", "application/problem+xml" }
    //                 };
    //             };
    //         });

    //         return services;
    //     }

    //     public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    //     {
    //         if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
    //         {
    //             services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
    //             {
    //                 var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
    //                 var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
    //                 var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
    //                 var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
    //                 string subscriptionName = configuration["SubscriptionClientName"];

    //                 return new EventBusServiceBus(serviceBusPersisterConnection, logger,
    //                     eventBusSubcriptionsManager, iLifetimeScope, subscriptionName);
    //             });
    //         }
    //         else
    //         {
    //             services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
    //             {
    //                 var subscriptionClientName = configuration["SubscriptionClientName"];
    //                 var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
    //                 var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
    //                 var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
    //                 var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    //                 var retryCount = 5;
    //                 if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
    //                 {
    //                     retryCount = int.Parse(configuration["EventBusRetryCount"]);
    //                 }

    //                 return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
    //             });
    //         }

    //         services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

    //         return services;
    //     }

    //     public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    //     {
    //         // prevent from mapping "sub" claim to nameidentifier.
    //         JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

    //         var identityUrl = configuration.GetValue<string>("IdentityUrl");

    //         services.AddAuthentication(options =>
    //         {
    //             options.DefaultAuthenticateScheme = AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    //             options.DefaultChallengeScheme = AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;

    //         }).AddJwtBearer(options =>
    //         {
    //             options.Authority = identityUrl;
    //             options.RequireHttpsMetadata = false;
    //             options.Audience = "orders";
    //         });

    //         return services;
    //     }
}