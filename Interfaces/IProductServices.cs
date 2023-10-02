namespace Restaurant.Interfaces;

public interface IProductServices
{
    Task<Product> GetProductById(string id);
    Task<Product> GetProductByName(string name);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<IEnumerable<Product>> GetAllAvailableProducts();

    Task DeleteProduct(string id);
    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);

}
