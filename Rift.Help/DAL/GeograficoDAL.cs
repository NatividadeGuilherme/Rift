using Dapper;
using Rift.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Rift.Help.DAL
{
    public class GeograficoDAL
    {
        readonly string conexao = ConfigurationManager.ConnectionStrings["RiftConnection"].ConnectionString;
        public List<Cidade> RetornarTodasCidades(string uf)
        {
            List<Cidade> listaCidade = new List<Cidade>();
            var sqlConexao = new SqlConnection(conexao);
            listaCidade = sqlConexao.Query<Cidade>(@"Select Nome 
                                                       from Cidade 
                                                      where Estado 
                                                  = (Select Id 
                                                       from Estado 
                                                      where Uf
                                                           =@Uf)", new { @Uf= uf }).ToList();
            return listaCidade;
             
        }
        public List<Estado> RetornarTodosEstados()
        {
            List<Estado> listaEstado = new List<Estado>();
            var sqlConexao = new SqlConnection(conexao);
            listaEstado = sqlConexao.Query<Estado>(@"Select Id
                                                           ,Nome
                                                           ,Uf
                                                       from Estado").ToList();
            return listaEstado;
        }
    }
}