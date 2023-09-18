using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Data.Contracts
{
    public interface IUsersRepository
    {
        //Task<bool> InsertUserAsync(IUser addAuthUser);
        Task<IUser> LoginUserAsync(IUser user);
        Task RegisterTokenUserAsync(IUser user);
    }
}
