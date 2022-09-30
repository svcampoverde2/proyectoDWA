using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class clientes
    {
        [Key]
        public int? idClientes { get; set; }
        [Required]
        public string? cedula { get; set; }
        [Required]
        public string? nombres { get; set; }
        [Required]
        public string? apellidos { get; set; }
        [Required]
        public string? correo { get; set; }
        [Required]
        public string? password { set; get; }
        [Required]
        public string? direccion { get; set; }
        [Required]
        public string? Ciudad { get; set; }
        [Required]
        public int? edad { get; set; }
       

    }
}
