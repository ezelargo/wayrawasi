using System.Data.SqlClient;
using WayraWasi.Helper;

namespace WayraWasi.Data
{
    public abstract class BaseRepository<Tentity> // Puedo usar uno o ningun metodo al ser abstracta
    {
        protected async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action)
        {
            try
            {
                return await action();
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                throw;
            }
        }

        protected async Task ExecuteAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                throw;
            }
        }

        protected async Task<TResult> QueryFirstOrDefaultAsync<TResult>(Func<Task<TResult>> action)
        {
            try
            {
                return await action();
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                throw;
            }
        }

        protected async Task<IEnumerable<TResult>> QueryAsync<TResult>(Func<Task<IEnumerable<TResult>>> action)
        {
            try
            {
                return await action();
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                throw;
            }
        }

        private void HandleSqlException(SqlException ex)
        {
            if (ex.Errors.Count > 0)
            {
                switch (ex.Errors[0].Number)
                {
                    case 2627:
                        throw new PrimaryKeyException("Ya existe un registro con el Id suministrado.", ex);
                    case 2601:
                    case 262:
                        throw new ForeignKeyException("Ya existe un registro con la información suministrada.", ex);
                    case 547:
                        throw new ForeignKeyException("Surgió un conflicto con la información suministrada y no se procesó.", ex);
                    default:
                        throw new Exception("Error de base de datos.", ex);
                }
            }
        }
    }
}
