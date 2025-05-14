using Web3.Module.Drivers;
//using Web3.Module.Migrations;
//using Web3.Module.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using Web3.module.Migrations;
using Web3.Module.Handlers;
//using Web3.Module.Indexes;
using YesSql.Indexes;
using OrchardCore.Data;
using Microsoft.AspNetCore.Mvc;
using Web3.module.Filters;
using Web3.module.Models;
using OrchardCore.Html.Models;

namespace Web3.module;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // Content parts
        services.AddContentPart<OrchardPostPart>().
            UseDisplayDriver<OrchardPostDisplayDriver>().
            AddHandler<OrchardPostPartHandler>();

        services.AddContentPart<HtmlBodyPart>();

        // Migrations
        services.AddDataMigration<OrchardPostMigrations>();

        // Filters
        services.Configure<MvcOptions>((options) =>
        {
            options.Filters.Add(typeof(ShapeInjectionFilter));
        });

        // Html part
        

    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "Home",
            areaName: "Web3.module",
            pattern: "Home/Index",
            defaults: new { controller = "Home", action = "Index" }
        );
    }
}

/*
 En este archivo se registran todas las dependencias de nuevos módulos que hayamos creado nosotros para que
sean visibles !!!

 */