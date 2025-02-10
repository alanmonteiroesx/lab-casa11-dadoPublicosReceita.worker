using DadosPublicosReceita.Domain.Enums;

namespace DadosPublicosReceita.Domain.Entities
{
    public class Estabelecimento
    {
        public Estabelecimento(
            string cnpjBasico,
            string cnpjOrdem,
            string cnpjDv,
            EEstabelecimentoType tipoDoEstabelecimento,
            string? nomeFantasia,
            DateOnly dataInicioAtividade,
            ESituacaoCadastralType situacaoCadastral,
            DateOnly dataSituacaoCadastral,
            string motivoSituacaoCadastral,
            string? situacaoEspecial,
            DateOnly? dataSituacaoEspecial,
            string email,
            string cnaePrincipalId)
        {
            _telefones = new List<Telefone>();

            CnpjBasico = cnpjBasico;
            CnpjOrdem = cnpjOrdem;
            CnpjDv = cnpjDv;
            TipoDoEstabelecimento = tipoDoEstabelecimento;
            NomeFantasia = nomeFantasia;
            DataInicioAtividade = dataInicioAtividade;
            SituacaoCadastral = situacaoCadastral;
            DataSituacaoCadastral = dataSituacaoCadastral;
            MotivoSituacaoCadastral = motivoSituacaoCadastral;
            SituacaoEspecial = situacaoEspecial;
            DataSituacaoEspecial = dataSituacaoEspecial;
            Email = email;
            CnaePrincipalId = cnaePrincipalId;
        }

        private readonly List<Telefone> _telefones;
        public string CnpjBasico { get; private set; }
        public string CnpjOrdem { get; private set; }
        public string CnpjDv { get; private set; }
        public EEstabelecimentoType TipoDoEstabelecimento { get; private set; }
        public string NomeFantasia { get; private set; }
        public DateOnly DataInicioAtividade { get; private set; }
        public ESituacaoCadastralType SituacaoCadastral { get; private set; }
        public DateOnly DataSituacaoCadastral { get; private set; }
        public string MotivoSituacaoCadastral { get; private set; }
        public string SituacaoEspecial { get; private set; }
        public DateOnly? DataSituacaoEspecial { get; private set; }
        public string Email { get; private set; }
        public string CnaePrincipalId { get; private set; }
        public Cnae CnaePrincipal { get; private set; }
        public Guid EnderecoId { get; private set; }
        public Endereco Endereco { get; private set; }
        public string? EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }
        public IReadOnlyCollection<Telefone> Telefones => _telefones.AsReadOnly();

        public void VincularEmpresa(Empresa empresa)
        {
            Empresa = empresa;
            EmpresaId = empresa.CnpjBasico;
        }

        public void AdicionarTelefone(Telefone telefone)
        {
            _telefones.Add(telefone);
        }

        public void SetEndereco(Endereco endereco)
        {
            Endereco = endereco;
            EnderecoId = endereco.Id;
        }

    }
}
