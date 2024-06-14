using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace WayraWasi.Data
{
    public class DBDapperContext : IdentityDbContext
    {
        private readonly string _connectionDapper;

        public DBDapperContext(DbContextOptions<DBDapperContext> options)
            : base(options)
        {
        }
        public DBDapperContext(string connectionDapper)
        {
            _connectionDapper = connectionDapper;
        }
        // Crea una nueva conexión a la base de datos por cada solicitud HTTP, y evita compartir conexiones con otras solicitudes.
        public IDbConnection GetConnection(){
            var connection = new SqlConnection(_connectionDapper);
            connection.Open();
            return connection;
        }
    }
}
