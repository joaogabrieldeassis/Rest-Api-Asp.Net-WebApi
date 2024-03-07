using AutoMapper;
using RestApiV1.Application.Dtos;
using RestApiV1.Domain.Models;

namespace RestApiV1.Application.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor,FornecedorDto>().ReverseMap();
            CreateMap<Endereco, EnderecoDto>();
            CreateMap<ProdutoDto, Produto>();

            CreateMap<Produto, ProdutoDto>()
            .ForMember(c=>c.NomeFornecedor, o=>o.MapFrom(n=>n.Fornecedor.Nome));                       
        }
    }
}
