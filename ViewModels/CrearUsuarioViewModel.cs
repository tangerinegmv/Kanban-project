namespace Kanban.ViewModels;
using System.ComponentModel.DataAnnotations;
public class CrearUsuarioViewModel
{
    private string nombreDeUsuario;
    private string password;
    private Rol? rolUsuario;

    [Required(ErrorMessage ="El nombre del usuario es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre de usuario no debe exceder los 100 caracteres.")]
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }

    [Required(ErrorMessage ="La contraseña es obligatoria")]
    [StringLength(100, ErrorMessage = "La contraseña no debe superar los 100 caracteres")]
    public string Password { get => password; set => password = value; }

    [Required]
    [Range(0, 1, ErrorMessage = "El rol de usuario debe ser 0(Administrador) o 1 (Operador)")]
    public Rol? RolUsuario { get => rolUsuario; set => rolUsuario = value; }
}