using Rift.Help.DAL;
using Rift.Models;
using System;
using System.Data.SqlTypes;

namespace Rift.Help.BLL
{
    public class ChamadoBLL : IChamado
    {
        public void AlterarChamado(Chamado chamado)
        {
            try
            {
                var dalChamado = new ChamadoDAL();
                dalChamado.AlterarChamado(chamado);

            }
            catch (SqlTypeException e)
            {
                throw new SqlTypeException("Formato de data inválido");
            }   
        }

        public Chamado ConsultarChamado(int idChamado)
        {
            var dalChamado = new ChamadoDAL();
            var chamado = dalChamado.ConsultarChamado(idChamado);
            if (chamado == null)
            {
                throw new NullReferenceException("Codígo de chamado não encontrado");
            }
            return chamado;
        }

        public void CriarChamado(int idColaborador, Chamado chamado)
        {
            var dalChamado = new ChamadoDAL();
            dalChamado.CriarChamado(idColaborador,chamado);

        }

        public bool ExcluirChamado(int idChamado)
        {
            try
            {
                var dalChamado = new ChamadoDAL();
                var result = dalChamado.ExcluirChamado(idChamado);
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Este chamado possui ações!");
            }
            
        }

        public bool FinalizarChamado(int idChamado)
        {
            var dalChamado = new ChamadoDAL();
            return dalChamado.FinalizarChamado(idChamado);
        }
    }
}