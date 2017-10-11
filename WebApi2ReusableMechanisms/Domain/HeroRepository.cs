using System;
using System.Collections.Generic;
using WebApi2ReusableMechanisms.Models;

namespace WebApi2ReusableMechanisms.Domain
{
	public interface IHeroRepository
	{
		HeroModel Create(HeroModel hero);
		HeroModel Get(Guid id);
		void Remove(Guid id);
		void Update(Guid id, HeroModel hero);
	}

	public class HeroRepository : IHeroRepository
	{
		static readonly Dictionary<Guid, HeroModel> Heroes = new Dictionary<Guid, HeroModel>();

		public HeroRepository()
		{
			Heroes.Add(DummyData.Superman.Id, DummyData.Superman);
			Heroes.Add(DummyData.Batman.Id, DummyData.Batman);
			Heroes.Add(DummyData.RocketRacoon.Id, DummyData.RocketRacoon);
		}

		public HeroModel Create(HeroModel hero)
		{
			var id = Guid.NewGuid();
			hero.Id = id;
			Heroes.Add(id, hero);

			return hero;
		}

		public void Update(Guid id, HeroModel hero)
		{
			hero.Id = id;
			Heroes[id] = hero;
		}

		public HeroModel Get(Guid id)
		{
			if (Heroes.ContainsKey(id))
				return Heroes[id];

			throw new ArgumentException($"Hero with id {id} not found");
		}

		public void Remove(Guid id)
		{
			Heroes.Remove(id);
		}
	}
}