using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class ciudad
    {
        [Key]
        public int? idCiudad { get; set; }
        [Required]
        public string? nombre { get; set; }
        [Required]
        public string? estado { get; set; }
    }
}
