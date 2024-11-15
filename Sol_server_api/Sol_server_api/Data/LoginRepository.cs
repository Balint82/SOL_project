using Sol_server_api.Entities;

namespace Sol_server_api.Data
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SolContext _context;

        public LoginRepository(SolContext context)
        {
            _context = context;
        }

        public Login Create(Login login)
        {
            // Ellenőrizd, hogy a megadott FKLoginCWID létezik-e a munkatars_fo táblában
            var coworker = _context.Coworkers.FirstOrDefault(c => c.CoworkerID == login.FKLoginCWID);
            if (coworker == null)
            {
                throw new InvalidOperationException("Invalid FKLoginCWID specified.");
            }

            _context.Logins.Add(login);
           _context.SaveChanges();
            

            return login;
        }


        public Login GetByLoginName(string loginName)
        {
            return _context.Logins.FirstOrDefault(login => login.LoginName == loginName);
        }

        public Login GetByID(int loginID)
        {
            return _context.Logins.FirstOrDefault(login => login.LoginID == loginID);
        }
    }
}
