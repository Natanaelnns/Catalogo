using APICatalago.Context;
using APICatalago.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace APICatalago.Repositories;
public class CategoriaRepository : Repository<Categoria>, ICategoryRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }
}

