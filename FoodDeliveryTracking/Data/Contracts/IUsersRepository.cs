using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Data.Contracts
{
    public interface IUsersRepository
    {
        Task<bool> LoginUserAsync(IUser user);
        Task RegisterTokenUserAsync(IUser user);
    }
}
