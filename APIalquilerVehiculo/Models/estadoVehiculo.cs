using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class estadoVehiculo
    {
        [Key]
        public int? idEst_vh { get; set; }
        [Required]
        public string? descripcion { get; set; }
        [Required]
        public string? estado { get; set; }
    }
}
