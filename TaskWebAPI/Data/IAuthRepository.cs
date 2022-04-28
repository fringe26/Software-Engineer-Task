using TaskWebAPI.Models;

namespace TaskWebAPI.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username,string password);  //will return a token string

        Task<bool> UserExist(string username);
    }
}
