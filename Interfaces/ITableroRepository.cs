using Kanban;
using Kanban.ViewModels;

public interface ITableroRepository
{
    public Tablero CrearTablero(Tablero tablero);
    public List<ListarTablerosViewModel> ListarTableros();
    //public List<ListarTablerosViewmodel> ObtenerPorPropietario(int idUsuario);
    public List<ListarTablerosViewModel> ObtenerPorUsuarioAsignado(int idUsuario);
    public Tablero ObtenerTablero(int id);
    public void ModificarTablero(int id, Tablero tablero);
    public void EliminarTablero(int id);
    public List<ListarTablerosViewModel> ListarTablerosPorUsuario(int idUsuario);
}