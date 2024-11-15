using Sol_server_api.Entities;

namespace Sol_server_api.Data
{
    public interface ILoginRepository
    {
        Login Create(Login loginEntity);
        Login GetByLoginName(string loginName);
        Login GetByID(int loginID);
    }
}
