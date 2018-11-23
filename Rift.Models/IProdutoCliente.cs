

namespace Rift.Models
{
    public interface IProdutoCliente
    {
        bool InserirProdutoCliente(ProdutoCliente prodCli);
        void ActivateProdutoCliente(int IdProdutoCliente, string Status);
        ProdutoCliente RetornarStatusProduto(int IdProdutoCliente);
    }
}
