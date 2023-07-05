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
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.entity;
using RestaurantAPI.models;
namespace RestaurantAPI
{
    public class RestaurantAutoMapper : Profile
    {
        public RestaurantAutoMapper()
        {
            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
            .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
            .ForMember(m => m.Postalcode, c => c.MapFrom(s => s.Address.Postalcode));

            CreateMap<Dish, dishDTO>();

            CreateMap<RestaurantAddDto, Restaurant>()
                .ForMember(c => c.Address, m => m.MapFrom(dto => new Address()
                { City = dto.City, Street = dto.Street, Postalcode = dto.PostalCode }));

        }
         
    }
}
