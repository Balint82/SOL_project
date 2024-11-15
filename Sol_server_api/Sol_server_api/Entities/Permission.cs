using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Sol_server_api.Entities
{
    public class Permission
    {
        public int PermissionID { get; set; }

        [Required]
        [MaxLength(255)]
        public string PermissionName{ get; set; } = string.Empty;

        public required ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
