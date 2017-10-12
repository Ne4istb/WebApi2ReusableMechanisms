using System;

namespace WebApi2ReusableMechanisms.Models
{
	public class HeroModel
	{
		public Guid Id;
		public string Name;
		public string AlterEgo;
		public int Age;
		public SuperPower[] SuperPowers;
		public Lair Lair;
	}

	public class SuperPower
	{
		public Guid Id;
		public string Description;
		public SourceType SourceType;
	}

	public enum SourceType
	{
		Acquired,
		Innate
	}

	public class Lair
	{
		public LairType LairType;
		public string Location;
	}

	public enum LairType
	{
		None,
		Building,
		Cave,
		Other
	}
}