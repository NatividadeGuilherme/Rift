using Dapper;
using Rift.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Rift.Help.DAL
{
    public class ChamadoDAL:IChamado
    {
        readonly string conexao = ConfigurationManager.ConnectionStrings["RiftConnection"].ConnectionString;
        public List<Chamado> RetornarTodosChamadosPendentes(int idProduto=0, int idCliente=0)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Select * 
                            From  Chamado 
                      Inner Join 
                                  Produto 
                              On  Produto.IdProduto=
                                  Chamado.Produto
                      Inner Join  
                                  Cliente 
                            On    Cliente.IdCliente=
                                  Chamado.Cliente 
                      Inner Join  
                                  Colaborador
                            On    Colaborador.idColaborador=
                                  Chamado.Colaborador
                            ";
            if (idProduto > 0)
            {
                sql += " Where Produto.IdProduto=@IdProduto";
            }
            if (idCliente > 0)
            {
                sql += " And Cliente.IdCliente = @IdCliente";
            }

            sqlConexao.Open();

            var listaChamados = sqlConexao.Query<Chamado, Produto, Cliente, Colaborador ,Chamado>(sql,
                (chamado, produto, cliente, colaborador) =>
                {
                    var acoesChamado = new AcoesChamadoDAL();
                    chamado.UltimoStatusAcao = acoesChamado.RetornarStatusUltimaAcao(chamado.IdChamado);
                    chamado.Produt = produto;
                    chamado.Client = cliente;
                    chamado.Colaborad = colaborador;

                    return chamado;

                }, new { @IdProduto=idProduto, @IdCliente=idCliente} ,splitOn: "IdProduto,IdCliente,idColaborador");
            sqlConexao.Close();
            return listaChamados.ToList();

        }
        public void CriarChamado(int idColaborador,Chamado chamado)
        {
            var sqlConexao = new SqlConnection(conexao);

            string sql = @"Insert Into 
                                       Chamado 
                                      (Titulo
                                      ,Cliente
                                      ,Produto
                                      ,Colaborador
                                      

                                      )
                               Values (@Titulo
                                      ,@Cliente
                                      ,@Produto
                                      ,@Colaborador
                                
                                      )";
            sqlConexao.Open();
            var resultInclusao = sqlConexao.Execute(sql, new
            {
                @Titulo = chamado.Titulo,
                @Cliente = chamado.Cliente,
                @Produto = chamado.Produto,
                @Colaborador = idColaborador,
                @DataAbertura= chamado.DataAbertura,
                @DataFechada = chamado.DataFechada,
                @Status = chamado.Status
            });
            sqlConexao.Close();
        }
        public void AlterarChamado(Chamado chamado)
        {
            var sqlConexao = new SqlConnection(conexao);

            string sql = @"Update Chamado 
                              Set Titulo=
                                  @Titulo
                                 ,Status=
                                  @Status
                                 ,DataFechada=
                                  @DataFechada
                           Where  IdChamado=
                                  @IdChamado";
            sqlConexao.Open();
            sqlConexao.Execute(sql, new
            {
                @Titulo = chamado.Titulo,
                @Status = chamado.Status,
                @DataFechada = chamado.DataFechada,
                @IdChamado = chamado.IdChamado
            });
            sqlConexao.Close();
        }

        public Chamado ConsultarChamado(int idChamado)
        {
            var sqlConexao = new SqlConnection(conexao);

            string sql = @"Select * From Chamado 
                                   Where IdChamado=
                                         @IdChamado";
            sqlConexao.Open();
            var chamado = sqlConexao.Query<Chamado>(sql, new { @IdChamado = idChamado }).FirstOrDefault();
            sqlConexao.Close();
            return chamado;
        }

        public bool ExcluirChamado(int idChamado)
        {
            var sqlConexao = new SqlConnection(conexao);

            sqlConexao.Open();

            var resultExclusao = sqlConexao.Execute("Delete From Chamado Where IdChamado=@IdChamado", new
            { @IdChamado= idChamado});
            sqlConexao.Close();
            return resultExclusao >= 1;
        }

        public bool FinalizarChamado(int idChamado)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Update Chamado 
                              Set Status='Concluída',
                                  DataFechada = GETDATE()
                            Where IdChamado=@IdChamado";
            sqlConexao.Open();
            var chamadoFinalizado = sqlConexao.Execute(sql, new { @IdChamado = idChamado });
            sqlConexao.Close();
            return chamadoFinalizado >= 1;
        }
    }
}