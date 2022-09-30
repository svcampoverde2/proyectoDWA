using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class tipoVehiculo
    {
        [Key]
        public int? idTipo_vh { get; set; } 
        [Required]
        public string? descripcion { get; set; }
        [Required]
        public string? estado { get; set; }
    }
}
