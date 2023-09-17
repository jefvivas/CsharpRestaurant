using Restaurant.Models;

namespace Restaurant.Interfaces;

public interface IAdminServices
{

    Task<Admin> GetAdminByUsername(string username);
    Task CreateAdmin(Admin admin);

}
