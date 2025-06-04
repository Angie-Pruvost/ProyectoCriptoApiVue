using criptoApiProyecto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace criptoApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public TransaccionesController(AppDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        //get todas las transacciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransaccion()
        {
            return await _context.Transacciones.Include(t => t.Cliente).ToListAsync();
        }

        [HttpGet("cliente / {clienteId}")]//get para obtener por cliente/idCliente
        public async Task<IActionResult> GetTransaccionPorCliente(int clienteId)
        {//el t.IdCliente me trae los datos de los clientes relacionados con la transaccion
            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == clienteId);

            if (!clienteExiste)
            {
                return NotFound($"No se encontro el cliente con id {clienteId}.");
            }

            var transacciones =await _context.Transacciones.Where(t => t.ClienteId == clienteId).OrderByDescending(t => t.Date).ToListAsync();

            return Ok(transacciones);
        }

        [HttpGet("{id}")]//get para obtener una transaccion puntual
        public async Task<IActionResult>GetTransaccion(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);

            if(transaccion == null)
            {
                return NotFound();
            }
            return Ok(transaccion);
        }

        [HttpPost]// Crea una nueva transaccion
        public async Task<ActionResult<Transaccion>> PostTransaccion([FromBody] Transaccion transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(transaccion.CryptoAmount <= 0)
            {
                return BadRequest("La cantidad debe ser mayor a cero");
            }
            if(transaccion.Action.ToLower() != "purchase" && transaccion.Action.ToLower() != "sale")
            {
                return BadRequest("La accion debe ser 'purchase' o 'sale'.");
            }
            string url = $"https://criptoya.com/api/argenbtc/{transaccion.CryptoCode}/ars";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Error consultando el precio de la criptomoneda.");

                }
                var json = await response.Content.ReadAsStringAsync();
                var resultado = JsonDocument.Parse(json);

                if(!resultado.RootElement.TryGetProperty("TotalAsk", out var precioActual))
                {
                    return BadRequest("No se pudo obtener el precio de la criptomoneda.");
                }

                decimal precio = precioActual.GetDecimal();

                //calcular el total en pesos argentinos

                transaccion.Money = transaccion.CryptoAmount * precio;

                //guardar en la base de datos
                _context.Transacciones.Add(transaccion);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTransaccion), new { id = transaccion.Id }, transaccion);
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Error al procesar la transaccion" + ex.Message);
            }
        }

        [HttpPut ("{id}")]//editar una transaccion
        public async Task<IActionResult> PutTransaccion(int id, [FromBody] Transaccion transaccion)
        {
            if(id != transaccion.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(transaccion.CryptoAmount <= 0)
            {
                return BadRequest("La cantidad debe ser mayor a 0.");
            }
            //llamar a la api de cryptoYa 
            var hhtpClient = new HttpClient();
            var url = $"https://criptoya.com/api/argenbtc/{transaccion.CryptoCode}/ars";
           
            try
            {
                var response = await hhtpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(jsonString);
                var root = jsonDoc.RootElement;
                var price = root.GetProperty("totalAsk").GetDecimal();

                transaccion.Money = price * transaccion.CryptoAmount;
                
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al obtener el precio de la cripto: {ex.Message}");
            }
            _context.Entry(transaccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(transaccion);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Transacciones.Any(t => t.Id == id))
                    return NotFound();

                throw;
            }
        }

        [HttpDelete("{id}")]//eliminar una transaccion por id
        public async Task<IActionResult> DeleteTransaccion(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);

            if(transaccion == null)
            {
                return NotFound();
            }

            _context.Transacciones.Remove(transaccion);
            await _context.SaveChangesAsync();

            return Ok($"Transaccion con Id {id} eliminada con exito."); 
        }

    }
}
