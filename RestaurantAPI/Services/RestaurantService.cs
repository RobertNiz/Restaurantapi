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
using AutoMapper;
using RestaurantAPI.models;
using Microsoft.EntityFrameworkCore;
namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantDTO> GetAll();
        RestaurantDTO GetById(int id);
        
        int CreateRestaurant(RestaurantAddDto dto);
        bool Delete(int id);

        bool Update(UpdateDtoRestaurant dto, int id);
    }
    public class RestaurantService : IRestaurantService
    {

        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _Mapper;
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _Mapper = mapper;
        }

        public bool Update( UpdateDtoRestaurant dto,int id)
        {
            var restaurant = _dbContext
               .Restaurants
               .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) return false;

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
            return true;


        }
        public bool Delete(int id)
        {
            var restaurant = _dbContext
               .Restaurants
               .FirstOrDefault(r => r.Id == id);
            if(restaurant is null) return false;

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
            return true;


        }
        
        public IEnumerable<RestaurantDTO> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantDTOS = _Mapper.Map<List<RestaurantDTO>>(restaurants);
            return restaurantDTOS;
        }
        
        public RestaurantDTO GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            
            var RestaurantDto = _Mapper.Map<RestaurantDTO>(restaurant);
            return RestaurantDto;
        }
        public int CreateRestaurant(RestaurantAddDto dto)
        {
            var restaurant = _Mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;   
        }
    }
}
