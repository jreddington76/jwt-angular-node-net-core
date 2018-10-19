using System.Collections.Generic;

namespace aspnet_core_token_server.Models
{
  public class UserModel
  {
    public string Lastname { get; set; }
    public string Firstname { get; set; }
    public string Fullname { get { return $"{Firstname} {Lastname}"; } }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }
  }
}