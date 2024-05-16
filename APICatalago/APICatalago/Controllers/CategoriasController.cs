
using APICatalago.Context;
using APICatalago.DTOs;
using APICatalago.DTOs.Mappings;
using APICatalago.Filters;
using APICatalago.Models;
using APICatalago.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CategoriasController(IUnitOfWork unitOfWork, ILogger<CategoriasController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("getAllCatalogo")]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<CategoriaDTO>> getAllCatalogo()
        {
            var categorias = _unitOfWork.CategoryRepository.GetAll();
            if (categorias is null)
                return NotFound("Não existem categorias...");

            var categoriasDto = categorias.ToCategoriaDTOList();

            return Ok(categoriasDto);
        }

        [HttpGet("getById/{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> getById(int id)
        {
            var categoria = _unitOfWork.CategoryRepository.Get(c => c.CategoriaId == id);

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com o id= {id} não foi encontrado...");
                return NotFound($"Categoria com o id= {id} não foi encontrado...");
            }
            var categoriaDto = categoria.ToCategoriaDTO();
            return Ok(categoriaDto);
        }

        [HttpPost("addition")]
        public ActionResult<CategoriaDTO> getPost(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
            {
                _logger.LogWarning($"Dados inválidos");
                return BadRequest("Dados inválidos");
            }

            var categoria = categoriaDto.ToCategoria();

            var categoriaCriada = _unitOfWork.CategoryRepository.Create(categoria);
            _unitOfWork.Commit();

            var novaCategoriaDto = categoriaCriada.ToCategoriaDTO();

            return new CreatedAtRouteResult("obterCategoria",
                new { id = novaCategoriaDto?.CategoriaId },
                novaCategoriaDto);
        }

        [HttpPut("update/{id:int}")]
        public ActionResult<CategoriaDTO> getPut(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                _logger.LogWarning($"Dados inválidos");
                return BadRequest("Dados inválidos");
            }
            
            var categoria = categoriaDto.ToCategoria();

            var categoriaAtualizada= _unitOfWork.CategoryRepository.Update(categoria);
            _unitOfWork.Commit();

            var categoriaAtualizadaDto = categoriaAtualizada.ToCategoriaDTO();
            return Ok(categoriaAtualizadaDto);
        }

        [HttpDelete("delete")]
        public ActionResult<CategoriaDTO> getDelete(int id)
        {
            var categoria = _unitOfWork.CategoryRepository.Get(c => c.CategoriaId == id);

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com o id= {id} não foi encontrado...");
                return NotFound($"Categoria com o id= {id} não foi encontrado...");
            }
           var categoriaExcluida = _unitOfWork.CategoryRepository.Delete(categoria);
            _unitOfWork.Commit();

            var categoriaExcluidaDto = categoriaExcluida.ToCategoriaDTO();

            return Ok(categoriaExcluidaDto);
        }
    }
}
