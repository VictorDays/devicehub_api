using System.ComponentModel;

namespace devicehub_api.Entities
{
    public class Ativo
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime DataAquisicao { get; set; }
        public float Valor { get; set; }
        public string Localizacao { get; set; }
        public string Status { get; set; }
        public int? ResponsavelId { get; set; }
        public Funcionario Responsavel { get; set; }
        public int? DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public int? FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public ICollection<Licenca> Licencas { get; set; } = new List<Licenca>();
        public ICollection<Manutencao> Manutencoes { get; set; } = new List<Manutencao>();
        public Garantia Garantia { get; set; }
    }
}
