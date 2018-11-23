
using Dapper;
using Rift.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Rift.Help.DAL
{
    public class ProdutoDAL
    {
        readonly string conexao = ConfigurationManager.ConnectionStrings["RiftConnection"].ConnectionString;
        public bool CadastrarProduto(Produto produto)
        {
            var sqlConexao = new SqlConnection(conexao);

            string sql = @"Insert into Produto
                                      (NomeProduto) 
                               values (@NomeProduto)";
            sqlConexao.Open();
           var registrosAfetados=  sqlConexao.Execute(sql, new { @NomeProduto = produto.NomeProduto });
            sqlConexao.Close();
            return registrosAfetados >= 1;
        }
        public List<Produto> TodosProdutos()
        {
            var sqlConexao = new SqlConnection(conexao);

            sqlConexao.Open();
            var listadeProdutos = sqlConexao.Query<Produto>("Select * from Produto order by IdProduto Desc").ToList();
            sqlConexao.Close();
            return listadeProdutos;
        }

        public Produto BuscarProdutoPorId(int idProduto)
        {
            var sqlConexao = new SqlConnection(conexao);
            var produto = sqlConexao.Query<Produto>("Select * from Produto where IdProduto=@IdProduto", new {
            @IdProduto =idProduto }).FirstOrDefault();

            return produto;
        }

        public void AlterarProduto(Produto produto)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Update Produto 
                              set NomeProduto=
                                  @NomeProduto
                            where
                                  IdProduto=
                                  @IdProduto";
            sqlConexao.Open();
            sqlConexao.Execute(sql, new { @NomeProduto = produto.NomeProduto, produto.IdProduto });
            sqlConexao.Close();
        }
        public void ExcluirProduto(int idProduto)
        {
            var sqlConexao = new SqlConnection(conexao);
            sqlConexao.Open();
            sqlConexao.Execute("Delete from Produto where IdProduto=@IdProduto", new { @IdProduto = idProduto });
            sqlConexao.Close();
        }
    }
}