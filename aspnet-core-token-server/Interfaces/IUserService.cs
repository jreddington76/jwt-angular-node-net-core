using aspnet_core_token_server.Models;

namespace aspnet_core_token_server.Interfaces
{
  public interface IUserService
  {
    UserModel GetUser(string username, string password);
  }
}