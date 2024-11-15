namespace Sol_server_api.DTOs
{
    public class ComponentDTO
    {
        public int ComponentID { get; set; }
        public string ComponentName { get; set; } = string.Empty;
        public float Price { get; set; }
        public string StockStatus { get; set; } = string.Empty; // Missing/Ordered/Arrived/InCompartment/InPackage
        public int? FKPackageID { get; set; }
        public int? CompartmentID { get; set; }
    }
}
