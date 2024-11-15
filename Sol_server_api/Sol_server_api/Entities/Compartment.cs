using System.ComponentModel.DataAnnotations.Schema;

namespace Sol_server_api.Entities
{
    public class Compartment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompartmentID { get; set; }
        public string? StoragedComponentName { get; set; }
        public int Row { get; set; } 
        public int Col { get; set; } 
        public int Bracket { get; set; }
        public int MaximumPiece {  get; set; }
        public int? StoragedPiece { get; set; }
        
        public int? FKComponentID { get; set; }//FK
        //public virtual Component? Component { get; set; }
    }
}
