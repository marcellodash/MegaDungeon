using System;
using EntityComponentSystemCSharp.Components;

namespace EntityComponentSystemCSharp.Systems
{
    public class CombatSystem : ISystem
	{
		Random _rand = new Random();
		EntityManager _em;
		ISystemLogger _logger;
		public CombatSystem(EntityManager em, ISystemLogger logger)
		{
			_logger = logger;
			_em = em;
		}

		public void Run()
		{
			foreach(var e in _em.GetAllEntitiesWithComponent<Attacked>())
			{
				var attacked = e.GetComponent<Attacked>();
				var alive = e.GetComponent<Alive>();
				var attackStat = attacked.attacker.GetComponent<AttackStat>();
				var defense = e.GetComponent<DefenseStat>();
				if(alive != null && attackStat != null)
				{
					var hit = _rand.Next(100);
					if(hit > attackStat.Accuracy)
					{
						var damage = _rand.Next(attackStat.Power + 1);
						if(defense != null)
						{
							if (hit > defense.Chance)
							{
								_logger.Log($"Hit {damage}.");
								alive.Health -= damage;
							}
						}
					}
					else
					{
						_logger.Log("miss!");
					}
				}
				e.RemoveComponent<Attacked>();
			}
		}
	}
}