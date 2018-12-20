namespace SimpleCms

open LiteDB
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open StructureMap
open Swashbuckle.AspNetCore.Swagger
open System

type Startup private () =
    let mutable configuration : IConfigurationRoot = null
    
    member this.Configuration
        with get () = configuration
        and private set (value) = configuration <- value
    
    new(env : IHostingEnvironment) as this =
        Startup()
        then 
            let builder =
                ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional = false, reloadOnChange = true)
                    .AddJsonFile(sprintf "appsettings.%s.json" env.EnvironmentName, optional = true)
                    .AddEnvironmentVariables()
                    
            this.Configuration <- builder.Build()
    
    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services : IServiceCollection) =
        
        services.AddLogging() |> ignore
        
        services.AddSwaggerGen(fun x ->
            x.SwaggerDoc("v1", new Info(Title = "SimpleCms", Version = "v1")) |> ignore)
            |> ignore
        
        services.AddMvcCore() |> ignore
        
        services.AddMvc() |> ignore
        
        let container =
            new Container(fun opt -> 
            
            opt.Scan(fun x -> 
                x.AssemblyContainingType(typeof<Startup>)
                x.Assembly("Dal")
                x.Assembly("Logic")
                x.WithDefaultConventions() |> ignore)
            
            opt.For<LiteDatabase>().Use(new LiteDatabase("Filename=database.db")) |> ignore
            
            opt.Populate(services) |> ignore)
        
        container.GetInstance<IServiceProvider>()
    
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app : IApplicationBuilder, env : IHostingEnvironment) =
        app.UseSwagger() |> ignore
        
        app.UseSwaggerUI(fun x ->
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1") |> ignore) |> ignore
        
        app.UseMvc(fun x ->
            x.MapRoute("default", "{controller=Home}/{action=Index}") |> ignore) |> ignore
