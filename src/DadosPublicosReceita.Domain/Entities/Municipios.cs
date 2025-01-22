using DadosPublicosReceita.Shared.Domain;

namespace DadosPublicosReceita.Domain.Entities
{
    public class Municipios : Entity
    {
        public Municipios(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
    }
}
