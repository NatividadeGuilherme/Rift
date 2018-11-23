
namespace Rift.Help.BLL
{
    public class MetodosMascara
    {
        public string RemoverMascaraCNPJ(string cnpj)
        {
            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            return cnpj;
        }
        public string RemoverMascaraTelefoneCel(string telefone)
        {
            telefone = telefone.Replace("(", "").Replace(")", "").Replace("-", "");
            return telefone;
        }
        public string RemoverMascaraCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");
            return cpf;
        }
        public string RemoverMascaraCep(string cep)
        {
            if (cep != "" && cep!=null)
            {
               cep = cep.Replace("-", "");
            }
            return cep;
        }

    }
}