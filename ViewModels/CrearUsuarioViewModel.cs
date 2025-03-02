using Kanban;
using System.ComponentModel.DataAnnotations;
public class CrearUsuarioViewModel
{
    private string usuario;
    private string password;
    private Rol? rolUsuario;

    [Required]
    [StringLength(100, ErrorMessage = "El nombre de usuario no debe exceder los 100 caracteres.")]
    public string Usuario { get => usuario; set => usuario = value; }

    [Required]
    [StringLength(100, ErrorMessage = "La contraseña no debe superar los 100 caracteres")]
    public string Password { get => password; set => password = value; }

    [Required]
    [Range(1, 2, ErrorMessage = "El rol de usuario debe ser 1(Administrador) o 2 (Operador)")]
    public Rol? RolUsuario { get => rolUsuario; set => rolUsuario = value; }
}