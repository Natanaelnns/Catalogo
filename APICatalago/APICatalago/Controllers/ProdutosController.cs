using APICatalago.Context;
using APICatalago.Models;
using APICatalago.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutoPelaCategoria(int id)
        {
            var produtos = _unitOfWork.ProdutoRepository.GetProdutosPelaCategoria(id);
            if (produtos is null)
                return NotFound();
            return Ok(produtos);
        }

        //Obtendo todos os produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();
            if (produtos is null)
                return NotFound();
            
            return Ok(produtos);
        }

        //Obtento os produtos pelo id
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {

            var produto = _unitOfWork.ProdutoRepository.Get(c => c.ProdutoId == id);
            if (produto is null)
            {
                return NotFound($"Produto com o id={id} não encontrado...");
            }

            return Ok(produto);
        }

        //Adicionando um novo produto
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            var newProduct = _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = newProduct.ProdutoId }, newProduct);
        }

        //Atualizando o produto
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();

            return Ok(produtoAtualizado);
        }

        //Deletando o produto
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(c => c.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }

            var produtoDeletado = _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();
            return Ok(produtoDeletado);

        }
    }

}

