using APICatalago.Models;

namespace APICatalago.Repositories;
public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPelaCategoria(int id);
}

