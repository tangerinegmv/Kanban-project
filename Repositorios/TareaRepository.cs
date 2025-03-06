using Microsoft.Data.Sqlite;
using Kanban;
using Kanban.ViewModels;
public class TareaRepository: ITareaRepository
{
    //private const string cadenaConexion = @"Data Source=Kanban.db";
    private readonly string _connectionString;

    public TareaRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    

    public Tarea CrearTarea(int idTablero, Tarea tarea)
    {
        Tarea? nueva = null;
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
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
        if (nueva == null)
        {
            throw new Exception("No se pudo crear la tarea.");
        }
        return nueva;
    }
    public List<ListarTareasViewModel> ListarTareasPorTablero(int idTablero)
    {
        List<ListarTareasViewModel> listaTarea = [];
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            string query = "SELECT * FROM Tarea WHERE id_tablero = @id_tablero;";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id_tablero", idTablero));
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tarea = new ListarTareasViewModel();
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
        if (listaTarea.Count == 0)
        {
            throw new Exception("No se encontraron tareas para el tablero.");
        }
        return listaTarea;
    }

    public List<Tarea> ListarTareasPorUsuario(int idUsuario)
    {
        List<Tarea> listaTarea = [];
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        var query = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @id_usuario;";
        SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id_usuario_asignado", idUsuario));
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
        if (listaTarea.Count == 0)
        {
            throw new Exception("No se encontraron tareas para el usuario.");
        }
        return listaTarea;

    }

    public Tarea Detalles(int id)
    {
        var tarea = new Tarea();
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            string query = "SELECT * FROM Tarea WHERE id = @id;";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();
        }
        if (tarea.Id == 0)
        {
            throw new Exception("No se encontró la tarea.");
        } 
        
        return tarea;
    }

    public void ModificarTarea(int id, Tarea tarea)
    {
        try{ 
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            var query = @"UPDATE Tarea 
                            SET estado = @estado
                            WHERE id = @id;
                            ";
            connection.Open();
            using var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            command.Parameters.Add(new SqliteParameter("@estado", tarea.Estado));

            command.ExecuteNonQuery();


            connection.Close();
        }
        catch
        {
            throw new Exception("No se pudo modificar la tarea.");
        }
    }

    public void EliminarTarea(int id)
    {
        try{
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            var query = @"DELETE FROM Tarea WHERE id = @id;";
            connection.Open();
            using SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch
        {
            throw new Exception("No se pudo eliminar la tarea.");
        }
    }

    public void AsignarUsuario(int idTarea, int idUsuario)
    {
        try{
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            {
                connection.Open();
                string query = "UPDATE Tarea SET id_usuario_asignado = @idUsuario WHERE id = @idTarea";

                using SqliteCommand command = new SqliteCommand(query, connection);
                {
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    command.Parameters.AddWithValue("@idTarea", idTarea);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        catch
        {
            throw new Exception("No se pudo asignar el usuario a la tarea.");
        }
    
    }
}