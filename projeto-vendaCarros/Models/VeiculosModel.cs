using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace projeto_vendaCarros.Models
{
    public class VeiculosModel
    {
        //private readonly static string _conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AgenciaAuto;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly static string _conn = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;


        //criando os elementos da tabela
        public int Id { get; set; }

        [Display(Name = "Marca: ")]
        [Required(ErrorMessage ="A marca do veículo é obrigatória!")]
        public string Nome { get; set; }

        [Display(Name = "Modelo: ")]
        [Required(ErrorMessage = "O modelo do veículo é obrigatória!")]
        public string Modelo { get; set; }

        public short Ano { get; set; }

        [Display(Name = "Fabricação: ")]
        [Range (1900, 2050, ErrorMessage = "O ano deve estar entre {1} e {2}!")]
        public short Fabricacao { get; set; }

        [Display(Name = "Cor: ")]
        [Required(ErrorMessage = "A cor do veículo é obrigatória!")]
        public string Cor { get; set; }

        [Display(Name = "Combustível: ")]
        [Range (1,5, ErrorMessage = "O combustível dever estar entre {1} e {2}!")]
        public Combustivel Combustivel { get; set; }

        [Display(Name = "Automático: ")]
        [Required(ErrorMessage = "Informe se o câmbio é automático ou não!")]
        public Automatico Automatico { get; set; }

        [Display(Name = "Valor: ")]
        [Range (0.01, 9999999.99, ErrorMessage = "O valor deve estar entre {1} e {2}!")]
        public decimal Valor { get; set; }

        //[Display(Name = "Ativo: ")]
        //[Required(ErrorMessage = "Valor inválido, favor insira a marca do veículo!")]
        public bool Ativo { get; set; }



        //construtor vazio
        public VeiculosModel() { }


        //instanciando o objeto carro
        public VeiculosModel(
            int id, 
            string nome, 
            string modelo,
            short ano,
            short fabricacao,
            string cor,
            Combustivel combustivel,
            Automatico automatico,
            decimal valor,
            bool ativo)
        {
            Id = id;
            Nome = nome;
            Modelo = modelo;
            Ano = ano;
            Fabricacao = fabricacao;
            Cor = cor;
            Combustivel = combustivel;
            Automatico = automatico;
            Valor = valor;
            Ativo = ativo;
        }

        public static List<VeiculosModel> GetCarros()
        {
            var listaCarros = new List<VeiculosModel>();
            var sql = "SELECT * FROM tb_Veiculos";

            try
            {

                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    listaCarros.Add(new VeiculosModel(
                                        Convert.ToInt32(dr["Id"]),
                                        dr["Nome"].ToString(),
                                        dr["Modelo"].ToString(),
                                        Convert.ToInt16(dr["Ano"]),
                                        Convert.ToInt16(dr["Fabricacao"]),
                                        dr["Cor"].ToString(),
                                        (Combustivel)Convert.ToByte(dr["Combustivel"]),
                                        (Automatico) Convert.ToByte(dr["Automatico"]),
                                        Convert.ToDecimal(dr["Valor"]),
                                        Convert.ToBoolean(dr["Ativo"])
                                        ));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha de conexão com o banco: " + ex.Message);
            }
            return listaCarros;
        }

        public void Salvar()
        {
            var sql = "";
            if(Id==0)
                sql = "INSERT INTO tb_Veiculos (nome, modelo, ano, fabricacao, cor, combustivel, " +
                    "automatico, valor, ativo) VALUES (@nome, @modelo, @ano, @fabricacao, @cor, " +
                    "@combustivel, @automatico, @valor, @ativo)";
            else
                sql = "UPDATE tb_Veiculos SET nome=@nome, modelo=@modelo, ano=@ano, " +
                    "fabricacao=@fabricacao, cor=@cor, combustivel=@combustivel, " +
                    "automatico=@automatico, valor=@valor, ativo=@ativo WHERE id=" + Id;

                try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nome", Nome);
                        cmd.Parameters.AddWithValue("@modelo", Modelo);
                        cmd.Parameters.AddWithValue("@ano", Fabricacao);
                        cmd.Parameters.AddWithValue("@fabricacao", Fabricacao);
                        cmd.Parameters.AddWithValue("@cor", Cor);
                        cmd.Parameters.AddWithValue("@combustivel", Combustivel);
                        cmd.Parameters.AddWithValue("@automatico", Automatico);
                        cmd.Parameters.AddWithValue("@valor", Valor);
                        cmd.Parameters.AddWithValue("@ativo", Ativo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha na conexão com o Banco"+ ex.Message);
            }
        
        }


        public void Excluir()
        {
            var sql = "DELETE FROM tb_Veiculos WHERE id=" + Id;
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fala ao excluir veículo!" + ex.Message);
            }
        }

        public void GetVeiculo(int id)
        {
            var sql = "SELECT nome, modelo, ano, fabricacao, cor, combustivel, automatico, valor," +
                "ativo FROM tb_Veiculos WHERE id=" + id;

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if(dr.Read())
                                {
                                    Id = id;
                                    Nome = dr["nome"].ToString();
                                    Modelo = dr["modelo"].ToString();
                                    Ano = Convert.ToInt16(dr["ano"]);
                                    Fabricacao = Convert.ToInt16(dr["fabricacao"]);
                                    Cor = dr["cor"].ToString();
                                    Combustivel = (Combustivel)Convert.ToByte(dr["combustivel"]);
                                    Automatico = (Automatico) Convert.ToByte(dr["automatico"]);
                                    Valor = Convert.ToDecimal(dr["valor"]);
                                    Ativo = Convert.ToBoolean(dr["ativo"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Nome = "Falha: " + ex.Message;
                Console.WriteLine("Falha ao atuliazar veiculo!" + ex.Message);
            }
        }
    }

    public enum Combustivel : int 
    {
        Gasolina = 1, 
        Álcool = 2, 
        Flex = 3, 
        Diesel = 4, 
        Gás = 5, 
        Eletricidade = 6
    }

    public enum Automatico : int
    {
        Não = 0,
        Sim = 1
    }
}