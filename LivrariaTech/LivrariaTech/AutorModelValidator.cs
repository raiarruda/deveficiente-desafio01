using FluentValidation;

namespace LivrariaTech
{
    public class AutorModelValidator : AbstractValidator<AutorModel>
    {
        public AutorModelValidator()
        {


            RuleFor(x => x.Nome)
                .NotEmpty()
                    .WithMessage("Nome do autor é obrigatório");

            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email do autor é obrigatório")
                .EmailAddress()
                    .WithMessage("Email do autor - Formato inválido");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                    .WithMessage("A descrição do autor é obrigatória.")
                .MaximumLength(400)
                    .WithMessage("A descrição do autor não pode passar de 400 caracteres.");


        }
    }
}
