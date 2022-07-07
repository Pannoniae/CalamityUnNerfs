using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace CalamityFly;

public class GItem : GlobalItem
{
	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		if(item.type == ItemID.EmpressFlightBooster)
		{
			item.SetNameOverride("Choete");
			var old = tooltips.Find(t=>t.Text.Contains("50%"));
			if (old != null)
			{
				var i = tooltips.IndexOf(old);
				tooltips[i] = new TooltipLine(CalamityFly.Instance, old.Name, "Grants infinite wing and rocket boot flight");
			}
		}
	}
}