using Rift.Help.DAL;
using Rift.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;


namespace Rift.Help.BLL
{
    public class ColaboradorBLL : IColaborador
    {
        ColaboradorDAL dalColaborador = new ColaboradorDAL();
        MetodosMascara metodosMascara = new MetodosMascara();

        public void Alterar(Colaborador colaborador)
        {
            if (colaborador.Telefone != null)
            {
                colaborador.Telefone = metodosMascara.RemoverMascaraTelefoneCel(colaborador.Telefone);
            }
            colaborador.Cep = metodosMascara.RemoverMascaraCep(colaborador.Cep);
            dalColaborador.Alterar(colaborador);
        }

        public bool Excluir(int IdColaborador)
        {
            var result = dalColaborador.Excluir(IdColaborador);
            return result;
        }

        public bool IncluirColaborador(Colaborador colaborador)
        {
            try
            {
                colaborador.Cpf = metodosMascara.RemoverMascaraCPF(colaborador.Cpf);
                if (colaborador.Telefone != null)
                {
                    colaborador.Telefone = metodosMascara.RemoverMascaraTelefoneCel(colaborador.Telefone);
                }
                colaborador.Celular = metodosMascara.RemoverMascaraTelefoneCel(colaborador.Celular);
                colaborador.Cep = metodosMascara.RemoverMascaraCep(colaborador.Cep);
                var registrosAfetados = dalColaborador.IncluirColaborador(colaborador);
                return registrosAfetados;

            }catch(SqlTypeException)
            {
                throw new SqlTypeException("Por gentileza informe uma data correta!");
            }
        }

        public Colaborador RetornarColaboradorPorId(int IdColaborador)
        {
            
            var colaborador = dalColaborador.RetornarColaboradorPorId(IdColaborador);
            if (colaborador == null)
            {
                throw new NullReferenceException("Colaborador não encontrado");
            }
            return colaborador;
        }

        public string RetornarSenha(string email)
        {
            string password="";
            var verificarExistenciaEmail = VerificarEmailCadastrado(email);

            if (verificarExistenciaEmail)
            {
                password = dalColaborador.RetornarSenha(email);
            }
            return password;
        }

        public List<Colaborador> RetornarTodosColaboradores()
        {
            var listaColaboradores = dalColaborador.RetornarTodosColaboradores();
            return listaColaboradores;
        }

        public Colaborador ValidarLogin(Colaborador colaborador)
        {
            var colaboradorAutenticado = dalColaborador.ValidarLogin(colaborador);
            if (colaboradorAutenticado != null)
            {
                return colaboradorAutenticado;
            }
            else
            {
                throw new NullReferenceException("E-mail ou senha inválido!");
            }

        }

        public bool VerificarEmailCadastrado(string email)
        {
            var resultVerificacao = dalColaborador.VerificarEmailCadastrado(email);

            return resultVerificacao;
        }
    }
}