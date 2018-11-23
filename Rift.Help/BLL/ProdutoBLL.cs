using Rift.Help.DAL;
using Rift.Models;
using System;
namespace Rift.Help.BLL
{
    public class ProdutoBLL
    {
        public bool CadastrarProduto(Produto produto)
        {

            var dalProduto = new ProdutoDAL();

            var result = dalProduto.CadastrarProduto(produto);

            if (result != true)
            {
                throw new Exception("Produto já cadastrado!");
            }
            return result;
        }
        public void ExcluirProduto(int idProduto)
        {
            try
            {
                var dalProduto = new ProdutoDAL();
                dalProduto.ExcluirProduto(idProduto);
            }
            catch (Exception)
            {
                throw new Exception("Impossível apagar este produto, pois o mesmo está" +
                    " sendo vinculado a um cliente!");
            }
        }

        public void AlterarProduto(Produto produto)
        {
            try
            {
                var dalProduto = new ProdutoDAL();
                dalProduto.AlterarProduto(produto);
            }
            catch(Exception e)
            {
                throw new Exception("Já existe um outro produto com este nome!");
            }    
        }
    }
}