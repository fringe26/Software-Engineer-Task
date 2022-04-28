using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskWebAPI.Data;
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
        private readonly DataContext _context;
        public ClientService(IMapper mapper,DataContext context)
        {
            _mapper= mapper;
            _context= context;
        }

        public async Task<ServiceResponse<List<GetClientDTO>>> AddClient(AddClientDTO client)
        {
            ServiceResponse<List<GetClientDTO>> serviceResponse = new ServiceResponse<List<GetClientDTO>>();
            //clients.Add(_mapper.Map<Client>(client));
            
            Client temp_client = _mapper.Map<Client>(client);
            await _context.Clients.AddAsync(temp_client);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _context.Clients.Select(c => _mapper.Map<GetClientDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetClientDTO>>> GetAllClients()
        {
            ServiceResponse<List<GetClientDTO>> serviceResponse = new ServiceResponse<List<GetClientDTO>>();
            List<Client> dbClients = await _context.Clients.ToListAsync();
            serviceResponse.Data = dbClients.Select(c => _mapper.Map<GetClientDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetClientDTO>> GetClientById(int id)
        {
            ServiceResponse<GetClientDTO> serviceResponse = new ServiceResponse<GetClientDTO>();
            
            serviceResponse.Data = _mapper.Map<GetClientDTO>(await _context.Clients.FirstAsync(c => c.Id == id));

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetClientDTO>> UpdateClient(UpdateClientDTO client)
        {
            ServiceResponse<GetClientDTO> serviceResponse = new ServiceResponse<GetClientDTO>(); // esli ID net ?
            try {
                Client temp_client = await _context.Clients.FirstAsync(c=>c.Id== client.Id);
                temp_client.Name = client.Name;

                _context.Clients.Update(temp_client);
                await _context.SaveChangesAsync();
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
                _context.Clients.Remove(await _context.Clients.FirstAsync(c => c.Id == id));
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Clients.Select(c=>_mapper.Map<GetClientDTO>(c)).ToList();
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
