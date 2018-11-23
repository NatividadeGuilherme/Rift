using System.Collections.Generic;

namespace Rift.Models
{
    public interface ICliente
    {
        List<Cliente> RetornarTodosClientes();
        bool IncluirCliente(Cliente cliente);
        bool AlterarCliente(Cliente cliente);
        Cliente RetornarClientePorId(int idCliente);
        bool ExcluirCliente(int IdCliente);
        
    }
}
