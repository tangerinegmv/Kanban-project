using Microsoft.Data.Sqlite;
using Kanban;
using Kanban.ViewModels;
public class TableroRepository: ITableroRepository
{
    //private const string cadenaConexion = @"Data Source=Kanban.db";
    private readonly string _connectionString;
    
    public TableroRepository(string connectionString)
    {
        _connectionString = connectionString;
       
    }
    
    public Tablero CrearTablero(Tablero tablero)
    {
        Tablero? nuevo = null;
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            var query = @"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) 
                          VALUES (@id_usuario_propietario, @nombre, @descripcion);
                          SELECT last_insert_rowid();";
            connection.Open();
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.Add(new SqliteParameter("@id_usuario_propietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SqliteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@descripcion", string.IsNullOrEmpty(tablero.Descripcion) ? DBNull.Value : tablero.Descripcion));
                int idGenerado = Convert.ToInt32(command.ExecuteScalar());
                nuevo = new Tablero();
                nuevo.Id = idGenerado;
                nuevo.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
                nuevo.Nombre = tablero.Nombre;
                nuevo.Descripcion = tablero.Descripcion;
                connection.Close();
            }
        
        }
        if (nuevo == null)
        {
            throw new Exception("No se pudo crear el tablero.");
        }
        return nuevo;

    }
    public List<ListarTablerosViewModel> ListarTableros()
    {
        List<ListarTablerosViewModel> listaTablero = new List<ListarTablerosViewModel>();
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            string query = "SELECT * FROM Tablero;";
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tablero = new ListarTablerosViewModel();
                    tablero.Id= Convert.ToInt32(reader["id"]);
                    tablero.IdUsuarioPropietario= Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre= reader["nombre"].ToString();
                    tablero.Descripcion=reader["descripcion"].ToString();

                    listaTablero.Add(tablero);
                }
            }
            connection.Close();
        }
        if(listaTablero.Count == 0)
        {
            throw new Exception("No se encontraron tableros.");
        }
        return listaTablero;
    }

     public List<ListarTablerosViewModel> ListarTablerosPorUsuario(int idUsuario)
        {
            List<ListarTablerosViewModel> listaTablero = new List<ListarTablerosViewModel>();
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                string query = @"SELECT t.id, t.id_usuario_propietario, t.nombre, t.descripcion, u.nombre_de_usuario AS nombre_propietario
                                FROM Tablero t
                                JOIN Usuario u ON t.id_usuario_propietario = u.id
                                WHERE t.id_usuario_propietario = @id_usuario_propietario;";
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@id_usuario_propietario", idUsuario));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    var tablero = new ListarTablerosViewModel();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    tablero.NombreUsuarioPropietario = reader["nombre_propietario"].ToString(); // Agregar el nombre del propietario
                    listaTablero.Add(tablero);
                    }
                }
                connection.Close();
            }
            if (listaTablero.Count == 0)
            {
                throw new Exception("No se encontraron tableros.");
            }
            return listaTablero;
        }
        public List<ListarTablerosViewModel> ObtenerPorUsuarioAsignado(int idUsuario)
    {
        List<ListarTablerosViewModel> listaTablero = new List<ListarTablerosViewModel>();
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                string query = @"SELECT DISTINCT t.id, t.id_usuario_propietario, t.nombre, t.descripcion, u.nombre_de_usuario AS nombre_propietario
                                FROM Tablero t
                                INNER JOIN Tarea ta ON ta.id_tablero = t.id
                                JOIN Usuario u ON t.id_usuario_propietario = u.id
                                WHERE ta.id_usuario_asignado = @idUsuario;";
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@idUsuario", idUsuario));
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    var tablero = new ListarTablerosViewModel();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tablero.NombreUsuarioPropietario = reader["nombre_propietario"].ToString(); // Agregar el nombre del propietario
                        listaTablero.Add(tablero);
                    }
                }
                connection.Close();
            }
            return listaTablero;
    }
    public Tablero ObtenerTablero(int id)
    {
        var tablero = new Tablero();
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            string query = "SELECT * FROM Tablero WHERE id = @id;";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero = new Tablero();
                    tablero.Id= Convert.ToInt32(reader["id"]);
                    tablero.IdUsuarioPropietario= Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre= reader["nombre"].ToString();
                    tablero.Descripcion=reader["descripcion"].ToString();
                }
            }
            connection.Close();
        }
        return tablero;
    }
    public void ModificarTablero(int id, Tablero tablero)
    {
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            string query = "UPDATE Tablero SET nombre = @nombre, descripcion = @descripcion WHERE id = @id;";
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.Parameters.Add(new SqliteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@descripcion", tablero.Descripcion));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void EliminarTablero(int id)
    {
        if(Verificar(id))
        {
            throw new InvalidOperationException("No se puede eliminar el tablero porque tiene tareas asociadas.");
        }
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            string query = "DELETE FROM Tablero WHERE id = @id;";
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    private bool Verificar(int id)
    {
        bool tieneTareas = false;
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            string query = "SELECT COUNT(*) FROM Tarea WHERE id_tablero = @id;";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));
            connection.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());
            tieneTareas = count > 0;
            connection.Close();
        }
        return tieneTareas;
    }
}