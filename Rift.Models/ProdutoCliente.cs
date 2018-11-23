namespace Rift.Models
{
    public class ProdutoCliente
    {
        public int IdProdutoCliente { get; set; }
        public int IdProduto { get; set; }
        public int IdCliente { get; set; }

        public Produto Produto { get; set; }
        public Cliente Cliente { get; set; }

        public string Status { get; set; }
    }
}
