using FluentValidation;

public class DiretorInputPostDTO {
     public string Nome { get; set; }
}

public class DiretorInputPostDTOValidator : AbstractValidator <DiretorInputPostDTO> {
     public DiretorInputPostDTOValidator (){
          RuleFor(diretor => diretor.Nome).NotNull().NotEmpty();
          RuleFor(diretor => diretor.Nome).Length(5,50).WithMessage("Tamanho {TotalLength} é invalido");
     }

}