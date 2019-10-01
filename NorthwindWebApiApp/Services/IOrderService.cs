using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindWebApiApp.Models;

namespace NorthwindWebApiApp.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<BriefOrderDescription>> GetOrdersAsync();

        Task<FullOrderDescription> GetOrderAsync(int orderId);
		
		Task<IEnumerable<BriefOrderVersion2Description >> GetExtendedOrdersAsync();
    }
}
