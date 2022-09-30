//ing Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class Usuario
    {
        [Key]
        public int? idUsuario { get; set; }
        [Required]
        public string? correo { get; set; }
        [Required]
        public string? password { set; get; }
        [Required]
        public int? idTipo_User { get; set; }
        [Required]
        public string? estado { get; set; }
    }
}
