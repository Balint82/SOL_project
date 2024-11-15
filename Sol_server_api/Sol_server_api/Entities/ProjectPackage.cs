using System.ComponentModel.DataAnnotations.Schema;

namespace Sol_server_api.Entities
{
    public class ProjectPackage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectPackageID { get; set; }
        public string RequiredComponentName { get; set; } = string.Empty;
        public bool ForDelivery { get; set; } = false;

        [ForeignKey("Project")]
        public int FKProjectID { get; set; } //FK
        virtual public Project? Project { get; set; }

        // Több csomag-komponens egy csomaghoz (PackageComponent kapcsolat)
        public ICollection<PackageComponent>? PackageComponents { get; set; }
    }
}
