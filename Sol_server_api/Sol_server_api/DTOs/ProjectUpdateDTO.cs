namespace Sol_server_api.DTOs
{
    public class ProjectUpdateDTO
    {
        public string? Location { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string? Description { get; set; }
        public string? ProcessStatus { get; set; }
        public int? FKCustomerID { get; set; }
        public int? FKProcessID { get; set; }
        public int? FKCoworkerID { get; set; }
    }
}
