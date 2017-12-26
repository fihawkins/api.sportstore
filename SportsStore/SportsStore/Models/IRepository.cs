using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        Task<int> SaveProductsAsync(Product product);
        Task<Product> DeleteProductAsync(int productID);

        IEnumerable<Order> Orders { get; }
        Task<int> SaveOrdersAsync(Order order);
        Task<Order> DeleteOrderAsync(int orderID);
    }
}
