using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_Geolocalizacion.Data;
using Prueba_Geolocalizacion.Models;
using Prueba_Geolocalizacion.Utils;

namespace Prueba_Geolocalizacion.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GuardarUbicacion([FromBody] UbicacionDTO ubicacion)
    {
        //BUSCANDO EMPLEADO
        var empleado = _context.Empleados.Include(e => e.Sede).FirstOrDefaultAsync(e => e.Id == ubicacion.EmpleadoId); // Asegúrate de mandar el ID

        if (empleado == null || empleado.Result.Sede == null)
            return BadRequest("Empleado o sede no encontrados.");

        var distancia = GeoUtils.CalcularDistancia(
            empleado.Result.Sede.Latitud,
            empleado.Result.Sede.Longitud,
            ubicacion.Latitud,
            ubicacion.Longitud
        );

        if (distancia > empleado.Result.Sede.RadioValidacion)
        {
            Console.WriteLine($"Distancia: {distancia} metros, FUERA DEL RANGO");
            return BadRequest("Estás fuera del rango permitido para marcar asistencia.");
        }
        Console.WriteLine($"Distancia: {distancia} metros, dentro del rango");



        // Aquí podrías comparar con la ubicación de la sede o simplemente guardar en BD.
        // Console.WriteLine($"Ubicación recibida: {ubicacion.Latitud}, {ubicacion.Longitud}, {ubicacion.EmpleadoId}");
        return Ok($"Ubicación recibida: Latitud {ubicacion.Latitud}, Longitud {ubicacion.Longitud}, EmpleadoId {ubicacion.EmpleadoId}, ASISTENCIA REGISTRADA");
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
