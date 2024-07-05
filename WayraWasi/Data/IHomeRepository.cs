using WayraWasi.Models;
using WayraWasi.ViewModels;

namespace WayraWasi.Data
{
    public interface IHomeRepository
    {
        Task<IEnumerable<DisponibilidadViewModel>> ListarDisponibilidadPorFecha(DateTime fechaInicio, DateTime fechaFin);
    }
}
