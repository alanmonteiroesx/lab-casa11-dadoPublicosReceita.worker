using DadosPublicosReceita.Shared.Domain;

namespace DadosPublicosReceita.Domain.Entities
{
    public class Endereco : Entity
    {
        public Endereco(
            string tipoLogradouro,
            string logradouro,
            string numero,
            string complemento,
            string bairro,
            string cep,
            string uf,
            string municipioId,
            string paisId,
            string estabelecimentoCnpjBasico,
            string estabelecimentoCnpjOrdem,
            string estabelecimentoCnpjDv)
        {
            TipoLogradouro = tipoLogradouro;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Uf = uf;
            MunicipioId = municipioId;
            PaisId = paisId;
            EstabelecimentoCnpjBasico = estabelecimentoCnpjBasico;
            EstabelecimentoCnpjOrdem = estabelecimentoCnpjOrdem;
            EstabelecimentoCnpjDv = estabelecimentoCnpjDv;
        }

        public string TipoLogradouro { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public string Uf { get; private set; }
        public string MunicipioId { get; private set; }
        public Municipio Municipio { get; private set; }
        public string PaisId { get; private set; }
        public Pais Pais { get; private set; }
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
