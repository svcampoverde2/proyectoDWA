using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class estadoPago
    {
        [Key]
        public int? idEstado_pag { get; set; }
        [Required]
        public string? descripcion { get; set; }
        [Required]
        public string? estado { get; set; }
    }
}
