using System.ComponentModel.DataAnnotations;

namespace criptoApiProyecto.DTOs
{
    public class ClientesDTOs
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public List< TransaccionesDTOs>? transacciones { get; set; }
    }
}
