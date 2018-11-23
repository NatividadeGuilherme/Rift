

using Rift.Help.DAL;
using Rift.Models;
using System;

namespace Rift.Help.BLL
{
    public class ProdutoClienteBLL : IProdutoCliente
    {
        ProdutoClienteDAL dalProdutoCliente = new ProdutoClienteDAL();
        public void ActivateProdutoCliente(int IdProdutoCliente, string Status)
        {
            string statusProdutoCliente;
            if (Status == "ATIVO")
            {
                statusProdutoCliente = "Inativo";
            }
            else
            {
                statusProdutoCliente = "ATIVO";
            }
            dalProdutoCliente.ActivateProdutoCliente(IdProdutoCliente, statusProdutoCliente);
        }

        public bool InserirProdutoCliente(ProdutoCliente prodCli)
        {
           
                var resultadoInserir = dalProdutoCliente.InserirProdutoCliente(prodCli);
 
                return resultadoInserir;
        }

        public ProdutoCliente RetornarStatusProduto(int IdProdutoCliente)
        {
            var produtoCliente = dalProdutoCliente.RetornarStatusProduto(IdProdutoCliente);
            return produtoCliente;
        }
        public bool RemoverProdutoCliente(int idProdutoCliente)
        {
            var resultExclusao = dalProdutoCliente.RemoverProdutoCliente(idProdutoCliente);
            if (resultExclusao==false)
            {
                throw new Exception("Não é possível excluir este Produto, pois ele possui chamado");
            }
            else
            {
                return true;
            }
        }
    }
}