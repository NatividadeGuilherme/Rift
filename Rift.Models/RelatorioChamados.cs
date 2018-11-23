using System;
using System.Collections.Generic;

namespace Rift.Models
{
    public class RelatorioChamados
    {
        public RelatorioChamados()
        {
            this.AcoesChamados = new List<AcoesChamado>();
        }

        public int IdCliente { get; set; }
        public int IdChamado { get; set; }
        public string Titulo { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataFechada { get; set; }
        public string Status { get; set; }
        public List<AcoesChamado> AcoesChamados { get; set; }
        public string NomeProduto { get; set; }
        public string NomeFantasia { get; set; }
        public string Nome { get; set; }
    }
    
}
