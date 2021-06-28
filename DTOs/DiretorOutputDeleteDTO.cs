public class DiretorOutputDeleteDTO {
    public long Id { get; set; }
    public string Nome { get; set; }

    public DiretorOutputDeleteDTO (long id, string nome) {
        Id = id;
        Nome = nome;
    }
}