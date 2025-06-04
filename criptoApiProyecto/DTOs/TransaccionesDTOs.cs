using criptoApiProyecto.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace criptoApiProyecto.DTOs
{
    public class TransaccionesDTOs
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public string CryptoCode { get; set; }  // Ej: "bitcoin", "usdc"

        public string Action { get; set; }  // "purchase" o "sale"

        public decimal CryptoAmount { get; set; }

        public decimal Money { get; set; }  // Total en pesos

        public DateTime Date { get; set; }
    }
}
