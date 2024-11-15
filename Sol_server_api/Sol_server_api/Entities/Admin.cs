using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Sol_server_api.Entities
{
    public class Admin
    {  
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }

        public virtual PersonalData? PersonalData { get; set; }
        public Role Role { get; set; }
    }
}
