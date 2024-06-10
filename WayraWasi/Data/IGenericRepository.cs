using static Dapper.SqlMapper;

namespace WayraWasi.Data
{
    public interface IGenericRepository<TEntity> /* Donde Entity representa al tipo de clase con la que se tratara osea que se adapta a cualquier tipo de clase para eso sirve el Entityy el Pk Representa la Primary Key */
    {
        Task<TEntity> BuscadorId(int id);
        Task<IEnumerable<TEntity>> ListarTodos();
        Task<int> Crear(TEntity model);
        Task<int> Editar(TEntity model);
        Task<int> Eliminar(int id);
    }
}
