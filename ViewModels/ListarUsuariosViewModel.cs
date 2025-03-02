namespace Kanban.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ListarUsuariosViewModel
{
    private int id;
    private string nombreDeUsuario;
    private Rol rolUsuario;
    private string password;

    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public Rol RolUsuario { get => rolUsuario; set => rolUsuario = value; }
    public int Id { get => id; set => id = value; }
    public string Password { get => password; set => password = value; }
}