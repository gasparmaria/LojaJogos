using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using LojaJogos.Repositorio;

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
            set { this.dataNasc = value; }
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

        // MÉTODOS CLIENTE

        Conexao con = new Conexao();

        // inserindo dados no banco
        public bool CadastrarCliente(Cliente cli)
        {
            string data_sistema = Convert.ToDateTime(cli.DataNasc).ToString("yyyy-MM-dd");
            MySqlCommand cmd = new MySqlCommand("insert into tbCliente values(@CPF,@Nome,@DataNasc,@Email,@Celular,@Endereco)", con.ConectarBD());
            cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cli.CPF;
            cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cli.Nome;
            cmd.Parameters.Add("@DataNasc", MySqlDbType.DateTime).Value = data_sistema;
            cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cli.Email;
            cmd.Parameters.Add("@Celular", MySqlDbType.VarChar).Value = cli.Celular;
            cmd.Parameters.Add("@Endereco", MySqlDbType.VarChar).Value = cli.Endereco;
            cmd.ExecuteNonQuery();
            con.DesconectarBD();
            return true;
        }

        // listar clientes pelo ID
        public Cliente ListarClienteID(int cod)
        {
            var comando = String.Format("select * from tbCliente where CPF = {0}", cod);
            MySqlCommand cmd = new MySqlCommand(comando, con.ConectarBD());
            var DadosCodCli = cmd.ExecuteReader();
            return ConverterDadosClientes(DadosCodCli).FirstOrDefault();
        }

        // listar todos os clientes
        public List<Cliente> ListarCliente()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbCliente", con.ConectarBD());
            var DadosCliente = cmd.ExecuteReader();
            return ConverterDadosClientes(DadosCliente);
        }

        // convertendo os dados do cliente para string
        public List<Cliente> ConverterDadosClientes(MySqlDataReader dt)
        {
            var TodosClientes = new List<Cliente>();
            while (dt.Read())
            {
                var ClienteTemp = new Cliente()
                {
                    CPF = dt["CPF"].ToString(),
                    Nome = dt["Nome"].ToString(),
                    DataNasc = DateTime.Parse(dt["DataNasc"].ToString()),
                    Email = dt["Email"].ToString(),
                    Celular = dt["Celular"].ToString(),
                    Endereco = dt["Endereco"].ToString()
                };
                TodosClientes.Add(ClienteTemp);
            }
            dt.Close();
            return TodosClientes;
        }
    }
}