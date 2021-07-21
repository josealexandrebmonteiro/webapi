using System.Collections.Generic;

public class DiretorListOutputGetAllDTO {
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public List<DiretorOutputGetDTO> Items { get; init; }
}

public class DiretorOutputGetDTO {
    public long Id { get; set; }
    public string Nome { get; set; }

    public DiretorOutputGetDTO(long id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}
