using AutoMapper;
using TaskWebAPI.DTO.Clients;
using TaskWebAPI.Models;

namespace TaskWebAPI.Services
{
    public class ClientService : IClientService
    {

        private static List<Client> clients = new List<Client>
        {
            new Client(),
            new Client {Name = "Tural"}
        };
        private readonly IMapper _mapper;
        public ClientService(IMapper mapper)
        {
            _mapper= mapper;
        }

        public async Task<ServiceResponse<List<GetClientDTO>>> AddClient(AddClientDTO client)
        {
            ServiceResponse<List<GetClientDTO>> serviceResponse = new ServiceResponse<List<GetClientDTO>>();
            //clients.Add(_mapper.Map<Client>(client));
            
            Client temp_client = _mapper.Map<Client>(client);
            temp_client.Id = clients.Max(c=>c.Id)+1;
            clients.Add(temp_client);
            serviceResponse.Data = clients.Select(c => _mapper.Map<GetClientDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetClientDTO>>> GetAllClients()
        {
            ServiceResponse<List<GetClientDTO>> serviceResponse = new ServiceResponse<List<GetClientDTO>>();
            serviceResponse.Data = clients.Select(c => _mapper.Map<GetClientDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetClientDTO>> GetClientById(int id)
        {
            ServiceResponse<GetClientDTO> serviceResponse = new ServiceResponse<GetClientDTO>();   
            serviceResponse.Data = _mapper.Map<GetClientDTO>(clients.FirstOrDefault(c => c.Id == id));

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetClientDTO>> UpdateClient(UpdateClientDTO client)
        {
            ServiceResponse<GetClientDTO> serviceResponse = new ServiceResponse<GetClientDTO>(); // esli ID net ?
            try {
                Client temp_client = clients.FirstOrDefault(c=>c.Id== client.Id);
                temp_client.Name = client.Name;
                serviceResponse.Data = _mapper.Map<GetClientDTO>(temp_client);
            }
            catch (Exception e) 
            { 
                serviceResponse.Success = false;
                serviceResponse.Message = $"Client with Id={client.Id} doesn't exist!";
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetClientDTO>>> DeleteClient(int id)
        {
            ServiceResponse<List<GetClientDTO>> serviceResponse = new ServiceResponse<List<GetClientDTO>>();
            try
            {
                clients.Remove(clients.First(c => c.Id == id));
                serviceResponse.Data = clients.Select(c=>_mapper.Map<GetClientDTO>(c)).ToList();
            }
            catch (Exception e)
            { 
                serviceResponse.Success=false;
                serviceResponse.Message = $"Client with Id={id} doesn't exist!";
            }

            return serviceResponse;
            

        }
    }
}
