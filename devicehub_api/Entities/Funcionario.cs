namespace devicehub_api.Entities
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<Ativo> Ativos { get; set; } = new List<Ativo>();
    }
}
