using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OrchardCore.DisplayManagement.Notify;

/*
    B�SICAMENTE ESTOY CREANDO UNA LIBRERIA O NAMESPACE en la que voy a a�adir cosas que yo vaya a ir necesitando de forma
general. Es una clase!
 */
namespace Web3.module.Controllers;

public sealed class HomeController : Controller
{
    private readonly IStringLocalizer T;
    private readonly INotifier _notifier;
    private readonly IHtmlLocalizer H;
    private readonly ILogger _logger;

    public HomeController(
        IStringLocalizer<HomeController> stringLocalizer,
        INotifier notifier,
        IHtmlLocalizer<HomeController> htmlLocalizer,
        ILogger<HomeController> logger)
    {
        _notifier = notifier; // Se autodefine _notifier. 
        _logger = logger;
        H = htmlLocalizer;
        T = stringLocalizer; // Se autodefine T como string localizer 
    }

    public ActionResult Index()
    {
        ViewData["Message"] = T["HOME CONTROLLER texto"]; // Con esto podemos Crear variables con mensajes. Este se
                                                        // ha a�adido a la parte de /Home/Index.cshtml

        _notifier.SuccessAsync(H["Success notification!"]); // A�adimos otra dependencia de Orchard, la de las notificaciones.
                                                            // Si ponemos TheBlogTheme se ve mejor!

        _logger.LogError("LOG!"); // Esto es para que nos salte el error si se lelga a esta linea por algun motivo. Da todos
                                // Los detalles de la web, usuario, etc, para ver donde se ha producido el error. Tambien hay para warnings U OTROS.

        return View();

    }
}

// Cada vez que agregamos algo con la extension de Lombiq se a�aden cosas solas
