using Restaurant.Models;

namespace Restaurant.Interfaces;

public interface IAdminServices
{

    Admin GetAdminByUsername(string username);
    void CreateAdmin(Admin admin);

}
