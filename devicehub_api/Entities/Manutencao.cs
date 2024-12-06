namespace devicehub_api.Entities
{
    public class Manutencao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public float Custo { get; set; }
        public int AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}
