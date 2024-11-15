using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Sol_server_api.Entities
{
   

    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }
        public string Location { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProjectDate { get; set; }
        public string? Description { get; set; }
        public string ProcessStatus { get; set; } = string.Empty; //PLK

        [ForeignKey("Customer")]
        public int FKCustomerID { get; set; } //FK
        public virtual Customer? Customer { get; set; }     

        /*
        [ForeignKey("Process")]
        public int? FKProcessID { get; set; }*/
        public virtual Process? Process { get; set; }
        
        [ForeignKey("Coworker")]
        public int? FKCoworkerID { get; set; }
        public virtual Coworker? Coworker { get; set;}
    }
}
