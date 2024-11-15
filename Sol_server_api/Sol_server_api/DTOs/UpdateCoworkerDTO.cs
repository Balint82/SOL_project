namespace Sol_server_api.DTOs
{
    public class UpdateCoworkerDTO
    {
        public string? Email { get; set; }
        public string? CoworkerName { get; set; }
        public int? RoleID { get; set; }
        public UpdateLoginDTO? Login { get; set; }
        public UpdatePersonalDataDTO? PersonalData { get; set; }
    }

}
