using System.ComponentModel.DataAnnotations;
using RestaurantAPI.Services;
namespace RestaurantAPI.models
{
    public class UpdateDtoRestaurant
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }


    }
}
