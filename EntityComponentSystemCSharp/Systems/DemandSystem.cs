﻿using System;
using System.Collections.Generic;
using EntityComponentSystemCSharp;
using EntityComponentSystemCSharp.Components;
using static EntityComponentSystemCSharp.EntityManager;

namespace EntityComponentSystemCSharp.Systems
{

	public class DemandSystem : SystemBase, ISystem
	{
		public DemandSystem(IEngine engine) : base(engine)
		{
		}

		public override void Run(Entity entity)
		{
			if(!entity.HasComponent<Demand>()){return;}

			var demand = entity.GetComponent<Demand>();
			var inventory = entity.GetComponent<Inventory>();
			if(demand != null)
			{
				foreach(var d in demand.Demands)
				{
					var items = GetItemsFromInventory(entity, d.Key);
					var numToRemove = Math.Min(items.Length, d.Value);
					for(int i = 0; i < numToRemove; i++)
					{
						inventory.Items.Remove(items[i]);
					}
				}
			}
		}

		Entity[] GetItemsFromInventory(Entity entity, string type)
		{
			var inventory = entity.GetComponent<Inventory>();
			if(inventory == null)
			{
				throw new MissingComponentException("Production requires an inventory for storage.");
			}
			var returnList = new List<Entity>();
			foreach(var item in inventory.Items)
			{
				var itemComponent = item.GetComponent<Item>();
				if(itemComponent != null && itemComponent.Type == type)
				{
					returnList.Add(item);
				}
			}
			return returnList.ToArray();
		}
	}
}