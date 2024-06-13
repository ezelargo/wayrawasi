using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WayraWasi.Models
{
    public class Cabania
    {
        //Ya es incordial hacerlo por migrations
        public int IdCabania { get; set; }

        public string NombreCabania { get; set; }

        public string Descripcion { get; set; }

        public int Capacidad { get; set; }

        public double PrecioNoche { get; set; }

        public string Disponibilidad { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
