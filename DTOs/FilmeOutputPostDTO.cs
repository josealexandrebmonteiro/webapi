public class FilmeOutputPostDTO {
    public int Id { get; set; }
    public string Titulo { get; set; }       
    public long DiretorID { get; set; }
    
    public FilmeOutputPostDTO(int id, string titulo, long diretorid) {
        Id = id;
        Titulo = titulo;
        DiretorID = diretorid;
    }
}