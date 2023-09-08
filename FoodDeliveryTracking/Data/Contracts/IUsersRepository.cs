using FoodDeliveryTracking.Models.Request;
using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Data.Contracts
{
    public interface IUsersRepository
    {
        Task<bool> InsertUserAsync(IUser addAuthUser);
        Task<bool> LoginUserAsync(IUser user);
        Task RegisterTokenUserAsync(IUser user);
    }
}
