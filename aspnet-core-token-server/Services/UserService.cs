using System.Collections.Generic;
using aspnet_core_token_server.Interfaces;
using aspnet_core_token_server.Models;

namespace aspnet_core_token_server.Services
{
  public class UserService : IUserService
  {
    public UserModel GetUser(string username, string password)
    {
      if (username == "j" && password == "j")
      {
        return new UserModel
        {
          Firstname = "James",
          Lastname = "Reddington",
          Email = username,
          Roles = new List<string>() { "Manager" }
        };
      }
      if (username == "r" && password == "r")
      {
        return new UserModel
        {
          Firstname = "Jon",
          Lastname = "Doe",
          Email = username,
          Roles = new List<string>() { "Operator" }
        };
      }

      return null;
    }
  }
}