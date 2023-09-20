using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Data.Contracts
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<IUser> LoginUserAsync(IUser user);
        Task RegisterTokenUserAsync(IUser user);
    }
}
