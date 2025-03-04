using System.ComponentModel.DataAnnotations;

namespace Kanban.ViewModels
{
    public class ModificarTareaViewModel
    {
        [Required(ErrorMessage = "El estado de la tarea es obligatorio.")]
        public EstadoTarea Estado { get; set; }
    }
}