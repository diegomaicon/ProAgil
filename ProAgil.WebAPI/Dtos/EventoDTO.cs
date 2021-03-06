
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Local { get; set; }
        public string  DataEvento { get; set; }
        [Required (ErrorMessage = "O Tema é de preenchimento obrigatório.")]
        public string Tema { get; set; }
        [Range(2, 120000, ErrorMessage = "Quantidade de pessoas de 2 até 120000")]
        public int QtdPessoas { get; set; }
        public string Lote { get; set; }
        public string ImagemURL  { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; } 
        public List<PalestranteDTO> Palestrantes { get; set; }  
    }
}