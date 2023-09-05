using FoodDeliveryTracking.Services.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryTracking.Models.Request
{
    public class VehicleRequest : IVehicle
    {
        public string Plate { get; set; }

        [JsonIgnore]
        public ICollection<IOrder> Orders { get; set; }

        [JsonIgnore]
        public ICollection<ILocation> LocationHistory { get; set; }

        public int CurrentLocationId { get; set; }

        public ILocation CurrentLocation { get; set; }
    }
}
