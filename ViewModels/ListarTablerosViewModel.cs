namespace Kanban.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ListarTablerosViewModel
{
    private int id;
    private int id_usuario_propietario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => id_usuario_propietario; set => id_usuario_propietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string NombreUsuarioPropietario { get; set; } 
    public string Descripcion { get => descripcion; set => descripcion = value; }
}