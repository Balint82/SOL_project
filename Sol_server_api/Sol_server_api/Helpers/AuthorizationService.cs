using Sol_server_api.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Sol_server_api.Helpers
{
    public class AuthorizationService
    {
        private readonly SolContext _context;


        public AuthorizationService(SolContext context)
        {
            _context = context;
        }

        public bool UserHasPermission(string loginName, string requiredPermission)
        {
            var coworkerIDidentification = _context.Logins.Where(login => login.LoginName == loginName).Select(login => login.FKLoginCWID).SingleOrDefault();
            var coworkerName = _context.Coworkers.Where(coworker => coworker.CoworkerID == coworkerIDidentification).Select(coworker => coworker.CoworkerName).SingleOrDefault();

            var role = _context.Coworkers.Where(coworker => coworker.CoworkerName == coworkerName).Select(coworker => coworker.Role).SingleOrDefault();
            
            var permissions = _context.RolePermissions.Where(rp => rp.RoleID == role.RoleID).Select(rp => rp.Permission.PermissionName).ToList();
            

            return permissions.Contains(requiredPermission);
        }



        public bool adminHasPermission(string adminName, string requiredPermission)
        {
            var role = _context.Admins.Where(admin => admin.AdminName == adminName).Select(admin => admin.Role).SingleOrDefault();

            var permissions = _context.RolePermissions.Where(rp => rp.RoleID == role.RoleID).Select(rp => rp.Permission.PermissionName).ToList();


            return permissions.Contains(requiredPermission);
        }
    }
}
