namespace Sol_server_api.DTOs
{
    public class PackageComponentDTO
    {
        public string ComponentName { get; set; } = string.Empty;
        public int RequiredPiece { get; set; }
        public int RealPiece { get; set; }
    }
}
