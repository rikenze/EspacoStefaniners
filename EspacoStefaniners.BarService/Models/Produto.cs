using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EspacoStefaniners.BarService.Models
{
    public class Produto
    {
        [JsonIgnore]
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
    }
}