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


    public async Task<Admin> GetAdminByUsername(string username)
    {
        return await _collection.Find(a => a.Username == username).FirstOrDefaultAsync();
    }

    public async Task CreateAdmin(Admin admin)
    {
        await _collection.InsertOneAsync(admin);
    }

}
