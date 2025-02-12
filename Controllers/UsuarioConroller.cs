using SQLitePCL;
using Kanban;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private UsuarioRepository usuarioRepository = new UsuarioRepository();

    public UsuarioController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(usuarioRepository.ListarUsuarios());
    }
    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new Usuario());
    }
    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        usuarioRepository.CrearUsuario(usuario);
        return RedirectToAction("Index");
    }


}