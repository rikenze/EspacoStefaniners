using System.ComponentModel.DataAnnotations;

namespace EspacoStefaniners.BarService.Data.DTO
{
    public class CriarProdutoDTO
    {
        [Required]
        [MaxLength(20)]
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
    }
}