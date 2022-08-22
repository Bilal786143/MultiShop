﻿using AutoMapper;
using MultiShop.Models.Models;
using MultiShop.Models.Models.DTOs;

namespace MultiShop
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>{
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CartDto, Cart>().ReverseMap();
                config.CreateMap<CartDetailsDto, CartDetails>().ReverseMap();
                config.CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
