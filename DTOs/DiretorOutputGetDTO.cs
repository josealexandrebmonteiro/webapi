public class DiretorOutputGetDTO {
    public long Id { get; set; }
    public string Nome { get; set; }

    public DiretorOutputGetDTO (long id, string nome) {
        Id = id;
        Nome = nome;
    }
}