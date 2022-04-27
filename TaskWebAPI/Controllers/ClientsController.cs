using Microsoft.AspNetCore.Mvc;
using TaskWebAPI.DTO.Clients;
using TaskWebAPI.Models;
using TaskWebAPI.Services;

namespace TaskWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService=clientService;
        }

        [HttpGet("allclients")]
        public async Task<IActionResult> Get()
        {
            return Ok( await _clientService.GetAllClients());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _clientService.GetClientById(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddClient(AddClientDTO newClient)
        {
            
            return Ok(await _clientService.AddClient(newClient));
        }

        [HttpPut("patch")]
        public async Task<IActionResult> UpdateClient(UpdateClientDTO updatedClient)
        {
            ServiceResponse<GetClientDTO> response = await _clientService.UpdateClient(updatedClient);

            if (response.Data == null)
            {
                return NotFound(response);  // so we don't have status code 200 brat 
            }
            return Ok(response);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetClientDTO>> response = await _clientService.DeleteClient(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
