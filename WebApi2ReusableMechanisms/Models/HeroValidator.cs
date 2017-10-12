using FluentValidation;
using WebApi2ReusableMechanisms.Domain;

namespace WebApi2ReusableMechanisms.Models
{
	public class HeroValidator: AbstractValidator<HeroModel>
	{
		readonly IHeroRepository repository;

		public HeroValidator(IHeroRepository repository)
		{
			this.repository = repository;

			RuleFor(hero => hero.Name)
				.NotEmpty()
				.MaximumLength(80)
				.Must(BeUniqueName)
				.WithMessage("Hero with this name already exists");

			RuleFor(hero => hero.AlterEgo).Length(0, 80);
			RuleFor(hero => hero.Age).GreaterThanOrEqualTo(0);

			RuleFor(hero => hero.SuperPowers)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotNull()
				.Must(HaveOneSuperPower)
				.WithMessage("Hero should have at least one super power")
				.SetCollectionValidator(new SuperPowerValidator());

			RuleFor(hero => hero.Lair)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotNull()
				.Must(BeCorrectLairDetails)
				.WithMessage("Lair location should be null if LairType is None");
		}

		bool BeUniqueName(string name)
		{
			return !repository.Exists(name);
		}

		bool HaveOneSuperPower(SuperPower[] powers)
		{
			return powers.Length > 0;
		}

		bool BeCorrectLairDetails(Lair lair)
		{
			return lair.LairType != LairType.None || string.IsNullOrEmpty(lair.Location);
		}
	}

	public class SuperPowerValidator : AbstractValidator<SuperPower>
	{
		public SuperPowerValidator()
		{
			RuleFor(power => power.Description).NotEmpty();
		}
	}
}