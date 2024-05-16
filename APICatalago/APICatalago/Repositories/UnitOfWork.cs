using APICatalago.Context;

namespace APICatalago.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository? _produtoRepository;

    private ICategoryRepository? _categoriaRepository;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
        }
    }
    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}


