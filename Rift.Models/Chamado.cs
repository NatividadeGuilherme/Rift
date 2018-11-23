using System;

namespace Rift.Models
{
    public class Chamado
    {
        public int IdChamado { get; set; }
        public string Titulo { get; set; }
        public int Cliente { get; set; }
        public int Produto { get; set; }
        public string Status { get; set; }
        public string UltimoStatusAcao { get; set; }
        public int Colaborador { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataFechada { get; set; }

        public Cliente Client { get; set; }
        public Produto Produt { get; set; }
        public Colaborador Colaborad { get; set; }
    }
}
