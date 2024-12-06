namespace devicehub_api.Entities
{
    public class Garantia
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}
