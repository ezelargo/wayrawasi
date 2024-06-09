using static Dapper.SqlMapper;

namespace WayraWasi.Data
{
    public interface IGenericRepository<TEntity, PkType> where TEntity : class, new() /* Donde Entity representa al tipo de clase con la que se tratara osea que se adapta a cualquier tipo de clase para eso sirve el Entityy el Pk Representa la Primary Key */
    {
        Task<TEntity> BuscadorId(PkType id);
        Task<IEnumerable<TEntity>> ListarTodos();
        Task<int> Crear(TEntity model);
        Task<int> Editar(TEntity model);
        Task<int> Eliminar(PkType id);
    }
}
