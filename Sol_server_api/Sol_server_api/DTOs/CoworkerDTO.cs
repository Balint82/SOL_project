using Sol_server_api.Entities;

namespace Sol_server_api.DTOs
{
    public class CoworkerDTO
    {
        public int CoworkerID { get; set; }
        public string CoworkerName { get; set; }
        public PersonalDataDTO PersonalData { get; set; }
        public int RoleID { get; set; }
        public Login Login { get; set; }
    }
}
