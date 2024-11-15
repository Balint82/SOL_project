using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sol_server_api.Entities
{
    public class Login
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginID { get; set; } 
        public string LoginName { get; set; } = string.Empty;
        //[JsonIgnore] //nem küldi vissza a passwordot a cookieval(vagy nem mutatja?)
        public string Password { get; set; } = string.Empty;

        public int FKLoginCWID { get; set; }
        //public virtual Coworker? Coworker { get; set; } 
    }
}
