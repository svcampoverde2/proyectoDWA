using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class Cliente
    {
        [Key]
        public int? idCliente { get; set; }
        [Required]
        public string? cedula { get; set; }
        [Required]
        public string? nombres { get; set; }
        [Required]
        public string? apellidos { get; set; }
        [Required]
        public string? direccion { get; set; }
        [Required]
        public int? edad { get; set; }
        [Required]
        public int? idCiudad { get; set; }
        [Required]
        public int? idUsuario { get; set; }
        [Required]
        public string? estado { get; set; }

    }
}
