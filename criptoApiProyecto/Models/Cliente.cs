using System.ComponentModel.DataAnnotations;

namespace criptoApiProyecto.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public List<Transaccion> Transacciones { get; set; }
    }
}
