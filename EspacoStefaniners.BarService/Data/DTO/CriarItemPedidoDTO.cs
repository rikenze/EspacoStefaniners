using System.ComponentModel.DataAnnotations;

namespace EspacoStefaniners.BarService.Data.DTO
{
    public class CriarItemPedidoDTO
    {
        [Required]
        public CriarProdutoDTO Produto { get; set; }

        [Required]
        public int Quantidade { get; set; }
    }
}