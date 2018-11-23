using Dapper;
using Rift.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Rift.Help.DAL
{
    public class ColaboradorDAL:IColaborador
    {
        readonly string conexao = ConfigurationManager.ConnectionStrings["RiftConnection"].ConnectionString;

        public bool Excluir(int IdColaborador)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Delete from Colaborador 
                                 where idColaborador=
                                      @IdColaborador";
            sqlConexao.Open();

            var registrosExcluidos = sqlConexao.Execute(sql, new { @IdColaborador = IdColaborador });
            sqlConexao.Close();
            return registrosExcluidos >= 1;

        }
        public List<Colaborador> RetornarTodosColaboradores()
        {
            var sqlConexao = new SqlConnection(conexao);
            sqlConexao.Open();
            var listaColaboradores = sqlConexao.Query<Colaborador>("Select * from Colaborador").ToList();
            sqlConexao.Close();
            return listaColaboradores;
        }
        public Colaborador RetornarColaboradorPorId(int IdColaborador)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Select * from Colaborador 
                                   where idColaborador
                                        =@idColaborador";
            sqlConexao.Open();
            var cliente = sqlConexao.Query<Colaborador>(sql, new { @idColaborador = IdColaborador }).FirstOrDefault();
            sqlConexao.Close();
            return cliente;

        }
        public bool IncluirColaborador(Colaborador colaborador)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Insert into Colaborador
                                      (Nome
                                      ,Email
                                      ,Senha
                                      ,Endereco
                                      ,Complemento
                                      ,Numero
                                      ,Bairro
                                      ,Cidade
                                      ,Cep
                                      ,Cpf
                                      ,Telefone
                                      ,Celular
                                      ,DataNascimento
                                      ,Uf)
                              values  (@Nome
                                      ,@Email
                                      ,@Senha
                                      ,@Endereco
                                      ,@Complemento
                                      ,@Numero
                                      ,@Bairro
                                      ,@Cidade
                                      ,@Cep
                                      ,@Cpf
                                      ,@Telefone
                                      ,@Celular
                                      ,@DataNascimento
                                      ,@Uf)";
            sqlConexao.Open();
            int registrosAfetados = sqlConexao.Execute(sql, new
            {
                @Nome = colaborador.Nome,
                @Email = colaborador.Email,
                @Senha = colaborador.Senha,
                @Endereco = colaborador.Endereco,
                @Complemento = colaborador.Complemento,
                @Numero = colaborador.Numero,
                @Bairro = colaborador.Bairro,
                @Cidade = colaborador.Cidade,
                @Cep = colaborador.Cep,
                @Cpf = colaborador.Cpf,
                @Telefone = colaborador.Telefone,
                @Celular = colaborador.Celular,
                @DataNascimento = colaborador.DataNascimento,
                @Uf = colaborador.Uf
            });
            sqlConexao.Close();
            return registrosAfetados >= 1;
        }public void Alterar(Colaborador colaborador)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Update Colaborador 
                              set Nome=
                                 @Nome
                                 ,Email=
                                 @Email
                                 ,Senha=
                                 @Senha
                                 ,Endereco=
                                 @Endereco
                                 ,Complemento=
                                 @Complemento
                                 ,Numero=
                                 @Numero
                                 ,Cidade=
                                 @Cidade
                                 ,Bairro=
                                 @Bairro
                                 ,Cep=
                                 @Cep
                                 ,Telefone=
                                 @Telefone
                                 ,Celular=
                                 @Celular
                                 ,DataNascimento=
                                 @DataNascimento
                                 ,Uf=
                                 @Uf
                          where
                                  idColaborador=@IdColaborador";
            sqlConexao.Open();
            sqlConexao.Execute(sql, new
            {
                @Nome = colaborador.Nome,
                @Email = colaborador.Email,
                @Senha = colaborador.Senha,
                @Endereco = colaborador.Endereco,
                @Complemento = colaborador.Complemento,
                @Numero = colaborador.Numero,
                @Cidade = colaborador.Cidade,
                @Bairro = colaborador.Bairro,
                @Cep = colaborador.Cep,
                @Telefone = colaborador.Telefone,
                @Celular = colaborador.Celular,
                @DataNascimento = colaborador.DataNascimento,
                @Uf = colaborador.Uf,
                @IdColaborador=colaborador.IdColaborador
            });
            sqlConexao.Close();
 
        }

        public Colaborador ValidarLogin(Colaborador colaborador)
        {
            var sqlConexao = new SqlConnection(conexao);
            string sql = @"Select * From Colaborador 
                                   Where Email=
                                        @Email
                                    and  Senha=
                                        @Senha";
            sqlConexao.Open();
            var funcionario = sqlConexao.Query<Colaborador>(sql, new
            {
                @Email = colaborador.Email, @Senha=colaborador.Senha
            }).FirstOrDefault();
            sqlConexao.Close();
            return funcionario;
        }

        public bool VerificarEmailCadastrado(string email)
        {
            var sqlConexao = new SqlConnection(conexao);

            string sql = @"Select * From 
                                         Colaborador 
                                   Where Email=@Email";

            sqlConexao.Open();
            var resultConsultaColaborador = sqlConexao.Query(sql, new { @Email = email }).FirstOrDefault();
            sqlConexao.Close();
            return resultConsultaColaborador != null;
        }

        public string RetornarSenha(string email)
        {
            string senha;
            var sqlConexao = new SqlConnection(conexao);

            string sql = @"Select Senha 
                             From Colaborador 
                            Where Email=@Email ";
            sqlConexao.Open();
            var existenciaEmail = VerificarEmailCadastrado(email);
            var resultado = sqlConexao.Query<Colaborador>(sql, new { @Email = email }).FirstOrDefault();
            senha = resultado.Senha;

            return senha;

        }
    }
}