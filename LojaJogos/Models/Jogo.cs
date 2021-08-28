using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LojaJogos.Models
{
    public class Jogo
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O código é obrigatório.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Versão")]
        public string Versao { get; set; }

        [Required(ErrorMessage = "O desenvolvedor é obrigatório.")]
        public string Desenvolvedor { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public string Genero { get; set; }

        [Display(Name = "Faixa Etária")]
        [Required(ErrorMessage = "A faixa etária é obrigatória.")]
        public string FaixaEtaria { get; set; }

        [Required(ErrorMessage = "A plataforma é obrigatória.")]
        public string Plataforma { get; set; }

        [Display(Name = "Ano de Lançamento")]
        [Required(ErrorMessage = "O ano de lançamento é obrigatório.")]
        public string AnoLanc { get; set; }

        [Required(ErrorMessage = "A sinopse é obrigatória.")]
        public string Sinopse { get; set; }
    }
}