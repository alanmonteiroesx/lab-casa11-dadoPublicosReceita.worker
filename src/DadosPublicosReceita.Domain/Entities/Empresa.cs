using DadosPublicosReceita.Domain.Enums;

namespace DadosPublicosReceita.Domain.Entities
{
    public class Empresa
    {
        public Empresa(
            string cnpjBasico,
            string razaoSocial, 
            string naturezaJuridica, 
            string qualificacaoResponsavel, 
            decimal capitalSocial, 
            string enteFederativoResp, 
            EPorteEmpresaType porteEmpresa)
        {
            CnpjBasico = cnpjBasico;
            RazaoSocial = razaoSocial;
            NaturezaJuridica = naturezaJuridica;
            QualificacaoResponsavel = qualificacaoResponsavel;
            CapitalSocial = capitalSocial;
            EnteFederativoResp = enteFederativoResp;
            PorteEmpresa = porteEmpresa;
            _estabelecimentos = new List<Estabelecimento>();
        }

        private readonly List<Estabelecimento> _estabelecimentos;
        public string CnpjBasico { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NaturezaJuridica { get; private set; }
        public string QualificacaoResponsavel { get; private set; }
        public decimal CapitalSocial { get; private set; }
        public string EnteFederativoResp  { get; private set; }
        public EPorteEmpresaType PorteEmpresa { get; private set; }
        public IReadOnlyCollection<Estabelecimento> Estabelecimentos => _estabelecimentos.AsReadOnly();

        public void AdicionarEstabelecimento(Estabelecimento estabelecimento)
        {
            _estabelecimentos.Add(estabelecimento);
        }

    }
}
