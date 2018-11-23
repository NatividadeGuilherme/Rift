

using Dapper;
using Rift.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Rift.Help.DAL
{
    public class ProdutoClienteDAL: IProdutoCliente
    {
        readonly string conexao = ConfigurationManager.ConnectionStrings["RiftConnection"].ConnectionString;
        public bool InserirProdutoCliente(ProdutoCliente prodCli)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Insert into ProdutoCliente 
                                       (IdProduto
                                       ,IdCliente)
                                values (@IdProduto
                                       ,@IdCliente)";
            sqlConexao.Open();
            var registroInclusos = sqlConexao.Execute(sql, new
            {
                @IdProduto = prodCli.IdProduto,
                @IdCliente = prodCli.IdCliente
            });
            sqlConexao.Close();
            return registroInclusos >= 1;
        }
        public List<ProdutoCliente> RetornarProdutosAtivos(int idCliente)
        {
            var sqlConection = new SqlConnection(conexao);
            string sql = @"Select *
                             From ProdutoCliente 
                       Inner Join Produto
                               On ProdutoCliente.IdProduto = Produto.IdProduto
                       Inner Join Cliente 
                               On Cliente.IdCliente = ProdutoCliente.IdCliente
                            Where ProdutoCliente.IdCliente = @IdCliente
                            And   Status='ATIVO' ";

            sqlConection.Open();


            var listaDeProdutosCliente = sqlConection.Query<ProdutoCliente, Produto, Cliente, ProdutoCliente>(
                sql, (produtoCliente, produto, cliente) =>
                {
                    produtoCliente.Produto = produto;
                    produtoCliente.Cliente = cliente;

                    return produtoCliente;
                }, new { IdCliente = idCliente }, splitOn: "IdProdutoCliente,IdProduto,IdCliente").ToList();


            sqlConection.Close();
            return listaDeProdutosCliente;

        }
        public List<ProdutoCliente> RetornarProdutosPorCliente(int idCliente)
        {
            var sqlConection = new SqlConnection(conexao);
            string sql = @"Select *
                             From ProdutoCliente 
                       Inner Join Produto
                               On ProdutoCliente.IdProduto = Produto.IdProduto
                       Inner Join Cliente 
                               On Cliente.IdCliente = ProdutoCliente.IdCliente
                            Where ProdutoCliente.IdCliente = @IdCliente ";

            sqlConection.Open();


            var listaDeProdutosCliente = sqlConection.Query<ProdutoCliente, Produto, Cliente, ProdutoCliente>(
                sql, (produtoCliente, produto, cliente) =>
                {
                    produtoCliente.Produto = produto;
                    produtoCliente.Cliente = cliente;

                    return produtoCliente;
                }, new { IdCliente = idCliente }, splitOn: "IdProduto,IdCliente").ToList();


            sqlConection.Close();
            return listaDeProdutosCliente;
        }
        public ProdutoCliente RetornarStatusProduto(int IdProdutoCliente)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Select Status 
                             From ProdutoCliente
                            Where IdProdutoCliente=
                                  @IdProdutoCliente";
            sqlConexao.Open();
            var statusProduto = sqlConexao.Query<ProdutoCliente>(sql, new { @IdProdutoCliente = IdProdutoCliente }).FirstOrDefault();
            sqlConexao.Close();
            return statusProduto;
        }
        public void ActivateProdutoCliente(int IdProdutoCliente, string Status)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Update ProdutoCliente 
                              Set Status=
                                  @Status
                            Where IdProdutoCliente=
                                  @IdProdutoCliente";
            sqlConexao.Open();
            sqlConexao.Execute(sql, new { @Status = Status, @IdProdutoCliente = IdProdutoCliente });
            sqlConexao.Close();
        }

        public bool RemoverProdutoCliente(int idProdutoCliente)
        {
            var sqlConexao = new SqlConnection(conexao);

            string sql = @"Delete From ProdutoCliente 
                                 Where IdProdutoCliente = @IdProdutoCliente";
            sqlConexao.Open();
            var resultExclusao = sqlConexao.Execute(sql, new { @IdProdutoCliente = idProdutoCliente });
            sqlConexao.Close();

            return resultExclusao >= 1;
        }
    }
}