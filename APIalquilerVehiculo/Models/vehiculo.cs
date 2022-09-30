using System.ComponentModel.DataAnnotations;

namespace APIalquilerVehiculo.Models
{
    public class vehiculo
    {
        [Key]
        public int? idAuto { get; set; }
        [Required]
        public string? marca { get; set; }
        [Required]
        public int? plazas { get; set; }
        [Required]
        public string? cambios { get; set; }
        [Required]
        public int? kilometraje { get; set; }
        [Required]
        public float? precioAlquiler { get; set; }
        [Required]
        public int? idTipo_vh { get; set; }
        [Required]
        public int? idEst_vh { get; set; }
        [Required]
        public string? estado { get; set; }

    }
}
