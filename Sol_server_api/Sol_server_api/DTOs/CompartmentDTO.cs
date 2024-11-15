using System.ComponentModel.DataAnnotations;

namespace Sol_server_api.DTOs
{
    public class CompartmentDTO
    {
        [Required]
        public string? StoragedComponentName { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Col { get; set; }

        [Required]
        public int Bracket { get; set; }

        [Required]
        public int MaximumPiece { get; set; }

        public int? StoragedPiece { get; set; }

        public int? FKComponentID { get; set; } // Foreign key
    }
}