using AutoMapper;
using EspacoStefaniners.BarService.Data.DTO;
using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Profiles
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<CriarPedidoDTO, Pedido>();
            CreateMap<ItemPedido, GetItemPedidoDTO>();
            CreateMap<Produto, GetItemPedidoDTO>();
            CreateMap<Pedido, GetPedidoDTO>();
            CreateMap<EditarPedidoDTO, Pedido>();
            CreateMap<EditarItemPedidoDTO, ItemPedido>();
            CreateMap<EditarProdutoDTO, Produto>();

        }
    }
}