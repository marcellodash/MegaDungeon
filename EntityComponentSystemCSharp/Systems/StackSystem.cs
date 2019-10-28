﻿using System;
using System.Collections.Generic;
using EntityComponentSystemCSharp;
using EntityComponentSystemCSharp.Components;
using static EntityComponentSystemCSharp.EntityManager;

namespace EntityComponentSystemCSharp.Systems
{
	public class StackSystem : SystemBase, ISystem
	{
		public StackSystem(EntityManager em) : base(em)
		{
			// TODO maybe compact stacks? InventoryItem would need a count.
		}

		public override void Run()
		{
			throw new NotImplementedException();
		}

		public Dictionary<string, int> GetStacks(Entity entity)
		{
			var results = new Dictionary<string, int>();
			var inventory = entity.GetComponent<Inventory>();
			if(inventory != null)
			{
				foreach(var item in inventory.Items)
				{
					var itemComponent = item.GetComponent<Item>();

					if(itemComponent != null && item.HasComponent<Stackable>())
					{
						if(!results.ContainsKey(itemComponent.Type))
						{
							results.Add(itemComponent.Type, 0);
						}
						results[itemComponent.Type] += 1;
					}
				}
			}
			return results;
		}
	}
}