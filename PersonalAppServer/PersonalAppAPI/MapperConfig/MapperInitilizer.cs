﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PersonalApp.Models.Dto;
using PersonalApp.Models.Entities;
using PersonalApp.Models.Identity;

namespace PersonalAppAPI.MapperConfig
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<ApiUser, UserDto>().ReverseMap(); 
            CreateMap<ApiUser, UserForAdminManagerDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap(); 
            CreateMap<Event, EventCreateDto>().ReverseMap();
            CreateMap<Notification, NotificationCreateDto>().ReverseMap();
            CreateMap<Notification, NotificationUpdateDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap(); 
            CreateMap<ApplicationRole, RoleDto>().ReverseMap();
        }
    }
}
