using EspacoStefaniners.BarService.Models;
using System.ComponentModel.DataAnnotations;

namespace EspacoStefaniners.BarService.Data.DTO
{
    public class GetPedidoDTO
    {
        public GetPedidoDTO()
        {
            
        }

        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
        public ICollection<GetItemPedidoDTO> ItensPedido { get; set; }
    }
}