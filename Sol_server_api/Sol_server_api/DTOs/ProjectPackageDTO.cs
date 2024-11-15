namespace Sol_server_api.DTOs
{
    public class ProjectPackageDTO
    {
        public string RequiredComponentName { get; set; } = string.Empty;
        public bool ForDelivery { get; set; } = false;
        public int FKProjectID { get; set; }

        // Több PackageComponent kezeléséhez
        public List<PackageComponentDTO> PackageComponents { get; set; }
    }
}

