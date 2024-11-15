using Sol_server_api.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sol_server_api.Entities
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }

        [Required]
        [MaxLength(42)]
        public string RoleName { get; set; } = string.Empty;

        public ICollection<Coworker>? Coworkers { get; set; } = new List<Coworker>();
        public ICollection<RolePermission>? RolePermissions { get; set; } = new List<RolePermission>();
    }
}
