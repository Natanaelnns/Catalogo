using APICatalago.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalago.Models;

[Table("Produtos")]
public class Produto 
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Required]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //    if (!string.IsNullOrEmpty(this.Nome))
    //    {
    //        var primeirLetra = Nome.ToString()[0].ToString();
    //        if (primeirLetra != primeirLetra.ToUpper())
    //        {
    //            yield return new
    //                ValidationResult("A primeira letra do produto deve ser maiúscula",
    //                new[]
    //                { nameof(this.Nome)
    //                });
    //        }
    //    }

    //    if(this.Estoque <= 0)
    //    {
    //        yield return new
    //               ValidationResult("O estoque deve ser maior que zero",
    //               new[]
    //               { nameof(this.Estoque)
    //               });
    //    }
    //}
}

