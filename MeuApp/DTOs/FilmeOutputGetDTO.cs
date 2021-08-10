using System.Collections.Generic;

public class FilmeListOutputGetAllDTO {
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public List<FilmeOutPutGetDTO> Items { get; init; }
}


public class FilmeOutPutGetDTO {
    public long Id { get; set; }
    public string Titulo { get; set; }

    public FilmeOutPutGetDTO(long id, string titulo) {
        Id = id;
        Titulo = titulo;
    }
}