
using System;

namespace Rift.Models
{
    public class AcoesChamado
    {
        public int IdAcoes { get; set; }
        public int IdChamado { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public int IdColaborador { get; set; }
        public DateTime DataCriacao { get; set; }
        public Colaborador Colaborador { get; set; }
    }
}
