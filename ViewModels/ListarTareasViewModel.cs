using Kanban;
using System.ComponentModel.DataAnnotations;

public class ListarTareasViewModel
{   

    private int idTablero;
    private string nombre;
    private string descripcion;
    private string color;
    private int? idUsuarioAsignado;

    public int IdTablero { get => idTablero; set => idTablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public EstadoTarea Estado { get; set; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

    
}
