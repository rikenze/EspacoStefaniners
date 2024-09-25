using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EspacoStefaniners.BarService.Models
{
    public class ItensPedido
    {
        [Required]
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [ForeignKey("Pedido"), Required]
        [JsonIgnore]
        public int IdPedido { get; set; }

        [ForeignKey("Produto"), Required]
        [JsonIgnore]
        public int IdProduto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        // Propriedades de navegação
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
    }
}