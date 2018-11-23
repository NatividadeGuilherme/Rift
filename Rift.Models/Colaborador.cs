

using System;

namespace Rift.Models
{
    public class Colaborador
    {
        public int  IdColaborador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Uf { get; set; }

    }
}
