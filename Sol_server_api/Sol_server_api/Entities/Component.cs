using System.ComponentModel.DataAnnotations.Schema;

namespace Sol_server_api.Entities
{
    public class Component
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComponentID { get; set; }
        public string ComponentName { get; set; } = string.Empty;
        public float Price { get; set; }
        public string StockStatus { get; set; } = string.Empty; //Missing/Ordered/Arrived/InCompartment/InPackage
        
        
        public int? FKPackageID { get; set; } //FK
        // public virtual ProjectPackage? ProjectPackage { get; set; }
        
        public int? CompartmentID { get; set; }
        public virtual Compartment? Compartment { get; set; }
    }
}
