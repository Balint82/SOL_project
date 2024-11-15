using Microsoft.EntityFrameworkCore;
using Sol_server_api.Entities;

namespace Sol_server_api.Data
{
    public class ProjectService
    {
        private readonly SolContext _context;
        
        public ProjectService(SolContext context) {  _context = context; }

        public Project? GetProjectDetails(int id)
        {
            var project = _context.Projects
                .Include(p => p.Customer)
                .Include(p => p.Process)
                .Include(p => p.Coworker)
                .FirstOrDefault(p => p.ProjectID == id);

            return project;
        }
    }
}