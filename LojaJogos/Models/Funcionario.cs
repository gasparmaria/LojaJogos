using System;
using MySql.Data.MySqlClient;
using LojaJogos.Repositorio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LojaJogos.Models
{
    public class Funcionario
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O código é obrigatório.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [RegularExpression(@"([0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2})|([0-9]{11})", ErrorMessage = "Informe um CPF válido.")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        [RegularExpression(@"([0-9]{2}.[0-9]{3}.[0-9]{3}-[0-9]{1})|([0-9]{9})", ErrorMessage = "Informe um RG válido.")]
        [Required(ErrorMessage = "O RG é obrigatório.")]
        public string RG { get; set; }

        [Display(Name = "Data Nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNasc
        {
            get { return this.dataNasc.HasValue ? this.dataNasc.Value : DateTime.Now; }
            set { this.dataNasc = value; }
        }
        private DateTime? dataNasc = null;

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Endereco { get; set; }

        [RegularExpression(@"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$", ErrorMessage = "Informe um número válido.")]
        [Required(ErrorMessage = "O celular é obrigatório.")]
        public string Celular { get; set; }

        [Display(Name = "E-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido.")]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        public string Cargo { get; set; }


        // MÉTODOS FUNCIONÁRIO

        Conexao con = new Conexao();

        // inserindo os dados no banco
        public bool CadastrarFuncionario(Funcionario func)
        {
            string data_sistema = Convert.ToDateTime(func.DataNasc).ToString("yyyy-MM-dd");
            MySqlCommand cmd = new MySqlCommand("insert into tbFuncionario values(@FuncionarioID,@Nome,@CPF,@RG,@DataNasc,@Endereco,@Celular,@Email,@Cargo)", con.ConectarBD());
            cmd.Parameters.Add("@FuncionarioID", MySqlDbType.Int32).Value = func.Codigo;
            cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = func.Nome;
            cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = func.CPF;
            cmd.Parameters.Add("@RG", MySqlDbType.VarChar).Value = func.RG;
            cmd.Parameters.Add("@DataNasc", MySqlDbType.DateTime).Value = data_sistema;
            cmd.Parameters.Add("@Endereco", MySqlDbType.VarChar).Value = func.Endereco;
            cmd.Parameters.Add("@Celular", MySqlDbType.VarChar).Value = func.Celular;
            cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = func.Email;
            cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = func.Cargo;
            cmd.ExecuteNonQuery();
            con.DesconectarBD();
            return true;
        }

        // listar funcionário por ID
        public Funcionario ListarFuncionarioID(int cod)
        {
            var comando = String.Format("select * from tbFuncionario where FuncionarioID = {0}", cod);
            MySqlCommand cmd = new MySqlCommand(comando, con.ConectarBD());
            var DadosCodFunc = cmd.ExecuteReader();
            return ConverterDadosFuncionarios(DadosCodFunc).FirstOrDefault();
        }

        // listar todos os funcionários
        public List<Funcionario> ListarFuncionario()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario", con.ConectarBD());
            var DadosFuncionario = cmd.ExecuteReader();
            return ConverterDadosFuncionarios(DadosFuncionario);
        }

        // convertendo os dados do funcionário para string 
        public List<Funcionario> ConverterDadosFuncionarios(MySqlDataReader dt)
        {
            var TodosFuncionarios = new List<Funcionario>();
            while (dt.Read())
            {
                var FuncionarioTemp = new Funcionario()
                {
                    Codigo = dt["FuncionarioID"].ToString(),
                    Nome = dt["Nome"].ToString(),
                    CPF = dt["CPF"].ToString(),
                    RG = dt["RG"].ToString(),
                    DataNasc = DateTime.Parse(dt["DataNasc"].ToString()),
                    Endereco = dt["Endereco"].ToString(),
                    Celular = dt["Celular"].ToString(),
                    Email = dt["Email"].ToString(),
                    Cargo = dt["Cargo"].ToString()
                };
                TodosFuncionarios.Add(FuncionarioTemp);
            }
            dt.Close();
            return TodosFuncionarios;
        }
    }
}