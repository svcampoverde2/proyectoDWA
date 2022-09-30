using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class tipoUsuario
    {
        [Key]
        public int? idtipo_user { get; set; }
        [Required]
        public string? rolUsuario { get; set; }
        [Required]
        public string? estado { get; set; }
    }
}
