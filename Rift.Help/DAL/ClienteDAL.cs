

using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Rift.Models;

namespace Rift.Help.DAL
{
    public class ClienteDAL : ICliente
    {
        readonly string conexao = ConfigurationManager.ConnectionStrings["RiftConnection"].ConnectionString;
        public List<Cliente> RetornarTodosClientes()
        {
            var listaClientes = new List<Cliente>();
            var sqlConexao = new SqlConnection(conexao);

            listaClientes = sqlConexao.Query<Cliente>("Select IdCliente, NomeFantasia, Cnpj, Telefone from Cliente").ToList();
            
            return listaClientes;
        }
        public bool IncluirCliente(Cliente cliente)
        {
            var sqlConexao = new SqlConnection(conexao);
            
            string sqlComando = @"Insert 
                                    into Cliente
                                        (RazaoSocial
                                        ,NomeFantasia
                                        ,Cnpj
                                        ,Email
                                        ,Endereco
                                        ,Complemento
                                        ,Numero
                                        ,Uf
                                        ,Cidade
                                        ,Bairro
                                        ,Cep
                                        ,Telefone)
                                 Values (@RazaoSocial
                                        ,@NomeFantasia
                                        ,@Cnpj
                                        ,@Email
                                        ,@Endereco
                                        ,@Complemento
                                        ,@Numero
                                        ,@Uf
                                        ,@Cidade
                                        ,@Bairro
                                        ,@Cep
                                        ,@Telefone)";
            sqlConexao.Open();
            int registroAfetados = sqlConexao.Execute(sqlComando, new
            {
                @RazaoSocial = cliente.RazaoSocial,
                @NomeFantasia = cliente.NomeFantasia,
                @Cnpj = cliente.Cnpj,
                @Email = cliente.Email,
                @Endereco = cliente.Endereco,
                @Complemento = cliente.Complemento,
                @Numero = cliente.Numero,
                @Uf= cliente.Uf,
                @Cidade = cliente.Cidade,
                @Bairro = cliente.Bairro,
                @Cep = cliente.Cep,
                @Telefone = cliente.Telefone
            });
            sqlConexao.Close();
            return registroAfetados >= 1;
        }
        public bool AlterarCliente(Cliente cliente)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sqlComando = @"Update Cliente
                                    set  RazaoSocial
                                        =@RazaoSocial
                                        ,NomeFantasia
                                        =@NomeFantasia
                                        ,Email
                                        =@Email
                                        ,Endereco
                                        =@Endereco
                                        ,Complemento
                                        =@Complemento
                                        ,Numero
                                        =@Numero
                                        ,Uf
                                        =@Uf
                                        ,Cidade
                                        =@Cidade
                                        ,Bairro
                                        =@Bairro
                                        ,Cep
                                        =@Cep
                                        ,Telefone
                                        =@Telefone
                                 where   IdCliente
                                        =@IdCliente";
            sqlConexao.Open();
            int registrosAfetados = sqlConexao.Execute(sqlComando, new
            {
                @RazaoSocial = cliente.RazaoSocial,
                @NomeFantasia = cliente.NomeFantasia,
                @Email = cliente.Email,
                @Endereco = cliente.Endereco,
                @Complemento = cliente.Complemento,
                @Numero = cliente.Numero,
                @Uf = cliente.Uf,
                @Cidade = cliente.Cidade,
                @Bairro = cliente.Bairro,
                @Cep = cliente.Cep,
                @Telefone = cliente.Telefone,
                @IdCliente = cliente.IdCliente
            });
            sqlConexao.Close();
            return registrosAfetados>=1;
        }
        public Cliente RetornarClientePorId(int idCliente)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sqlComando = @"Select * 
                                    from Cliente
                                   where IdCliente = @IdCliente";
            sqlConexao.Open();
            Cliente cliente = sqlConexao.Query<Cliente>(sqlComando, new { @IdCliente = idCliente }).FirstOrDefault();
            sqlConexao.Close();
            return cliente;
        }

        public bool ExcluirCliente(int IdCliente)
        {
            var sqlConexao = new SqlConnection(conexao);
            sqlConexao.Open();
            int registrosApagados = sqlConexao.Execute(@"Delete 
                                                           from Cliente 
                                                          where IdCliente = @IdCliente", new { @IdCliente = IdCliente });
            sqlConexao.Close();

            return registrosApagados >= 1;
        }

        
    }
}