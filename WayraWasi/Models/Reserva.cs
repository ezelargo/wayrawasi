using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WayraWasi.Models
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdReservacion { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCliente { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? FechaEntrada { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? FechaSalida { get; set; }

        [Required]
        public int NumeroPersonas { get; set; }

        [ForeignKey("FKCabania")]
        public int IdCabania { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }
        public virtual Cabania Cabania { get; set; }
    }
}
