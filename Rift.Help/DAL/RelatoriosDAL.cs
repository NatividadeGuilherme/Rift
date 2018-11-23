using Dapper;
using Rift.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Rift.Help.DAL
{
    public class RelatoriosDAL
    {
        readonly string conexao = ConfigurationManager.ConnectionStrings["RiftConnection"].ConnectionString;
        public List<RelatorioChamados> ObterRelatorioChamados(DateTime dataInicial, DateTime dataFinal, int codigoDoCliente, int codigoDoProduto)
        {
            var sqlConexao = new SqlConnection(conexao);

            string sql = @"Select 
                                  ColaboradorAcao.Nome  
                                , Chamado.IdChamado
                                , Chamado.Titulo
                                , Chamado.DataAbertura
                                , Chamado.DataFechada
                                , Chamado.Status
                                , Produto.NomeProduto
                                , Cliente.NomeFantasia
                                , Colaborador.Nome
                                , AcoesChamado.*
                           From Chamado
                     Inner Join AcoesChamado
                             On AcoesChamado.IdChamado = Chamado.IdChamado
                     Inner Join Colaborador ColaboradorAcao
                             On ColaboradorAcao.idColaborador = AcoesChamado.IdColaborador 
                     Inner Join Produto 
                             On Chamado.Produto = Produto.IdProduto
                     Inner Join Cliente 
                             On Chamado.Cliente = Cliente.IdCliente
                     Inner Join Colaborador 
                             On Colaborador.idColaborador=Chamado.Colaborador 
                          Where Chamado.DataAbertura 
                        Between @DataInicial
                            And @DataFinal ";

            if (codigoDoCliente > 0)
            {
                sql += " And Chamado.Cliente = @Cliente";
            }

            if (codigoDoProduto > 0)
            {
                sql += " And Chamado.Produto = @Produto";
            }

            sqlConexao.Open();


            var lookup = new Dictionary<int, RelatorioChamados>();

            var dados = sqlConexao.Query<RelatorioChamados, AcoesChamado,  RelatorioChamados>(
                sql, (relatorio, acoes) =>
                {
                        var colaboradorDaAcao = new ColaboradorDAL();
                        var colaborador = colaboradorDaAcao.RetornarColaboradorPorId(acoes.IdColaborador);
                    //    acoes.Colaborador = nomeColaboradorDaAcao;
                    acoes.Colaborador = colaborador;
                    RelatorioChamados RelatorioChamados = new RelatorioChamados();

                    if (!lookup.TryGetValue(relatorio.IdChamado, out RelatorioChamados))
                        lookup.Add(relatorio.IdChamado, RelatorioChamados = relatorio);

                    if (acoes != null)
                        RelatorioChamados.AcoesChamados.Add(acoes);

                    return relatorio;
                }, new { DataInicial = dataInicial, DataFinal = dataFinal, Cliente = codigoDoCliente, Produto = codigoDoProduto }, splitOn: "IdAcoes");

           
            sqlConexao.Close();

            return lookup.Values.ToList();
        } 
    }
}