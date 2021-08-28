using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LojaJogos.Models
{
    public class Cliente
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [RegularExpression(@"([0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2})|([0-9]{11})", ErrorMessage = "Informe um CPF válido.")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        [Display(Name = "Data Nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNasc
        {
            get { return this.dataNasc.HasValue ? this.dataNasc.Value : DateTime.Now; }
            set { this.dataNasc = value;}
        }
        private DateTime? dataNasc = null;

        [Display(Name = "E-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido.")]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        public string Email { get; set; }

        [RegularExpression(@"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$", ErrorMessage = "Informe um número válido.")]
        [Required(ErrorMessage = "O celular é obrigatório.")]
        public string Celular { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Endereco { get; set; }
    }
}