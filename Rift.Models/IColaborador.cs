
using System.Collections.Generic;


namespace Rift.Models
{
    public interface IColaborador
    {
        Colaborador RetornarColaboradorPorId(int IdColaborador);
        bool IncluirColaborador(Colaborador colaborador);
        List<Colaborador> RetornarTodosColaboradores();
        void Alterar(Colaborador colaborador);
        bool Excluir(int IdColaborador);
        bool VerificarEmailCadastrado(string email);
        string RetornarSenha(string email);
       


    }
}
