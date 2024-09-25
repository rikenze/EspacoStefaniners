using System.ComponentModel.DataAnnotations;

namespace EspacoStefaniners.BarService.Models
{
    public class Produto
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
    }
}