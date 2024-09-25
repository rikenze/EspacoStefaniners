using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoStefaniners.BarService.Models
{
    public class ItensPedido
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Pedido")]
        public int IdPedido { get; set; }
        [Required]
        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        [Required]
        public int Quantidade { get; set; }

        // Propriedades de navegação
        public virtual Pedido Pedido { get; set; } // Propriedade de navegação para Pedido
        public virtual Produto Produto { get; set; } // Presumindo que exista uma classe Produto
    }
}