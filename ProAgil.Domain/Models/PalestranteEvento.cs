namespace ProAgil.Domain.Models
{
    public class PalestranteEvento
    {
        public int PalestranteId { get; set; }
        public Palestrante Palestrante { get; }
        public int EventoId { get; set; }
        public Evento Evento { get;  }
    }
}