using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Sol_server_api.Entities
{
    public class Coworker
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CoworkerID { get; set; }
        [Required]
        public string CoworkerName { get; set; } = string.Empty;

        [Required]
        public virtual PersonalData PersonalData { get; set; }

        [Required]
        public int RoleID { get; set; }
        public Role? Role { get; set; }

        public virtual Login? Login { get; set; }  
    }
}
