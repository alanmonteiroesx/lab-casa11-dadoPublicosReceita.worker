using DadosPublicosReceita.Shared.Domain;

namespace DadosPublicosReceita.Domain.Entities
{
    public class Telefone : Entity
    {
        public Telefone(
            string codigo,
            string numero,
            string estabelecimentoCnpjBasico,
            string estabelecimentoCnpjOrdem,
            string estabelecimentoCnpjDv)
        {
            Codigo = codigo;
            Numero = numero;
            EstabelecimentoCnpjBasico = estabelecimentoCnpjBasico;
            EstabelecimentoCnpjOrdem = estabelecimentoCnpjOrdem;
            EstabelecimentoCnpjDv = estabelecimentoCnpjDv;
        }

        public string Codigo { get; private set; }
        public string Numero { get; private set; }
        public string EstabelecimentoCnpjBasico { get; private set; }
        public string EstabelecimentoCnpjOrdem { get; private set; }
        public string EstabelecimentoCnpjDv { get; private set; }
        public Estabelecimento Estabelecimento { get; private set; }

        public void SetEstabelecimento(string cnpjBasico, string cnpjOrdem, string cnpjDv)
        {
            EstabelecimentoCnpjBasico = cnpjBasico;
            EstabelecimentoCnpjOrdem = cnpjOrdem;
            EstabelecimentoCnpjDv = cnpjDv;
        }
    }
}
