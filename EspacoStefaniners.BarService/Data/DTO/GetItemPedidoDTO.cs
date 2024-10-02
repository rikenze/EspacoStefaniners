using EspacoStefaniners.BarService.Models;
using System.Text.Json.Serialization;

namespace EspacoStefaniners.BarService.Data.DTO
{
    public class GetItemPedidoDTO
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get { return Produto.NomeProduto; } set { value = Produto.NomeProduto; } }
        public decimal ValorUnitario { get { return Produto.Valor; } set { value = Produto.Valor; } }
        public int Quantidade { get; set; }

        [JsonIgnore]
        public Produto Produto { get; set; }
    }
}