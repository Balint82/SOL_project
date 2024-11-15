using System;
using System.Collections.Generic;

namespace Sol_server_api.DTOs
{
    public class ProjectDTO
    {
        public int ProjectID { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTime ProjectDate { get; set; }
        public string? Description { get; set; }
        public string ProcessStatus { get; set; } = string.Empty; //PLK

        public int FKCustomerID { get; set; } // Foreign Key
        public int? FKProcessID { get; set; }
        public int? FKCoworkerID { get; set; }  // Opcióként kezelhető, ha vannak hozzárendelt coworkerek
    }
}
