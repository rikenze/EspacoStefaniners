using System.ComponentModel.DataAnnotations;

namespace EspacoStefaniners.BarService.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        [MaxLength(60)]
        public string NomeCliente { get; set; }
        [MaxLength(60)]
        public string EmailCliente { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
    }
}