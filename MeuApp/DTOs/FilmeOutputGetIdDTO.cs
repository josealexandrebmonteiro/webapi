public class FilmeOutputGetIdDTO {
    public long Id { get; set; }
    public string Titulo { get; set; }
    public string NomeDoDiretor { get; set; }

    public FilmeOutputGetIdDTO (long id, string titulo, string nomeDoDiretor) {
        Id = id;
        Titulo = titulo;
        NomeDoDiretor = nomeDoDiretor;
    }
}