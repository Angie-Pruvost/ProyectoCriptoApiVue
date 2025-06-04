using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace criptoApiProyecto.Models
{
    public class Transaccion
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public string CryptoCode { get; set; }  // Ej: "bitcoin", "usdc"

        [Required]
        public string Action { get; set; }  // "purchase" o "sale"

        [Required]
        public decimal CryptoAmount { get; set; }

        public decimal Money { get; set; }  // Total en pesos

        [Required]
        public DateTime Date { get; set; }

    }
}
