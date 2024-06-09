using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WayraWasi.Models
{
    public class Cabania
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdCabania { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCabania { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public int Capacidad { get; set; }

        [Required]
        public double PrecioNoche { get; set; }

        [Required]
        public bool Disponible { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
