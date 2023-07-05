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
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("restaurant/api") ]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateDtoRestaurant dto , [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var IsUpdate = _restaurantService.Update(dto, id);
            if(!IsUpdate)
            {
                return NotFound();
            }
            return Ok();    

        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var Isdelete=_restaurantService.Delete(id);

             if(Isdelete)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]RestaurantAddDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _restaurantService.CreateRestaurant(dto);
            return Created($"/restaurant/api/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll()
        {
            var restauran = _restaurantService.GetAll();
            return Ok(restauran);
        }

        [HttpGet( "{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            var restauran = _restaurantService.GetById(id);

            if(restauran is null)
            {
                return NotFound();
            }
            return Ok(restauran);
        }


            
      

    }
}
