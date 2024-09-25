using System.ComponentModel.DataAnnotations;

namespace EspacoStefaniners.BarService.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
    }
}