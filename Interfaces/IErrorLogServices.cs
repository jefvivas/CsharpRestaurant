namespace Restaurant.Interfaces;

public interface IErrorLogServices
{
    Task CreateLog(ErrorLog log);

}
