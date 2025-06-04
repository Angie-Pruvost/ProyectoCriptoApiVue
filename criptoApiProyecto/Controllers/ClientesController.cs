using criptoApiProyecto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace criptoApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            return Ok(await _context.Clientes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if(cliente  == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cliente);
            }
        }

        [HttpPost]
        public async Task <IActionResult> PostCliente(Cliente cliente)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new {id =  cliente.Id});
        }
    }
}
