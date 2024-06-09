using System.Data.SqlClient;

namespace WayraWasi.Data
{
    public class DBDapperContext
    {
        private readonly string _dbConnectionString;

        public DBDapperContext(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }

        public SqlConnection GetConnection() // Crea una nueva conexión a la base de datos por cada solicitud HTTP, y evita compartir conexiones con otras solicitudes.
        {
            try
            {
                var conexion = new SqlConnection(_dbConnectionString);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
                throw;
            }
        }
    }
}
