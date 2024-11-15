using System.ComponentModel.DataAnnotations.Schema;

namespace Sol_server_api.Entities
{
    public class PackageComponent
    {
        public int PackageComponentID { get; set; } // Az alkatrész azonosítója
        public string ComponentName { get; set; } = string.Empty;
        public int RequiredPiece { get; set; }
        public int RealPiece { get; set; }

        [ForeignKey("ProjectPackage")]
        public int ProjectPackageID { get; set; }
        public virtual ProjectPackage ProjectPackage { get; set; }
    }
}
