using Sol_server_api.Entities;

namespace Sol_server_api.DTOs
{
    public class LoginRegisterDTO
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public int FKLoginCoworkerID { get; set; }
    }
}
