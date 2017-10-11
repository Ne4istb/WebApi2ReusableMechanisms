using System;
using WebApi2ReusableMechanisms.Models;

namespace WebApi2ReusableMechanisms.Domain
{
	public static class DummyData
	{
		public static readonly HeroModel Superman = new HeroModel
		{
			Id = new Guid("30000000-0000-0000-0000-000000000001"),
			Name = "Clark Kent",
			Age = 32,
			SuperPowers = new[]
			{
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0001-000000000001"),
					Description = "Superhuman strength, speed, durability, and longevity",
					SourceType = SourceType.Innate
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0002-000000000001"),
					Description = "Flight",
					SourceType = SourceType.Innate
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0003-000000000001"),
					Description = "Heat vision",
					SourceType = SourceType.Innate
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0004-000000000001"),
					Description = "Freezing breath",
					SourceType = SourceType.Innate
				}
			},
			Lair = new Lair
			{
				LairType = LairType.Other,
				Location = "Metropolis"
			}
		};

		public static readonly HeroModel Batman = new HeroModel
		{
			Id = new Guid("30000000-0000-0000-0000-000000000002"),
			Name = "Bruce Wayne",
			Age = 39,
			SuperPowers = new[]
			{
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0001-000000000002"),
					Description = "Genius-level intellect",
					SourceType = SourceType.Innate
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0002-000000000002"),
					Description = "Peak human physical and mental condition",
					SourceType = SourceType.Acquired
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0003-000000000002"),
					Description = "Skilled martial artist and hand-to-hand combatant",
					SourceType = SourceType.Acquired
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0004-000000000002"),
					Description = "Expert detective",
					SourceType = SourceType.Acquired
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0005-000000000002"),
					Description = "Utilizes high-tech equipment and weapons",
					SourceType = SourceType.Acquired
				}
			},
			Lair = new Lair
			{
				LairType = LairType.Cave,
				Location = "Gotham"
			}
		};

		public static readonly HeroModel RocketRacoon = new HeroModel
		{
			Id = new Guid("30000000-0000-0000-0000-000000000003"),
			Name = "Rocket Raccoon",
			Age = 5,
			SuperPowers = new[]
			{
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0001-000000000003"),
					Description = "Master tactician and field commander",
					SourceType = SourceType.Acquired
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0002-000000000003"),
					Description = "Expert marksman and sniper",
					SourceType = SourceType.Acquired
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0003-000000000003"),
					Description = "Accomplished starship aviator",
					SourceType = SourceType.Acquired
				},
				new SuperPower
				{
					Id = new Guid("30000000-0000-0000-0004-000000000003"),
					Description = "Normal-physical attributes of an Earth raccoon",
					SourceType = SourceType.Innate
				}
			},
			Lair = new Lair
			{
				LairType = LairType.None
			}
		};
	}
}