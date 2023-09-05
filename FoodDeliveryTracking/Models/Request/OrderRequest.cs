using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Services.Models;

namespace FoodDeliveryTracking.Models.Request
{
    public class OrderRequest : IOrder
    {
        public string Description { get; set; }
        public int? AssignedVehicleId { get; set; }
        public IVehicle AssignedVehicle { get; set; }
        public OrderStatus Status { get; set; }
    }


}
