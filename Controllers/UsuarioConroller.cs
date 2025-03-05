using Kanban;
using Kanban.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public IActionResult ListarUsuarios()
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated")))
                return RedirectToAction("Index", "Login");

            List<ListarUsuariosViewModel> usuarios = _usuarioRepository.ListarUsuarios();
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            return View(usuarios);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo cargar la lista de usuarios";
            return View(new List<ListarUsuariosViewModel>());
        }
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        try
        {
            if (HttpContext.Session.GetString("IsAuthenticated") != null &&
                HttpContext.Session.GetString("Rol") == Rol.Administrador.ToString())
            {
                return View(new CrearUsuarioViewModel());
            }
            else
            {
                return RedirectToAction("ListarTableros", "Tablero");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo cargar el formulario de creación de usuario";
            return RedirectToAction("ListarUsuarios");
        }
    }

    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel usuario)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Datos inválidos para la creación de usuario.");
                ViewBag.ErrorMessage = "Datos inválidos.";
                return View(usuario);
            }
            Usuario nuevo = _usuarioRepository.CrearUsuario(usuario);
            return RedirectToAction("ListarUsuarios");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo crear el usuario";
            return RedirectToAction("ListarUsuarios");
        }
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        try
        {
            if (HttpContext.Session.GetString("IsAuthenticated") != null &&
                HttpContext.Session.GetString("Rol") == Rol.Administrador.ToString())
            {
                Usuario usuario = _usuarioRepository.Detalles(id);
                ModificarUsuarioViewModel modificarUsuario = new ModificarUsuarioViewModel
                {
                    NombreDeUsuario = usuario.NombreDeUsuario,
                    RolUsuario = usuario.RolUsuario
                };
                return View(modificarUsuario);
            }
            else
            {
                return RedirectToAction("ListarUsuarios");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Error al cargar el formulario para modificar usuario";
            return RedirectToAction("ListarUsuarios");
        }
    }

    [HttpPost]
    public IActionResult ModificarUsuario(int id, ModificarUsuarioViewModel usuario)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Credenciales inválidas.");
                ViewBag.ErrorMessage = "Credenciales inválidas.";
                return View(usuario);
            }
            _usuarioRepository.ModificarUsuario(id, usuario);
            return RedirectToAction("ListarUsuarios");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo modificar el usuario";
            return RedirectToAction("ListarUsuarios");
        }
    }

    [HttpGet]
    public IActionResult EliminarUsuario(int id)
    {
        try
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated")))
                return RedirectToAction("Index", "Login");

            if (HttpContext.Session.GetString("Rol") != Rol.Administrador.ToString())
            {
                return RedirectToAction("ListarUsuarios");
            }

            Usuario usuario = _usuarioRepository.Detalles(id);
            if (usuario == null)
            {
                _logger.LogError($"Usuario con ID {id} no encontrado.");
                TempData["ErrorMessage"] = "Usuario no encontrado.";
                return RedirectToAction("ListarUsuarios");
            }
            return View(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Error al cargar el formulario para eliminar usuario";
            return RedirectToAction("ListarUsuarios");
        }
    }

    [HttpPost]
    public IActionResult EliminarUsuario(Usuario usuario)
    {
        try
        {
            _usuarioRepository.EliminarUsuario(usuario.Id);
            return RedirectToAction("ListarUsuarios");
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("ListarUsuarios");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "No se pudo eliminar el usuario";
            return RedirectToAction("ListarUsuarios");
        }
    }

    [HttpGet]
    public IActionResult CambiarPassword(int id)
    {
        int idUsuarioLogueado = (int)HttpContext.Session.GetInt32("Id");
        string rolUsuario = HttpContext.Session.GetString("Rol");

        if (rolUsuario != Rol.Administrador.ToString() && idUsuarioLogueado != id)
        {
            TempData["ErrorMessage"] = "No tienes permiso para cambiar la contraseña de otro usuario.";
            return RedirectToAction("ListarUsuarios");
        }

        return View(_usuarioRepository.Detalles(id));

    }

    [HttpPost]
    public IActionResult CambiarPassword(int id, Usuario usuario)
    {
        int idUsuarioLogueado = (int)HttpContext.Session.GetInt32("Id");
        string rolUsuario = HttpContext.Session.GetString("Rol");

        if (rolUsuario != Rol.Administrador.ToString() && idUsuarioLogueado != id)
        {
            TempData["ErrorMessage"] = "No tienes permiso para cambiar la contraseña de otro usuario.";
            return RedirectToAction("ListarUsuarios");
        }

        _usuarioRepository.CambiarPassword(id, usuario);
        return RedirectToAction("ListarUsuarios");
    }
}