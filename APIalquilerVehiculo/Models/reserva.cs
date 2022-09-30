using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class reserva
    {
        [Key]
        public int? idReserva { get; set; }
        [Required]
        public string? cedula { get; set; }
        [Required]
        public string? nombres { get; set; }
        [Required]
        public string? apellidos { get; set; }
        [Required]
        public string? marca { get; set; }
        [Required]
        public int? cantidad { get; set; }
        [Required]
        public float? costo { get; set; }

    }
}
