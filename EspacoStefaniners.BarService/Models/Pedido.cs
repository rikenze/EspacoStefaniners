using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EspacoStefaniners.BarService.Models
{
    public class Pedido
    {
        [JsonIgnore]
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
        [JsonIgnore]
        public virtual ICollection<ItensPedido> Itens { get; set; } = new List<ItensPedido>();
    }
}