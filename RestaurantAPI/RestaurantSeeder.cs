using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.entity;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbcontext;

        public RestaurantSeeder(RestaurantDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Seed()
        {
            if(_dbcontext.Database.CanConnect())
            {
                if(!_dbcontext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbcontext.Restaurants.AddRange(restaurants);
                    _dbcontext.SaveChanges();

                }
            }
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "Kfc",
                    Description = "KFC - Kenucky fried Chicken",
                    Category = "Fast food",
                    HasDelivery = true,
                    ContactEmail = "KFC@KFC.com",
                    ContactNumber = "-",
                    Dishes = new List<Dish>
                    {
                        new Dish()
                        {
                            Name = "chicken",
                            price = 7.99M
                        },
                        new Dish()
                        {
                            Name = "Chicken burger",
                            price = 8.99M
                        },
                    },
                    Address = new Address()
                    {
                        City = "Krakow",
                        Street = "Ruczaj",
                        Postalcode = "43-012"
                    }
                },
                  new Restaurant()
                  {

                    Name = "McDonald",
                    Description = "Mcdonald-american fast food",
                    Category = "Fast food",
                    HasDelivery = true,
                    ContactEmail = "Mcdonald@mcdonald.pl",
                    ContactNumber = "-",
                    Dishes = new List<Dish>
                    {
                        new Dish()
                        {
                            Name = "McBurger",
                            price = 7.99M
                        },
                        new Dish()
                        {
                            Name = "Chickenburger",
                            price = 8.99M
                        },
                    },
                    Address = new Address()
                    {
                        City = "Krakow",
                        Street = "Sukiennice",
                        Postalcode = "43-012"
                    }
                  }
               

             };
            return restaurants;
        }
    }
}
