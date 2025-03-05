using System.ComponentModel.DataAnnotations;
namespace Kanban.ViewModels;

public class CrearTableroViewModel
{
  private int idUsuarioPropietario;
  private string nombre;
  private string descripcion;
  [Required(ErrorMessage ="Elegir un propietario es obligatorio.")]
  public int IdUsuarioPropietario { get => this.idUsuarioPropietario; set => this.idUsuarioPropietario = value; }
  
  [Required(ErrorMessage ="El nombre del tablero es obligatorio")]
  [StringLength(100, ErrorMessage = "El nombre de tablero no debe exceder los 100 caracteres.")]
  public string Nombre { get => nombre; set => nombre = value; }
  [StringLength(255, ErrorMessage = "La descripcion no debe exceder los 255 caracteres.")]

  [Required(ErrorMessage = "La descripción del tablero es obligatoria.")]
  public string Descripcion { get => descripcion; set => descripcion = value; }


}