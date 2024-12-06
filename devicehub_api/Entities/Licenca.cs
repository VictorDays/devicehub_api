namespace devicehub_api.Entities
{
    public class Licenca
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime DataAquisicao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string Software { get; set; }
        public int AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}
