public class DiretorOutputGetIdDTO {
    public long Id { get; set; }
    public string Nome { get; set; }

    public DiretorOutputGetIdDTO (long id, string nome) {
        Id = id;
        Nome = nome;
    }
}