using Kanban;

public interface ITableroRepository
{
    public Tablero CrearTablero(Tablero tablero);
    public List<Tablero> ListarTableros();
    public Tablero ObtenerTablero(int id);
    public void ModificarTablero(int id, Tablero tablero);
    public void EliminarTablero(int id);
    public List<Tablero> ListarTablerosPorUsuario(int idUsuario);
}