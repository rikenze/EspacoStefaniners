using EspacoStefaniners.BarService.Models;
using System.ComponentModel.DataAnnotations;

namespace EspacoStefaniners.BarService.Data.DTO
{
    public class CriarPedidoDTO
    {
        public CriarPedidoDTO()
        {
            Itens = new List<CriarItemPedidoDTO>();
        }

        [Required]
        [MaxLength(60)]
        public string NomeCliente { get; set; }
        [Required]
        [MaxLength(60)]
        public string EmailCliente { get; set; }
        [Required]
        public bool Pago { get; set; }

        [Required]
        public List<CriarItemPedidoDTO> Itens { get; set; }
    }
}