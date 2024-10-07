using EspacoStefaniners.BarService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoStefaniners.BarService.Data.DTO
{
    public class EditarPedidoDTO
    {
        [Required]
        [MaxLength(60)]
        public string NomeCliente { get; set; }
        [Required]
        [MaxLength(60)]
        public string EmailCliente { get; set; }
        public ICollection<EditarItemPedidoDTO> ItensPedido { get; set; }
    }

    public class EditarItemPedidoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public EditarProdutoDTO Produto { get; set; }
    }

    public class EditarProdutoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string NomeProduto { get; set; }
    }
}