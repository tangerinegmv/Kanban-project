using Kanban;

public interface ITareaRepository
{
    public Tarea CrearTarea(int idTablero, Tarea tarea);
    public List<Tarea> ListarTareasPorTablero(int idTablero);
    public List<Tarea> ListarTareasPorUsuario(int idUsuario);
    public Tarea Detalles(int id);
    public void ModificarTarea(int id, Tarea tarea);
    public void EliminarTarea(int id);
    void AsignarUsuario(int idTarea, int idUsuario);
    
}