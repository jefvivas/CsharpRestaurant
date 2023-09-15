using MongoDB.Driver;
using Restaurant.Interfaces;
using Restaurant.Models;

namespace Restaurant.Services;

public class AdminServices : IAdminServices
{
    private readonly IMongoCollection<Admin> _collection;

    public AdminServices(IMongoCollection<Admin> collection)
    {
        _collection = collection;
    }


    public Admin GetAdminByUsername(string username)
    {
        return _collection.Find(a => a.Username == username).FirstOrDefault();
    }

    public void CreateAdmin(Admin admin)
    {
        _collection.InsertOne(admin);
    }

}
