using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class AgendamentoRequest
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Local { get; set; }

        [Required]
        public DateTime Inicio { get; set; }

        [Required]
        public DateTime Fim { get; set; }        
    }

    public class AgendamentoLembreteRequest : AgendamentoRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O lembrete deve ser de no mínimo 1 minuto.")]
        public int MinutosLembrete { get; set; }
    }    
}
