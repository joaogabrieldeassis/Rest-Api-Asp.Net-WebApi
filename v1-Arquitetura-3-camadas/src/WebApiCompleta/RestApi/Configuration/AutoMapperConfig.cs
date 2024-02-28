using AutoMapper;
using Dev.AppMvc.ViewModels;
using Dev.Data.Repository;
using MinhaAp.Busines.Models;

namespace RestApi.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            IMappingExpression<FornecedorViewModel, Fornecedor> mappingExpression2 = CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            IMappingExpression<EnderecoViewModel, Endereco> mappingExpression1 = CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            IMappingExpression<ProdutoViewModel, Produto> mappingExpression = CreateMap<ProdutoViewModel, Produto>();            
            IMappingExpression<Produto,ProdutoViewModel> mappingExpression3 = CreateMap<Produto, ProdutoViewModel>()
            .ForMember(x=>x.NomeFornecedor,y=>y.MapFrom(m=>m.Fornecedor.Nome));
        }
    }
}
