using TaskWebAPI.DTO.Clients;
using TaskWebAPI.Models;

namespace TaskWebAPI.Services
{
    public interface IClientService
    {
        Task<ServiceResponse<List<GetClientDTO>>> GetAllClients();
        Task<ServiceResponse<GetClientDTO>> GetClientById(int id);
        Task<ServiceResponse<List<GetClientDTO>>> AddClient(AddClientDTO client);

        Task<ServiceResponse<GetClientDTO>> UpdateClient(UpdateClientDTO client);

        Task<ServiceResponse<List<GetClientDTO>>> DeleteClient(int id);
    }
}
