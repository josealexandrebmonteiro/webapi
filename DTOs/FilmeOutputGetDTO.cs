public class FilmeOutPutGetDTO {
    public long Id { get; set; }
    public string Titulo { get; set; }

    public FilmeOutPutGetDTO(long id, string titulo) {
        Id = id;
        Titulo = titulo;
    }
}