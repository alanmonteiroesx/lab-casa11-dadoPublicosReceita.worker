namespace DadosPublicosReceita.Domain.Entities
{
    public class Cnae
    {
        public Cnae(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
    }
}
