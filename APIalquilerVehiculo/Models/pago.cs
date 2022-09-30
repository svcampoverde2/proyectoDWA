using MessagePack;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace APIalquilerVehiculo.Models
{
    public class pago
    {
        [Key]
        public int? idPago { get; set; }
        [Required]
        public float? costo { get; set; }
        [Required]
        public int? id_client { get; set; }
        [Required]
        public int? idAuto { get; set; }
        [Required]
        public int? idEst_pago { get; set; }
        [Required]
        public string? estado { get; set; }
    }
}
