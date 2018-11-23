
using Dapper;
using Rift.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Rift.Help.DAL
{
    public class AcoesChamadoDAL
    {
        readonly string conexao = ConfigurationManager.ConnectionStrings["RiftConnection"].ConnectionString;
        public List<AcoesChamado> RetornarTodasAcoesChamado(int idChamado)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @" Select  *  
                             From   AcoesChamado 
                       Inner Join   Colaborador ON Colaborador.idColaborador= 
                                    AcoesChamado.IdColaborador 
                            Where   IdChamado=@IdChamado";
            sqlConexao.Open();
            var listaAcoes = sqlConexao.Query<AcoesChamado, Colaborador, AcoesChamado>(sql, (acoes, colaborador)=> 
            {
                acoes.Colaborador = colaborador;
                return acoes;

            }, new { @IdChamado=idChamado}, splitOn: "idColaborador");
            sqlConexao.Close();
            return listaAcoes.ToList();
        }
        public void IncluirAcoes(AcoesChamado acoes)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Insert Into AcoesChamado 
                                      (IdChamado
                                      ,Descricao
                                      ,IdColaborador
                                      ,DataCriacao
                                      ,Status) 
                                Values(@IdChamado
                                      ,@Descricao
                                      ,@IdColaborador
                                      ,GETDATE()
                                      ,@Status)";
            sqlConexao.Open();
            sqlConexao.Execute(sql, new { @IdChamado = acoes.IdChamado, @Descricao = acoes.Descricao,
            @IdColaborador=acoes.IdColaborador, @Status=acoes.Status});
            sqlConexao.Close();
        }

        public AcoesChamado RetornarAcao(int idAcao)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Select * From AcoesChamado
                                   Where IdAcoes=
                                        @IdAcoes";
            sqlConexao.Open();
            var acao = sqlConexao.Query<AcoesChamado>(sql, new { @IdAcoes = idAcao }).FirstOrDefault();
            sqlConexao.Close();
            return acao;
        }
        public void AlterarAcoes(AcoesChamado acoes)
        {
            var sqlConexao = new SqlConnection(conexao);

            string sql = @"Update AcoesChamado 
                              Set Descricao=
                                 @Descricao
                                 ,Status=
                                 @Status
                           Where IdAcoes=
                                 @IdAcoes";
            sqlConexao.Open();
            sqlConexao.Execute(sql, new { @Descricao = acoes.Descricao, @IdAcoes = acoes.IdAcoes, @Status=acoes.Status });
            sqlConexao.Close();
        }

        public bool ExcluirAcoes(int idAcoes)
        {
            var sqlConexao = new SqlConnection(conexao);

            sqlConexao.Open();

             var resultExclusao = sqlConexao.Execute("Delete From AcoesChamado Where IdAcoes=@IdAcoes",
                new { @IdAcoes = idAcoes });
            sqlConexao.Close();

            return resultExclusao >= 1;
        }
        public string RetornarStatusUltimaAcao(int IdChamado)
        {
            string status;
            AcoesChamado chamado = null;
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Select Status 
                            From  AcoesChamado
                           Where  IdAcoes= ( 
                           Select Max (IdAcoes) 
                            From  AcoesChamado
                           Where  IdChamado=
                                 @IdChamado) ";
            sqlConexao.Open();
            var resultado = sqlConexao.Query<AcoesChamado>(sql, new { @IdChamado = IdChamado }).FirstOrDefault();
            chamado = resultado;
            sqlConexao.Close();
            if (chamado == null)
                status = "";
            else
                status = chamado.Status;
            return status;
        }
        
    }
}