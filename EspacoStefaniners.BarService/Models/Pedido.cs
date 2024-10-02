using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EspacoStefaniners.BarService.Models
{
    public class Pedido
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string NomeCliente { get; set; }
        [Required]
        [MaxLength(60)]
        public string EmailCliente { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
        [Required]
        public bool Pago { get; set; }

        // Propriedade de navegação para ItensPedido
        public ICollection<ItemPedido> Itens { get; set; }
    }
}