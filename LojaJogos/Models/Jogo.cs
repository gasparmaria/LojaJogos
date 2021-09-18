using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LojaJogos.Repositorio;
using MySql.Data.MySqlClient;

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
        
        [Display(Name = "Data de Lançamento")]
        [Required(ErrorMessage = "A data de lançamento é obrigatória.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AnoLanc
        {
            get { return this.anoLanc.HasValue ? this.anoLanc.Value : DateTime.Now; }
            set { this.anoLanc = value; }
        }
        private DateTime? anoLanc = null;

        [Required(ErrorMessage = "A sinopse é obrigatória.")]
        public string Sinopse { get; set; }

        // MÉTODOS JOGO

        Conexao con = new Conexao();

        //inserindo dados no banco
        public bool CadastrarJogo(Jogo jg)
        {
            string data_sistema = Convert.ToDateTime(jg.AnoLanc).ToString("yyyy-MM-dd");
            MySqlCommand cmd = new MySqlCommand("insert into tbJogo values(@JogoID,@Nome,@Desenvolvedor,@Genero,@FaixaEtaria,@Plataforma,@AnoLanc,@Sinopse,@Versao)", con.ConectarBD());
            cmd.Parameters.Add("@JogoID", MySqlDbType.Int32).Value = jg.Codigo;
            cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = jg.Nome;
            cmd.Parameters.Add("@Desenvolvedor", MySqlDbType.VarChar).Value = jg.Desenvolvedor;
            cmd.Parameters.Add("@Genero", MySqlDbType.VarChar).Value = jg.Genero;
            cmd.Parameters.Add("@FaixaEtaria", MySqlDbType.Int32).Value = jg.FaixaEtaria;
            cmd.Parameters.Add("@Plataforma", MySqlDbType.VarChar).Value = jg.Plataforma;
            cmd.Parameters.Add("@AnoLanc", MySqlDbType.DateTime).Value = data_sistema;
            cmd.Parameters.Add("@Sinopse", MySqlDbType.VarChar).Value = jg.Sinopse;
            cmd.Parameters.Add("@Versao", MySqlDbType.VarChar).Value = jg.Versao;
            cmd.ExecuteNonQuery();
            con.DesconectarBD();
            return true;
        }

        // listar jogo pelo ID
        public Jogo ListarCodJogo(int cod)
        {
            var comando = String.Format("select * from tbJogo where JogoID = {0}", cod);
            MySqlCommand cmd = new MySqlCommand(comando, con.ConectarBD());
            var DadosCodJg = cmd.ExecuteReader();
            return ConverterDadosJogos(DadosCodJg).FirstOrDefault();
        }

        // listar todos os jogos
        public List<Jogo> ListarJogo()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbJogo", con.ConectarBD());
            var DadosJogo = cmd.ExecuteReader();
            return ConverterDadosJogos(DadosJogo);
        }

        // convertendo os dados do jogo para string
        public List<Jogo> ConverterDadosJogos(MySqlDataReader dt)
        {
            var TodosJogos = new List<Jogo>();
            while (dt.Read())
            {
                var JogoTemp = new Jogo()
                {
                    Codigo = dt["JogoID"].ToString(),
                    Nome = dt["Nome"].ToString(),
                    Desenvolvedor = dt["Desenvolvedor"].ToString(),
                    Genero = dt["Genero"].ToString(),
                    FaixaEtaria = dt["FaixaEtaria"].ToString(),
                    Plataforma = dt["Plataforma"].ToString(),
                    AnoLanc = DateTime.Parse(dt["AnoLanc"].ToString()),
                    Sinopse = dt["Sinopse"].ToString(),
                    Versao = dt["Versao"].ToString()
                };
                TodosJogos.Add(JogoTemp);
            }
            dt.Close();
            return TodosJogos;
        }
    }
}