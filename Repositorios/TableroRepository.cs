using Microsoft.Data.Sqlite;
using Kanban;
public class TableroRepository
{
    private const string cadenaConexion = @"Data Source=Kanban.db";
    public void CrearTablero(Tablero tablero)
    {
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            var query = "INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@id_usuario_propietario, @nombre, @descripcion);";
            connection.Open();
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.Add(new SqliteParameter("@id_usuario_propietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SqliteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@descripcion", tablero.Descripcion));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
    public List<Tablero> ListarTableros()
    {
        List<Tablero> listaTablero = new List<Tablero>();
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            string query = "SELECT * FROM Tablero;";
            SqliteCommand command = new SqliteCommand(query, connection);
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tablero = new Tablero(
                    Convert.ToInt32(reader["id_usuario_propietario"]),
                    reader["nombre"].ToString(),
                    reader["descripcion"].ToString()
                    );
                    listaTablero.Add(tablero);
                }
            }
            connection.Close();
        }
        return listaTablero;
    }
}