namespace Restaurant.Interfaces;

public interface IOrderLogServices
{
    Task CreateLog(OrderLog log);

}
