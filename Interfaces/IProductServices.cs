namespace Restaurant.Interfaces;

public interface IProductServices
{
    Product GetProductById(string id);
    Product GetProductByName(string name);
    IEnumerable<Product> GetAllProducts();
    void DeleteProduct(string id);
    void CreateProduct(Product product);
    void UpdateProduct(Product product);

}
