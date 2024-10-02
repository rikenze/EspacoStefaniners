using AutoMapper;
using EspacoStefaniners.BarService.Data.DTO;
using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Profiles
{
    public class ItemPedidoProfile : Profile
    {
        public ItemPedidoProfile()
        {
            CreateMap<CriarItemPedidoDTO, ItemPedido>();
        }
    }
}