namespace Kanban.ViewModels;
using System.ComponentModel.DataAnnotations;

public class CrearTareaViewModel
{
    private int idTablero;
    private string nombre;
    private EstadoTarea estado;
    private string descripcion;
    private string color;

    [Required]
    public int IdTablero { get => idTablero; set => idTablero = value; }
    
    [Required(ErrorMessage = "El nombre de tarea es obligatorio")]
    public string Nombre { get => nombre; set => nombre = value; }
    [Required(ErrorMessage ="Elegir estado es obligatorio.")]
    [Range(1, 5, ErrorMessage = "El estado debe ser 1 (Ideas), 2 (Todo), 3 (Doing), 4 (Review), 5 (Done)")]
    public EstadoTarea Estado { get => estado; set => estado = value; }
    [Required]
    [StringLength(255, ErrorMessage = "La descripcion no debe exceder los 255 caracteres.")]
    public string Descripcion { get => descripcion; set => descripcion = value; }
    [StringLength(7, ErrorMessage = "El color debe estar en formato hexadecimal")]
    public string? Color { get => color; set => color = value; }
}