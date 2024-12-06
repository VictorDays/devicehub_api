namespace devicehub_api.Entities
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ICollection<Ativo> Ativos { get; set; } = new List<Ativo>();
        public ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();

    }
}
