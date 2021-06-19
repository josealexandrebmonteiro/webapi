public class Filme {

    public Filme (string titulo) {
        Titulo = titulo;
    }

    public int Id { get; set;}
    public string Titulo { get; set; } // é uma property por isso começa com T Maisculo 
    public string Ano { get; set; }
    public string Genero { get; set; }
    public long DiretorID { get; set; }

    public Diretor Diretor { get; set; }
    
}