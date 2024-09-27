using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Web.Http;

namespace EspacoStefaniners.BarService.Models
{
    public class ItemPedido
    {
        [Required]
        [Key]
        [SwaggerIgnorePost]
        public int Id { get; set; }

        [ForeignKey("Pedido"), Required]
        [SwaggerIgnorePost]
        public int IdPedido { get; set; }

        [ForeignKey("Produto"), Required]
        [SwaggerIgnorePost]
        public int IdProduto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        // Propriedades de navegação
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
    }
}