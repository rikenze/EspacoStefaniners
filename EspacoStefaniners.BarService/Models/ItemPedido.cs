using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoStefaniners.BarService.Models
{
    public class ItemPedido
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Pedido"), Required]
        public int IdPedido { get; set; }

        [ForeignKey("Produto"), Required]
        public int IdProduto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        // Propriedades de navegação
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
    }
}