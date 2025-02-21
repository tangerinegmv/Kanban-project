using Microsoft.Data.Sqlite;
using Kanban;
public class TareaRepository
{
    private const string cadenaConexion = @"Data Source=Kanban.db";
    public Tarea CrearTarea(int idTablero, Tarea tarea)
    {
        Tarea? nueva = null;
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = @"INSERT INTO Tarea 
                        (id_tablero, nombre, descripcion, color, estado, id_usuario_asignado) 
                        VALUES (@id_tablero, @nombre, @descripcion, @color, @estado, @id_usuario_asignado);
                        ";
            connection.Open();
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.Add(new SqliteParameter("@id_tablero", idTablero));
                command.Parameters.Add(new SqliteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SqliteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SqliteParameter("@color", tarea.Color));
                command.Parameters.Add(new SqliteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SqliteParameter("@id_usuario_asignado", DBNull.Value));

                command.ExecuteNonQuery(); 

                command.CommandText = "SELECT MAX(id) FROM Tarea";
                int idGenerado = Convert.ToInt32(command.ExecuteScalar());


                nueva = new Tarea();
                nueva.Id = idGenerado;
                nueva.IdTablero = idTablero;
                nueva.Nombre = tarea.Nombre;
                nueva.Descripcion = tarea.Descripcion;
                nueva.Color = tarea.Color;
                nueva.Estado = tarea.Estado;
                nueva.IdUsuarioAsignado = null;
                connection.Close();
            }
        }
        return nueva;
    }
    public List<Tarea> ListarTareasPorTablero(int idTablero)
    {
        List<Tarea> listaTarea = new List<Tarea>();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM Tarea WHERE id_tablero = @id_tablero;";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id_tablero", idTablero));
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tarea = new Tarea();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["id_usuario_asignado"]);
                    listaTarea.Add(tarea);
                }
            }
            connection.Close();
        }
        return listaTarea;
    }
}