using AutoMapper;
using TaskWebAPI.DTO.Clients;
using TaskWebAPI.Models;

namespace TaskWebAPI
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, GetClientDTO>();  // c клиента из базы данных в короктую версию
            CreateMap<AddClientDTO, Client>();   // c клента с форнта в клиента в базу версию
        }
    }
}
