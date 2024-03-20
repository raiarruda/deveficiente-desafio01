using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LivrariaTech
{
    public record AutorModel
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        [JsonIgnore]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? DataAtualizacao { get; set; }

    }
}
