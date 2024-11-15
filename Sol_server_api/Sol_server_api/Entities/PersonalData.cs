using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Sol_server_api.Entities
{
    public class PersonalData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonalDataID { get; set; }
        public string TelNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string Address { get; set; } = string.Empty;

        [ForeignKey("Coworker")]
        public int? CoworkerID { get; set; }
        //public virtual Coworker? Coworker { get; set; } 
    }
}
