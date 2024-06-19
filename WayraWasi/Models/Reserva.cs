using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WayraWasi.Models
{
    public class Reserva
    {
        public int IdReservacion { get; set; }
        public string? NombreCliente { get; set; }
        public DateTime? FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public int NumeroPersonas { get; set; }
        public int IdCabania { get; set; }
        public string? Estado { get; set; }
        public virtual Cabania Cabania { get; set; }
    }
}
