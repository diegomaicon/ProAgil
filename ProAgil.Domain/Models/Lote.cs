using System;

namespace ProAgil.Domain.Models
{
    public class Lote
    {
        public int Id { get; set; } 
        public string Nome { get; set; }    
        public decimal Preco { get; set; }  
        public DateTime? DataInicio { get; set; }
        public DateTime? MyProperty { get; set; }
        public int Quantidade { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}