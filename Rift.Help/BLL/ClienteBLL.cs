using Rift.Help.DAL;
using Rift.Models;
using System;
using System.Collections.Generic;

namespace Rift.Help.BLL
{
    public class ClienteBLL : ICliente
    {
        ClienteDAL clienteDAL = null;
        readonly MetodosMascara metodosMascara = new MetodosMascara();
        public bool IncluirCliente(Cliente cliente)
        {
                clienteDAL = new ClienteDAL();
                cliente.Cnpj = metodosMascara.RemoverMascaraCNPJ(cliente.Cnpj);
                cliente.Telefone = metodosMascara.RemoverMascaraTelefoneCel(cliente.Telefone);
                cliente.Cep = metodosMascara.RemoverMascaraCep(cliente.Cep);
                var incluirCliente = clienteDAL.IncluirCliente(cliente);
                return incluirCliente;      
        }

        public List<Cliente> RetornarTodosClientes()
        {
            clienteDAL = new ClienteDAL();
            var listaClientes = clienteDAL.RetornarTodosClientes();
            return listaClientes;
        }
       
        public bool AlterarCliente(Cliente cliente)
        {
            var dalAlterarCliente = new ClienteDAL();
            cliente.Telefone = metodosMascara.RemoverMascaraTelefoneCel(cliente.Telefone);
            cliente.Cep = metodosMascara.RemoverMascaraCep(cliente.Cep);
            bool registroAfetados = dalAlterarCliente.AlterarCliente(cliente);
            return registroAfetados;
        }

        public Cliente RetornarClientePorId(int idCliente)
        {
            var dalConsultarCLientePorId = new ClienteDAL();
            var cliente = dalConsultarCLientePorId.RetornarClientePorId(idCliente);

            return cliente;

        }

        public bool ExcluirCliente(int IdCliente)
        {
            var dalExcluirCliente = new ClienteDAL();
            var registroConcluidos = dalExcluirCliente.ExcluirCliente(IdCliente);
            return registroConcluidos;
        }

    }
}