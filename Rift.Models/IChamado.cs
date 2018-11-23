
namespace Rift.Models
{
    public interface IChamado
    {
        void CriarChamado(int idColaborador, Chamado chamado);
        void AlterarChamado(Chamado chamado);
        Chamado ConsultarChamado(int idChamado);
        bool ExcluirChamado(int idChamado);

        bool FinalizarChamado(int idChamado);
    }

}
