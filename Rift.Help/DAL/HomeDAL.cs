

using Dapper;
using Rift.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Rift.Help.DAL
{
    public class HomeDAL
    {
        readonly string conexao = ConfigurationManager.ConnectionStrings["RiftConnection"].ConnectionString;

        public Home RetornarQuantidadeChamados()
        {
            string sql = "Select Count(IdChamado) As QuantidadeChamado From Chamado Where Status='Em andamento'";
            var  qntchamados = RealizarConsultaContadores(sql);
            return qntchamados;
        }


        private Home RealizarConsultaContadores(string sql)
        {
            var sqlConexao = new SqlConnection(conexao);

            sqlConexao.Open();

            var quantidadeChamados = sqlConexao.Query<Home>(sql).FirstOrDefault();
            sqlConexao.Close();
            return quantidadeChamados;
        }

        public Home RetornarQuantidadeClientes()
        {
            string sql = "Select Count(IdCliente) As QuantidadeClientes From Cliente";
            var qntClientes = RealizarConsultaContadores(sql);
            return qntClientes;
        }
        public Home RetornarQuantidadeColaborador()
        {
            string sql = "Select Count(IdColaborador) As QuantidadeColaboradores From Colaborador";
            var quantidadeColaboradores = RealizarConsultaContadores(sql);
            return quantidadeColaboradores;
        }
    }
}