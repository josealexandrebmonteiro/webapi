using FluentValidation;

public class FilmeInputPutDTO {
    public long Id { get; set; }
    public string Titulo { get; set; }
    public long DiretorId { get; set; }
    public FilmeInputPutDTO(long id, string titulo, long diretorId) {
        Id = id;
        Titulo = titulo;
        DiretorId = diretorId;
    }

     public class FilmeInputPutDTOValidator : AbstractValidator <FilmeInputPutDTO> {
     public FilmeInputPutDTOValidator (){
          RuleFor(filme => filme.Titulo).NotNull().NotEmpty();
          RuleFor(filme => filme.Titulo).Length(5,50).WithMessage("Tamanho {TotalLength} é invalido");
          RuleFor(filme => filme.DiretorId).NotNull().NotEmpty();
     }
}