using System.ComponentModel.DataAnnotations.Schema;

namespace Sol_server_api.Entities
{
    public class Process
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; } = string.Empty;//FK
        public string Desc { get; set; } = string.Empty;
        
        [ForeignKey("Project")]
        public int? FKProjectID { get; set; }
        public virtual Project? Project { get; set; }
        }
}
