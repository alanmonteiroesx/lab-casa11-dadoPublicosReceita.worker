namespace DadosPublicosReceita.Domain.Entities
{
    public class Municipio
    {
        public Municipio(string codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
        }

        public string Codigo { get; private set; }
        public string Nome { get; private set; }
    }
}
