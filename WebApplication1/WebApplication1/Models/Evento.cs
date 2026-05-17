using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventosPRO.Web.Models
{
    [Table("eventos")]
    public class Evento
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Local { get; set; } = string.Empty;

        [Required]
        public DateTime Data { get; set; } = DateTime.UtcNow;

        public string? Descricao { get; set; }

        public string? AnaliseIA { get; set; }

        // USUÁRIO DONO DO EVENTO
        public string UsuarioId { get; set; } = string.Empty;
    }
}