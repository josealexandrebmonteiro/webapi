using FluentValidation;
public class DiretorInputPutDTO {
     public string Nome { get; set; }
}


public class DiretorInputPutDTOTOValidator : AbstractValidator <DiretorInputPutDTO> {
     public DiretorInputPutDTOTOValidator (){
          RuleFor(diretor => diretor.Nome).NotNull().NotEmpty();
          RuleFor(diretor => diretor.Nome).Length(5,50).WithMessage("Tamanho {TotalLength} é invalido");
     }

}